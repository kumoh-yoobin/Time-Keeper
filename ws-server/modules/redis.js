const redis = require('redis');
const util = require('./util');

const client = redis.createClient({ legacyMode: false });

client.on('error', (err) => {
  console.log('Error ' + err);
});

client.on('connect', () => {
  console.log('Redis client connected');
});

client.connect().then();



module.exports = {
  setData : async (key, value = '') => {
    await client.set(key, value);
  },
  getKey: async (key, callback) => {
    const datas = await client.keys(key);
    callback(datas);

    return datas;
  },
  getDataAsync : async (key) => {
    return await client.get(key);
  }
}