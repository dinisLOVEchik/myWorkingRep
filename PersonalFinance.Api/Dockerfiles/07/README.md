#To build a docker image:
docker build --build-arg root_pw=YourPassword -t docker-test-07:0.1 .

# To run a docker container:
docker run -p 3306:3306 -e MYSQL_ROOT_PASSWORD=YourPassword docker-test-07:0.1

# To run a docker container in the background:
docker run -d -p 3306:3306 -e MYSQL_ROOT_PASSWORD=YourPassword docker-test-07:0.1

# To "shell" into a running container
docker exec -it <container-id> /bin/sh
