FROM docker-test-03:0.1
RUN apk update && apk add nginx
COPY nginx.conf /etc/nginx/nginx.conf

EXPOSE 8080

CMD ["nginx", "-g", "daemon off;"]