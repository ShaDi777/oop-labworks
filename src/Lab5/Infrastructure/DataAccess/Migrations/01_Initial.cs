using System.Diagnostics.CodeAnalysis;
using FluentMigrator;
using Itmo.Dev.Platform.Postgres.Migrations;

namespace DataAccess.Migrations;

[SuppressMessage("Naming", "SA1649", Justification = "Migration")]
[Migration(1, "Initial")]
public class Initial : SqlMigration
{
    protected override string GetUpSql(IServiceProvider serviceProvider) =>
    """
    DO $$
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'user_role') THEN
            CREATE TYPE user_role AS enum
            (
                'admin',
                'user'
            );
        END IF;

        IF NOT EXISTS (SELECT 1 FROM pg_type WHERE typname = 'transaction_type') THEN
            CREATE TYPE transaction_type AS enum
            (
                'deposit',
                'withdraw',
                'transfer'
            );
        END IF;
    END$$;
    
    CREATE TABLE IF NOT EXISTS users
    (
        id bigint primary key generated always as identity,
        name text not null,
        salt text not null,
        password_hash text not null,
        role user_role not null
    );
    
    CREATE TABLE IF NOT EXISTS cards
    (
        card_number bigint primary key generated always as identity (minvalue 1000),
        salt text not null,
        pin_code_hash text not null,
        owner_user_id bigint not null references users(id),
        balance decimal not null
    );
    
    CREATE TABLE IF NOT EXISTS transactions
    (
        transaction_id bigint primary key generated always as identity ,
        card_number bigint not null references cards(card_number),
        transaction_type transaction_type not null,
        amount decimal not null,
        time_stamp timestamp not null
    );
    
    -- user_password is admin
    INSERT INTO users (name, salt, password_hash, role)
    SELECT 'admin', 'ADM1NSALT', '8B636480CE887BC4FD8F928D532120F56CBCAAD6E7E2E82311CC7736A96C3750', 'admin'
    WHERE NOT EXISTS(
        SELECT id FROM users
        WHERE name = 'admin' AND
              password_hash = '8B636480CE887BC4FD8F928D532120F56CBCAAD6E7E2E82311CC7736A96C3750'
    );
    """;

    protected override string GetDownSql(IServiceProvider serviceProvider) =>
    """
    drop table users;
    drop table cards;
    drop table transactions;
    
    drop type user_role;
    drop type transaction_type;
    """;
}