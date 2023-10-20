FROM mcr.microsoft.com/mssql/server:2022-latest
ENV ACCEPT_EULA=Y
ENV SA_PASSWORD=DinSabSQLServerPassword@05
WORKDIR /data

COPY ./script.sql .

RUN /opt/mssql/bin/sqlservr --accept-eula & sleep 20s \
    && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P DinSabSQLServerPassword@05 -i script.sql \
    && pkill sqlservr

EXPOSE 8080

CMD /opt/mssql/bin/sqlservr