#To build a docker image:
docker build -t docker-test-03:0.1

# To run a docker container:
docker run docker-test-03:0.1

# To run a docker container in the background:
docker run -d docker-test-03:0.1

# To "shell" into a running container
docker exec -it <container-id> /bin/sh
