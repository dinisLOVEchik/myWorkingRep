#To build a docker image:
docker build -t <username/image-name> -f <path-of-Dockerfile>

#Example: docker build -t nerzhaveyka/personal-finance-api-dock -f ./Dockerfile-test01 .

# To run a docker container:
docker run <image-name>

# To run a docker container in the background:
docker run -d <image-name>

# To "shell" into a running container
docker exec -it <container-id> /bin/sh
