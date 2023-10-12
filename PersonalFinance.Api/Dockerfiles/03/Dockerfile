
FROM alpine:latest
RUN apk update
RUN apk add nano

COPY ./app .

RUN apk add nginx
COPY nginx.conf /etc/nginx/sites-available/nginx.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]