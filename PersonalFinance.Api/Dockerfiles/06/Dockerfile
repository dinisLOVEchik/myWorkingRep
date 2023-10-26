FROM mcr.microsoft.com/mssql/server:2022-CU9-ubuntu-20.04
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=DinSabSQLServerPassword@05
ENV MSSQL_PID=Evaluation

COPY entrypoint.sh /usr/local/bin/

COPY script.sql /docker-entrypoint-initdb.d/

EXPOSE 1433

CMD /bin/bash /usr/local/bin/entrypoint.sh