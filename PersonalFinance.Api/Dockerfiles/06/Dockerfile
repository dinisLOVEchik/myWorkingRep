FROM mcr.microsoft.com/mssql/server:2022-CU9-ubuntu-20.04
ENV ACCEPT_EULA=Y
ENV MSSQL_SA_PASSWORD=Password123!
ENV MSSQL_PID=Evaluation

COPY script.sql /docker-entrypoint-initdb.d/

EXPOSE 1433

COPY entrypoint.sh /usr/local/bin/

USER root
RUN bash -c 'chmod +x /usr/local/bin/entrypoint.sh'

CMD /bin/bash /usr/local/bin/entrypoint.sh