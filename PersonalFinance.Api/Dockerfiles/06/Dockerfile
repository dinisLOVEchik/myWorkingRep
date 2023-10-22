FROM mcr.microsoft.com/mssql/server:2022-latest
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=DinSabSQLServerPassword@05
ENV MSSQL_PID=Evaluation

COPY script.sql /docker-entrypoint-initdb.d/

EXPOSE 1433

CMD ["/opt/mssql/bin/sqlservr"]