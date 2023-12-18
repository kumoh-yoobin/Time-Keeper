## 실행 방법
실행 조건
- node 18.17.1
- docker

1. 라이브러리 설치
   ```bash
   npm install
   ```
2. docker에 redis-alpine 설치
   ```bash
   docker pull redis:alpine
   ```
3. redis server 실행
   ```bash
   docker run --hostname=a2a1e7adec46 --mac-address=02:42:ac:11:00:02 --env=PATH=/usr/local/sbin:/usr/local/bin:/usr/sbin:/usr/bin:/sbin:/bin --env=REDIS_VERSION=7.2.3 --env=REDIS_DOWNLOAD_URL=http://download.redis.io/releases/redis-7.2.3.tar.gz --env=REDIS_DOWNLOAD_SHA=3e2b196d6eb4ddb9e743088bfc2915ccbb42d40f5a8a3edd8cb69c716ec34be7 --volume=/data --workdir=/data -p 6379:6379 --restart=no --runtime=runc -d redis:alpine
   ```
4. (optional) pm2 설치 및 실행
   ```bash
   npm install -g pm2
   pm2 start "index.js" --name server
   ```
5. node 서버 실행
   ```bash
   node index.js
   ```
