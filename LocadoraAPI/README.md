🚗 API de Gestão e Aluguel de Veículos

API RESTful desenvolvida em .NET 10 (C#) e PostgreSQL.

🛠️ Tecnologias Utilizadas

    C# / .NET 10 (ASP.NET Core Web API)
    Entity Framework Core (ORM)
    PostgreSQL 17 (Banco de dados relacional)
    Docker & Docker Compose (Containerização do banco)
    Swagger / OpenAPI (Documentação interativa)
    Postman Collection (Testes de integração)

📋 Pré-requisitos

Antes de iniciar, você precisará ter instalado:

    .NET SDK 10
    Docker e Docker Compose
    Git

🚀 Como Executar o Projeto

Passo 1: Clonar o Repositório

Abra o terminal e execute:

    git clone <apiGestaoAluguelVeiculos>
    cd apiGestaoAluguelVeiculos

Passo 2: Iniciar o Banco de Dados (PostgreSQL via Docker)

Na pasta raiz do projeto, execute o Docker Compose para subir o banco de dados:

terminal:

    docker compose up -d

O container locadora-db iniciará na porta 5433. O script de inicialização (scripts/init.sql) cria as tabelas e insere registros de teste automaticamente.

Passo 3: Executar a Aplicação .NET

Acesse a pasta da API, restaure as dependências e inicie o servidor:

Terminal:

    cd LocadoraAPI
    dotnet restore
    dotnet run

A API será inicializada e responderá em: http://localhost:5077/swagger