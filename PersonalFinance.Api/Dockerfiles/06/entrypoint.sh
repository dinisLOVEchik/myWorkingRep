#!/bin/bash

/opt/mssql/bin/sqlservr & sleep 30s && /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P Password123! -d RatesBase -i /docker-entrypoint-initdb.d/script.sql