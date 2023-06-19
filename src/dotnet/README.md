
# Redis Commands
Create Docker Instance: `docker run --name some-redis -d redis`

With persistant storage: `docker run --name some-redis -d redis redis-server --save 60 1 --loglevel warning`

