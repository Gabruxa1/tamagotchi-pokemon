# 🐾 Pokémon Tamagotchi

**Projeto de desenvolvimento em C# utilizando a PokéAPI**

O Pokémon Tamagotchi é um projeto experimental desenvolvido em C# com o objetivo de construir uma aplicação inspirada nos clássicos jogos de cuidados com mascotes virtuais, utilizando Pokémon como personagens.

A proposta é evoluir gradualmente a aplicação, explorando conceitos de programação, consumo de APIs REST, orientação a objetos, persistência de dados e desenvolvimento de funcionalidades interativas.

---

## 📌 Etapa 1 — Integração com a PokéAPI e seleção de espécies

**Status:** Concluída

### Objetivo

Implementar a comunicação entre a aplicação C# e a PokéAPI, permitindo que o jogador consulte o catálogo de espécies disponíveis, escolha um Pokémon por meio de seu ID e visualize as informações retornadas pela API.

### Tecnologias utilizadas

| Tecnologia         | Aplicação                       |
| ------------------ | ------------------------------- |
| C#                 | Linguagem de programação        |
| .NET 10            | Plataforma de desenvolvimento   |
| Visual Studio Code | Ambiente de desenvolvimento     |
| RestSharp 114.1.0  | Cliente para requisições HTTP   |
| System.Text.Json   | Leitura e interpretação de JSON |
| PokéAPI            | Fonte externa de dados          |

### Funcionalidades implementadas

- Inicialização de uma aplicação Console com .NET 10.
- Instalação e configuração da biblioteca RestSharp via NuGet.
- Configuração do endereço-base da PokéAPI.
- Requisição HTTP GET para consultar o catálogo de espécies.
- Consulta de até 2.000 espécies por meio do parâmetro `limit`.
- Extração dos IDs e nomes diretamente dos resultados da API.
- Exibição das opções disponíveis no console.
- Solicitação de escolha do Pokémon por meio de um identificador inteiro (`int`).
- Validação da entrada utilizando `int.TryParse()`.
- Validação da existência do ID utilizando `HashSet<int>`.
- Segunda requisição HTTP GET para consultar o Pokémon selecionado.
- Recebimento e impressão do JSON retornado pela API.

### Endpoints utilizados

**1. Consulta do catálogo de espécies**

Método: `GET`

```text
https://pokeapi.co/api/v2/pokemon-species?limit=2000
```

Responsabilidade: recuperar a lista de espécies, incluindo seus nomes e URLs, das quais são extraídos os identificadores.

**2. Consulta individual do Pokémon**

Método: `GET`

```text
https://pokeapi.co/api/v2/pokemon/{id}
```

Responsabilidade: recuperar as características do Pokémon escolhido pelo jogador.

### Fluxo de execução

```text
INÍCIO
   |
   v
Consultar catálogo de espécies
   |
   v
Exibir ID e nome dos Pokémon
   |
   v
Solicitar ID ao jogador
   |
   v
Validar entrada
   |
   +---- ID inválido ----> Solicitar novamente
   |
   v
Consultar Pokémon pelo ID
   |
   v
Receber resposta JSON
   |
   v
Exibir informações no console
   |
   v
FIM
```

### Conceitos praticados

- Comunicação HTTP entre aplicações.
- Consumo de APIs REST.
- Utilização de bibliotecas externas através do NuGet.
- Criação de clientes e requisições com RestSharp.
- Execução assíncrona com `async/await`.
- Leitura e interpretação de respostas JSON com `System.Text.Json`.
- Manipulação de coleções e estruturas de dados.
- Conversão e validação de entradas do usuário.
- Controle de fluxo utilizando estruturas condicionais e repetição.

---

## 🐾 Espécies de interesse do projeto

O catálogo inicial de mascotes terá cinco espécies como referência para o desenvolvimento das mecânicas de cuidado:

| Pokémon    |  ID | Característica planejada             |
| ---------- | --: | ------------------------------------ |
| Eevee      | 133 | Evoluções condicionadas aos cuidados |
| Togepi     | 175 | Felicidade e amizade                 |
| Rockruff   | 744 | Energia e treinamento                |
| Sprigatito | 906 | Personalidade e afinidade            |
| Fuecoco    | 909 | Alimentação e descanso               |

**Observação:** o catálogo implementado consulta as espécies disponibilizadas pela PokéAPI, não estando limitado às cinco espécies de interesse inicial.

---

## 🗺️ Roadmap do projeto

| Etapa | Desenvolvimento                                      | Status    |
| ----- | ---------------------------------------------------- | --------- |
| 01    | Integração com a PokéAPI e seleção por ID            | Concluída |
| 02    | Tratamento de erros e desserialização tipada do JSON | Planejada |
| 03    | Modelagem das entidades Pokémon em C#                | Planejada |
| 04    | Exibição estruturada das características dos Pokémon | Planejada |
| 05    | Sistema de adoção de mascotes                        | Planejada |
| 06    | Necessidades: fome, energia e felicidade             | Planejada |
| 07    | Evolução e sistema de amizade                        | Planejada |
| 08    | Persistência e salvamento dos dados                  | Planejada |

---

## ▶️ Como executar

### Pré-requisitos

- .NET 10 SDK instalado.
- Acesso à internet.
- Visual Studio Code ou outro editor compatível com C#.

### Restaurar as dependências

```bash
dotnet restore
```

### Executar a aplicação

```bash
dotnet run
```

A aplicação realizará a consulta ao catálogo de espécies e solicitará ao jogador o ID do Pokémon desejado.

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
