FROM mcr.microsoft.com/mssql/server

ARG SA_PASSWORD="SQLpass!1"
ENV SA_PASSWORD=$SA_PASSWORD
ENV ACCEPT_EULA="Y"

EXPOSE 1433

RUN mkdir -p /var/opt/mssql/scripts
COPY ./build/database/*.sql /var/opt/mssql/scripts/
WORKDIR /var/opt/mssql/scripts

# Start SQL Server and execute the SQL script
#CMD /bin/bash -c "/opt/mssql/bin/sqlservr & /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -i 00-create-db.sql"
#CMD /bin/bash -c "/opt/mssql/bin/sqlservr & sleep 30 && /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -i 00-create-db.sql"

# Start SQL Server and execute the SQL script after it becomes available
CMD /bin/bash -c "/opt/mssql/bin/sqlservr & \
                  while ! /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -Q 'SELECT 1' ; do \
                    sleep 5 ; \
                  done ; \
                  /opt/mssql-tools/bin/sqlcmd -S localhost -U SA -P $SA_PASSWORD -i 00-create-db.sql ; \
                  tail -f /dev/null"