const redis = require('./redis');

const Clients = require('./clients');

class Room {
  #code;
  #type;
  #users;
  #state;
  #changedFields = new Set();

  get code() {
    return this.#code;
  }

  get type() {
    return this.#type;
  }

  get users() {
    return this.#users;
  }

  get state() {
    return this.#state;
  }

  set code(value) {
    this.#code = value;
    this.#changedFields.add('code');
  }

  set type(value) {
    this.#type = value;
    this.#changedFields.add('type');
  }

  set users(value) {
    this.#users = value;
    this.#changedFields.add('users');
  }

  set state(value) {
    this.#state = value;
    this.#changedFields.add('state');
  }

  #updateRedis() {
    for (const field of this.#changedFields) {
      if (field === 'users') {
        redis.setData(`${this.#type}:${this.#code}:${field}`, JSON.stringify(this[field]));
        continue;
      }
      redis.setData(`${this.#type}:${this.#code}:${field}`, this[field]);
    }

    this.#changedFields.clear();
  }

  constructor(...args) {
    if (args.length === 0) this.#conscturctorNewRoom('lobby');
    else if (args.length === 1) this.#conscturctorNewRoom(...args);
    else if (args.length === 4) this.#constructorOldRoom(...args);
    else throw new Error('Invalid arguments');
  }

  #conscturctorNewRoom(type) {
    this.code = Math.random().toString(36).substring(2, 6).toUpperCase();
    this.type = type;
    this.users = [];
    this.state = 'waiting';
    this.#updateRedis();
  }

  #constructorOldRoom(type, code, users, state) {
    this.code = code;
    this.type = type;
    this.users = users;
    this.state = state;
  }

  getRoomInfo() {
    return {
      code: this.#code,
      type: this.#type,
      users: this.#users,
      state: this.#state,
    }
  }

  getState() {
    return this.#state;
  }
  getRoomCode() {
    return this.#code;
  }

  joinUser(user) {

    // 유저 중복 체크
    let isDuplicate = false;
    this.#users.forEach((u) => {
      if (u === user) isDuplicate = true;
    });
    if(isDuplicate) return this;

    this.#users.push(user);
    this.#changedFields.add('users');
    if (this.#users.length === 2) {
      this.state = 'playing';
    }
    this.#updateRedis();
    return this;
  }
  
  sendMessage(msg) {
    const sockets = this.#users.map((user) => Clients.getSocket(user));
    msg = JSON.parse(msg);

    const d = {
      msg: 'room-msg',
      type: msg.type,
      data: JSON.stringify(msg),
    }
    console.log(`data: ${JSON.stringify(d)}\nRoom: ${this.getRoomCode()}`)
    // console.log(sockets);
    sockets.forEach((socket) => {
      socket.send(JSON.stringify(d));
    });
  }

  exitUser(user) {
    const exitUser = this.#users.find((u) => u === user);
    if (!exitUser) return false;

    this.#users = this.#users.filter((u) => u !== user);
    if (this.#users.length === 0) {
      this.#state = 'waiting';
    }

    console.log(`exitUser: ${exitUser}\nRoom: ${this.getRoomCode()}\nusers: ${this.#users}`)
    this.#changedFields.add('users');
    this.#updateRedis();
    return true;
  }
};

module.exports = Room;