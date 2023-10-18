FROM mcr.microsoft.com/mssql/server:2022-latest
RUN apk update
WORKDIR /data

COPY ./data/script.sql/docker-entrypoint-initdb.d/

EXPOSE 8080

ENTRYPOINT ["SELECT", "*", "FROM", "rates;"]