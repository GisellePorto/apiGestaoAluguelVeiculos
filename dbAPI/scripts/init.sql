CREATE TABLE IF NOT EXISTS "Clientes" (
    "Id" SERIAL PRIMARY KEY,
    "Nome" VARCHAR(100) NOT NULL,
    "Cpf" VARCHAR(14) NOT NULL UNIQUE,
    "Email" VARCHAR(100) NOT NULL UNIQUE,
    "Telefone" VARCHAR(20)
);

CREATE TABLE IF NOT EXISTS "Veiculos" (
    "Id" SERIAL PRIMARY KEY,
    "Marca" VARCHAR(50) NOT NULL,
    "Modelo" VARCHAR(50) NOT NULL,
    "Ano" INTEGER NOT NULL CHECK ("Ano" >= 1900),
    "Placa" VARCHAR(10) NOT NULL UNIQUE,
    "ValorDiaria" DECIMAL(10, 2) NOT NULL CHECK ("ValorDiaria" > 0),
    "Disponivel" BOOLEAN NOT NULL DEFAULT TRUE
);

CREATE TABLE IF NOT EXISTS "Alugueis" (
    "Id" SERIAL PRIMARY KEY,
    "VeiculoId" INTEGER NOT NULL,
    "ClienteId" INTEGER NOT NULL,
    "DataInicio" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    "DataFim" TIMESTAMP WITHOUT TIME ZONE NOT NULL,
    
    CONSTRAINT "FK_Alugueis_Veiculos_VeiculoId" 
        FOREIGN KEY ("VeiculoId") 
        REFERENCES "Veiculos" ("Id") 
        ON DELETE RESTRICT,

    CONSTRAINT "FK_Alugueis_Clientes_ClienteId" 
        FOREIGN KEY ("ClienteId") 
        REFERENCES "Clientes" ("Id") 
        ON DELETE RESTRICT,
        
    CONSTRAINT "CK_Alugueis_Datas" 
        CHECK ("DataFim" > "DataInicio")
);