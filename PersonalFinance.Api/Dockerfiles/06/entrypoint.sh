#!/bin/bash

/opt/mssql/bin/sqlservr & sleep 30s && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P DinSabSQLServerPassword@05 -d YourDatabaseName -i /docker-entrypoint-initdb.d/script.sql