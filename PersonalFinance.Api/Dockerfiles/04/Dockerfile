
FROM docker-test-03:0.1
RUN apk add nginx
COPY nginx.conf /etc/nginx/sites-available/nginx.conf

EXPOSE 80

CMD ["nginx", "-g", "daemon off;"]