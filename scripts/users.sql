create table "Users" (
	"Id" serial primary key,
	"Username" varchar(50) not null,
	"PasswordHash" varchar(500) not null,
	"Email" varchar(100) not null,
	"Status" varchar(20) not null,
	"CreatedAt" timestamptz default current_timestamp,
	"UpdatedAt" timestamptz default current_timestamp
);

create user ucb with encrypted password 'abc123.,';
grant all privileges on database "erpUcb" to ucb;
GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO ucb;
GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO ucb;