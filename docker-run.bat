docker stop crypto
docker rm crypto
docker build -t crypto:latest .
docker run -d --restart unless-stopped -p 8081:8081 -p 8080:8080 --name crypto crypto