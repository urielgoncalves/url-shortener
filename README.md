# url-shortener
.NET SDK 5.0.302 + ReactJS 17.0.2


## Backend
Directory: /UrlShortenerAPI \
https://localhost:5001/ \
VSCode: dotnet run \
It can also be run via Visual Studio

Ps: For now the information are in memory cache.

### Running on Docker
UrlShortenerAPI$ docker build -t url-shortener-api:dev . \
UrlShortenerAPI$ docker run --rm -dt -p 5000:80 -p 5001:443 url-shortener-api:dev \
http://localhost:5000/


## Frontend
Directory: /app \
http://localhost:3000/

npm install \
npm start


### Running on Docker
app$ docker build -t url-shortener-app:dev . \
app$ docker run --rm -p 3000:3000 url-shortener-app:dev


## Future improvements:
-Validate url's \
-Improve unique ID algorithm for URL's \
-Add TTL for short URL's \
-Add database \
-Add distributed cache for scalability
