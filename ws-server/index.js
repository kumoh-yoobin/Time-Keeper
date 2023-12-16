const express = require('express');
const helmet = require('helmet');
const ws = require('ws');
const http = require('http');

const app = express();

const Clients = require('./modules/clients');
const util = require('./modules/util');

app.use(helmet.contentSecurityPolicy({
  directives: {
    defaultSrc: ["'self'"],
    connectSrc: ["*"]
    // 다른 CSP 설정...
  }
}));

const server = http.createServer(app);
const wss = new ws.Server({ server });

const RoomManager = require('./modules/RoomManager');

const gameTypes = ['lobby', 'yacht', 'laser', 'card'];


const managers = {};
gameTypes.forEach((type) => {
    managers[type] = new RoomManager(type);
});
console.log(managers)

wss.on('connection', (ws) => {
    console.log('Client connected');
    // create uid randomly
    const users = {
        msg: 'alert-uid',
        data: Clients.addSocket(ws),
    }
    ws.send(JSON.stringify(users));

    ws.on('message', (message) => {
        const data = JSON.parse(message);
        console.log(`on message: ${message}`)

        // Object.keys(managers).forEach((key) => {
        //     managers[key].printRooms();
        // });

        if (data.type === 'join') {
            const roomInfo = managers[data.roomType].joinRoom(data.user);
            const d = {
                msg: 'join-room',
                data: roomInfo.code,
            }
            ws.send(JSON.stringify(d));
            

        // turn 결정을 위해서 room에 있는 유저 수를 보내 줘야함
            let a = {
                player: roomInfo.users.length > 1 ? false : true
            }
            let player = {
                msg: 'room-msg',
                type: 'player-set',
                data: JSON.stringify(a)
            }
            ws.send(JSON.stringify(player));

            setTimeout(()=> {
                if (data.roomType === 'card' && roomInfo.users.length === 2) {
                    console.log('card game start');
                    let cardData = {
                        type: 'card-set',
                        cards: JSON.stringify(util.createCards()),
                    };
                    const room = managers['card'].getRoom(roomInfo.code);
                    room.sendMessage(JSON.stringify(cardData));
                }
            }, 1000);


            
        } else if (data.type === 'exit') {
            const room = manager.exitRoom(data.roomType, data.user, data.roomCode);
            ws.send(JSON.stringify(room));
        } else if (data.type === 'roomMsg') {
            const room = managers[data.roomType].getRoom(data.roomCode);
            room.sendMessage(data.msg);
        }
    })

    ws.on('close', () => {
        const key = Clients.searchSocket(ws);
        for(const type in managers) {
            managers[type].exitRoom(key);
        }
        Clients.removeSocket(key);
        console.log('Client disconnected');
    })


});

server.listen(8080, () => {
    console.log('Listening on http://localhost:8080');
});