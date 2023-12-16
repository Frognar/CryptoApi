docker build -t crypto .
docker run -d --restart unless-stopped -p 8081:8081 -p 8080:8080 --name crypto crypto