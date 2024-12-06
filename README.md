# CRUD de cadastro de Processos

## Objetivo

O objetivo deste projeto é criar uma aplicação web em C# que permita realizar operações CRUD (Create, Read, Update, Delete) para o cadastro de processos. As funcionalidades implementadas incluem o cadastro, listagem, edição e exclusão de processos, utilizando as seguintes tecnologias e padrões:

- **ASP.NET Core MVC**
- **Entity Framework**
- **Banco de dados SQL**

## Funcionalidades

- **Cadastro de Processos**: Permite adicionar novos processos com validação dos campos obrigatórios.
- **Listagem de Processos**: Exibe uma lista paginada dos processos cadastrados, mostrando apenas o NPU, a data de cadastro e a UF do processo.
- **Edição de Processos**: Possibilidade de editar as informações dos processos cadastrados.
- **Exclusão de Processos**: Permite excluir processos indesejados.
- **Visualização de Detalhes**: Ao clicar em visualizar, são exibidas as informações completas do processo, incluindo o nome do processo, NPU, data de cadastro, UF e município.
- **Integração com a API do IBGE**: Na tela de cadastro, ao selecionar a UF, é realizada uma busca na API do IBGE para obter os municípios correspondentes.

## Campos da Tabela de Processos

A tabela de processos contém os seguintes campos:

- **Id**: Identificador único do processo (chave primária).
- **Nome do Processo**: Nome descritivo do processo.
- **NPU**: Número único de processo no formato `1111111-11.1111.1.11.1111`, com validação para aceitar apenas números.
- **Data de Cadastro**: Data em que o processo foi cadastrado no sistema.
- **Data de Visualização**: Data e hora em que o processo foi visualizado.
- **UF**: Unidade Federativa do processo.
- **Município**: Nome do município do processo.

## Tecnologias Utilizadas

- **ASP.NET Core MVC**: Para criação do front-end e back-end da aplicação.
- **Entity Framework Core**: Para interação com o banco de dados SQL.
- **Banco de Dados SQL**: Para armazenar as informações dos processos.
- **API do IBGE**: Utilizada para obter a lista de municípios com base na UF selecionada. [Documentação da API](https://servicodados.ibge.gov.br/api/docs/localidades)

## Como Rodar o Projeto Localmente

### Clonar o Repositório:

```bash
git clone <url>
cd ProcessRegistration
```

### Configurar o Banco de Dados:

1. **Crie um banco de dados SQL** utilizando os scripts disponibilizados na pasta `App_Data` do repositório.
2. **Atualize a string de conexão** no arquivo `appsettings.json` para conectar ao seu banco de dados.

4. **Rodar a Aplicação**:

   ```bash
   dotnet run
   ```

5. **Acessar a Aplicação**:

   Abra o navegador e acesse `https://localhost:7042`.

## Setup do Banco de Dados

O banco de dados utilizado é um **SQL Server LocalDB**. Inclua os seguintes scripts para criar a tabela de processos:

```sql
CREATE TABLE Processes (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    NPU NVARCHAR(25) NOT NULL,
    RegistrationDate DATETIME NOT NULL,
    ViewDate DATETIME NULL,
    State NVARCHAR(2) NOT NULL,
    City NVARCHAR(100) NOT NULL,
    CityCode INT NOT NULL
);
```
