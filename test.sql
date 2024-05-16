CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL
);


"DefaultConnection": "Host=db;Port=5432;Database=postgres;Username=postgres;Password=postgres"