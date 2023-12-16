const Clients = new Map();

module.exports = {
  addSocket: (socket) => {
    const id = Math.random().toString(36).substring(2, 16);
    Clients.set(id, socket);

    return id;
  },
  removeSocket: (id) => {
    Clients.delete(id);
  },
  getSocket: (id) => {
    return Clients.get(id);
  },
  getAllSockets: () => {
    return Clients;
  },
  searchSocket: (socket) => {
    for (const [key, value] of Clients) {
      if (value === socket) {
        return key;
      }
    }
  }
}