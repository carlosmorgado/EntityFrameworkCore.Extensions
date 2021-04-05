#start SQL Server, start the script to create the DB and import the data
/opt/mssql/bin/sqlservr & /db/import-data.sh & tail -f /dev/null