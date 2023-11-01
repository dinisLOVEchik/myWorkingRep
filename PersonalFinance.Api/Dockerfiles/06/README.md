#To build a docker image:
docker build --build-arg SAPASSWORD=Your_Password -t docker-test-06:0.1 .

# To run a docker container:
docker run -p 1433:1433 docker-test-06:0.1

# To run a docker container in the background:
docker run -d -p 1433:1433 docker-test-06:0.1

# To "shell" into a running container
docker exec -it <container-id> /bin/sh
