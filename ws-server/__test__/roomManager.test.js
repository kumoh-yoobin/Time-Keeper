const RoomManager = require('../modules/RoomManager');

describe('RoomManager 테스트', () => {
  test('방 입장 테스트', () => {
    const manager = new RoomManager();
    const room = manager.joinRoom('yacht', 'user1');

    expect(room).toEqual(expect.objectContaining({
      code: expect.any(String),
      type: 'yacht',
      users: ['user1'],
      state: 'waiting',
    }));
  });

  test('방 퇴장 테스트', () => {
    const manager = new RoomManager();
    const room = manager.joinRoom('yacht', 'user1');

    const rooms = 
    manager
      .exitRoom('yacht', 'user1', room.code)
      .getRooms('yacht');

    expect(rooms).toHaveLength(0);
  })
});