#To build a docker image:
docker build -t docker-test-04:0.1 .

# To run a docker container:
docker run -p 8080:80 docker-test-04:0.1

# To run a docker container in the background:
docker run -d -p 8080:80 docker-test-04:0.1

# To "shell" into a running container
docker exec -it <container-id> /bin/sh
