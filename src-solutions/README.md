"# ASP.NET-Core-Simple-Store"

docker build --no-cache -t simplestore .

docker run -it -p 8000:8080 simplestore

docker-compose -f "docker-compose.yml" -f "docker-compose.override.yml" -p dockercompose3076579552341503214 --no-ansi config

docker-compose up
docker-compose down

docker-compose.exe -f docker-compose-linebase.yml -f docker-compose.override.yml config

docker-compose.exe -f docker-compose-linebase.yml -f docker-compose.override.yml up
