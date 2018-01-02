//to add new migration use this commands

//postgress sql
add-migration MIGRATIONNAME -context ApplicationNpgsqlContext -o Migrations\NpgsqlMigrations
update-database -context ApplicationNpgsqlContext

//sql server
add-migration MIGRATIONNAME -context ApplicationContext -o Migrations\SqlServerMigrations
update-database -context ApplicationContext