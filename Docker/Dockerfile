# Reference: https://github.com/twright-msft/mssql-node-docker-demo-app/blob/master/Dockerfile
FROM mcr.microsoft.com/mssql/rhel/server

# Switch to root user for access to apt-get install
USER root

COPY . /db
WORKDIR /db

# Grant permissions for the import-data script to be executable
RUN chmod +x /db/import-data.sh
# Grant permissions for the import-data script to be executable
RUN chmod +x /db/entrypoint.sh

EXPOSE 1433

# Switch back to mssql user and run the entrypoint script
USER mssql
ENTRYPOINT /bin/bash ./entrypoint.sh