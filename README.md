# url-shortener
.NET SDK 5.0.302 + ReactJS 17.0.2


## Backend
Directory: /UrlShortenerAPI \
https://localhost:5001/ \
VSCode: dotnet run \
It can also be run via Visual Studio

Ps: For now the information are in memory cache.

### Running on Docker
docker build -t urlshortenerapi:dev .
docker run -dt -p 5000:80 -p 5001:443 urlshortenerapi:dev
http://localhost:5000/


## Frontend
Directory: /app \
http://localhost:3000/

npm install \
npm start


## Next steps:
-Add unit tests on Backend side \
-Generate Docker images \
-Validate url's \
-Refactoring unique ID for URL's \
-Review react components \
-Add TTL for short URL's \
-Add database \
-Add distributed cache for scalability
