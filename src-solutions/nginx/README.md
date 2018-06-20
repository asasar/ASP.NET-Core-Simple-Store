mkdir -p ssl
openssl req -x509 -nodes -days 365 -newkey rsa:2048 -keyout ssl/nginx.key -out ssl/nginx.crt
openssl dhparam -out ssl/dhparam.pem 4096


docker-compose -f docker-compose.production-proxy.yml build --no-cache
docker-compose -f docker-compose.production-proxy.yml up --scale websimplestore=2

docker-compose -f docker-compose.production-proxy.yml down
docker-compose -f docker-compose.production-proxy.yml stop