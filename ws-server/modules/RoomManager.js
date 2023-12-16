const redis = require('./redis');

const Room = require('./Room');

class RoomManager {
  #type;
  
  #rooms;

  async #getRoomFromCode(code) {
    const roomkey = ['users', 'state']
    const room = []
    for(const field of roomkey) {
      const value = await redis.getDataAsync(`${this.#type}:${code}:${field}`);
      room.push(value);
    }
    return room;
  } 

  async #getRooms(codes) {
    const rooms = [];
    for(const code of codes) {
      const room = await this.#getRoomFromCode(code);
      rooms.push(room);
    }

    this.#rooms = rooms.map((room, idx) => {
      return new Room(this.#type, codes[idx], JSON.parse(room[0]), room[1]);
    });
  }

  constructor(type) {
    this.#type = type;
    redis.getKey(`${type}:*:code`, (data) => {
      data = data.map((item) => item.split(':')[1]);
      this.#getRooms(data);
    })
  }

  joinRoom(user) {
    const room = this.#rooms.find((room) => room.getState() === 'waiting');
    if (!room) {
      const newRoom = new Room(this.#type);
      newRoom.joinUser(user);
      this.#rooms.push(newRoom);
      return newRoom.getRoomInfo();
    }
    if (!room.joinUser(user)) {
      return {
        code: false
      }
    }
    return room.getRoomInfo();
  }

  exitRoom(user) {
    this.#rooms.forEach((room) => {
      if (room.exitUser(user)) {
        return room.getRoomInfo();
      }
    });
  } 

  printRooms() {
    this.#rooms.forEach((room) => {
      console.log(room.getRoomInfo());
    });
  }

  getRoom(code) {
    return this.#rooms.find((room) => room.getRoomCode() === code);
  }
};

module.exports = RoomManager;