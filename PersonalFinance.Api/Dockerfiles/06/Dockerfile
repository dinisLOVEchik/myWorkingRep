FROM mcr.microsoft.com/mssql/server:2022-CU9-ubuntu-20.04
ENV ACCEPT_EULA=Y

ARG SAPASSWORD
ENV MSSQL_SA_PASSWORD=$SAPASSWORD

COPY script.sql /docker-entrypoint-initdb.d/

EXPOSE 1433
RUN /opt/mssql/bin/sqlservr & sleep 30s && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P $SAPASSWORD -i /docker-entrypoint-initdb.d/script.sql