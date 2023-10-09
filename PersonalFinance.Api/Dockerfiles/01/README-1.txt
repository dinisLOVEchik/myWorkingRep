#To build a docker image:
docker build -t nerzhaveyka/personal-finance-api-dock -f ./Dockerfile-test01 .

# To run a docker container:
docker run personal-finance-api-dock

# To run a docker container in the background:
docker run -d personal-finance-api-dock

# To "shell" into a running container
docker exec -it <container-id> /bin/sh
