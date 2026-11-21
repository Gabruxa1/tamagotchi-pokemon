# 🐾 Pokémon Tamagotchi

**Projeto de desenvolvimento em C# utilizando a PokéAPI**

O Pokémon Tamagotchi é um projeto experimental desenvolvido em C# com o objetivo de construir uma aplicação inspirada nos clássicos jogos de cuidados com mascotes virtuais, utilizando Pokémon como personagens.

A proposta é evoluir gradualmente a aplicação, explorando conceitos de programação, consumo de APIs REST, orientação a objetos, persistência de dados e desenvolvimento de funcionalidades interativas.

---

## 📌 Etapa 1 — Primeira integração com a PokéAPI

**Status:** Concluída

### Objetivo

Implementar a primeira comunicação entre a aplicação C# e uma API externa, utilizando uma requisição HTTP do tipo GET para consultar informações de um Pokémon.

### Tecnologias utilizadas

| Tecnologia         | Aplicação                     |
| ------------------ | ----------------------------- |
| C#                 | Linguagem de programação      |
| .NET 10            | Plataforma de desenvolvimento |
| Visual Studio Code | Ambiente de desenvolvimento   |
| RestSharp 114.1.0  | Cliente para requisições HTTP |
| PokéAPI            | Fonte externa de dados        |

### Funcionalidades implementadas

- Inicialização de uma aplicação Console com .NET 10.
- Instalação e configuração da biblioteca RestSharp via NuGet.
- Configuração do endereço-base da PokéAPI.
- Construção de uma requisição HTTP GET.
- Consulta dos dados do Pokémon Eevee.
- Recebimento da resposta HTTP.
- Impressão do conteúdo JSON diretamente no console.

### Endpoint utilizado

**Método:** GET

```text
https://pokeapi.co/api/v2/pokemon/eevee
```

### Conceitos praticados

- Comunicação HTTP entre aplicações.
- Consumo de APIs REST.
- Utilização de bibliotecas externas através do NuGet.
- Criação de clientes e requisições com RestSharp.
- Execução assíncrona com `async/await`.
- Recebimento e leitura de respostas JSON.

---

## 🐾 Espécies planejadas

O catálogo inicial de mascotes será composto por cinco espécies:

| Pokémon    | Característica                       |
| ---------- | ------------------------------------ |
| Eevee      | Evoluções condicionadas aos cuidados |
| Togepi     | Felicidade e amizade                 |
| Rockruff   | Energia e treinamento                |
| Sprigatito | Personalidade e afinidade            |
| Fuecoco    | Alimentação e descanso               |

**Observação:** nesta primeira etapa, somente a consulta do Eevee foi implementada. As demais espécies fazem parte do escopo planejado.

---

## 🗺️ Roadmap do projeto

| Etapa | Desenvolvimento                          | Status    |
| ----- | ---------------------------------------- | --------- |
| 01    | Primeira consulta à PokéAPI com HTTP GET | Concluída |
| 02    | Tratamento e desserialização do JSON     | Planejada |
| 03    | Modelagem das entidades Pokémon em C#    | Planejada |
| 04    | Catálogo e seleção de espécies           | Planejada |
| 05    | Sistema de adoção de mascotes            | Planejada |
| 06    | Necessidades: fome, energia e felicidade | Planejada |
| 07    | Evolução e sistema de amizade            | Planejada |
| 08    | Persistência e salvamento dos dados      | Planejada |

---

## ▶️ Como executar

**Pré-requisitos:**

- .NET 10 SDK instalado.
- Acesso à internet.

Clone o repositório e entre na pasta do projeto.

Restaure as dependências:

```bash
dotnet restore
```

Execute a aplicação:

```bash
dotnet run
```

---

## 📂 Estrutura atual

```text
tamagotchi-pokemon/
│
├── Program.cs
├── tamagotchi-pokemon.csproj
├── README.md
├── .gitignore
│
├── bin/
└── obj/
```

_Os diretórios `bin/` e `obj/` são gerados automaticamente pelo .NET e não devem ser versionados._

---

## 🎯 Objetivo final

Desenvolver uma aplicação de mascotes virtuais Pokémon, permitindo ao jogador escolher, adotar e cuidar de diferentes espécies, acompanhando suas necessidades, personalidade, felicidade e evolução.

**Projeto em desenvolvimento — Etapa 1 concluída.**
