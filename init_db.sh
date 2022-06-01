#!/bin/bash
set -e

psql -v ON_ERROR_STOP=1 --username "standard" --dbname "standard" <<-EOSQL
    CREATE USER core_slave WITH PASSWORD 'core';
    CREATE DATABASE standard_core WITH OWNER 'core_slave';
EOSQL

psql -v ON_ERROR_STOP=1 --username "core_slave" --dbname "standard_core" <<-EOSQL
    CREATE EXTENSION IF NOT EXISTS "uuid-ossp";
EOSQL