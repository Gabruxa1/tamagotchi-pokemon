# 🐾 Pokémon Tamagotchi

Projeto desenvolvido em C# com integração à [PokéAPI](https://pokeapi.co/), com o objetivo de construir uma aplicação de console inspirada em mascotes virtuais Pokémon.

O desenvolvimento é organizado em etapas, utilizando conceitos de programação orientada a objetos, consumo de APIs REST, manipulação de JSON e modelagem de dados.

## 🚀 Etapas do desenvolvimento

### Etapa 1 — Consulta à PokéAPI e seleção de Pokémon

**Status:** Concluída.

Implementação da integração com a PokéAPI para consultar e apresentar o catálogo de espécies disponíveis.

Funcionalidades implementadas:

- Configuração do cliente HTTP utilizando RestSharp.
- Consulta ao endpoint `pokemon-species`.
- Recuperação de até 2.000 espécies.
- Extração dos IDs e nomes das espécies.
- Exibição do catálogo no terminal.
- Solicitação do ID do Pokémon desejado.
- Validação da entrada utilizando `int.TryParse` e `HashSet<int>`.
- Consulta dos dados individuais pelo endpoint `pokemon/{id}`.

**Endpoints utilizados:**

```http
GET https://pokeapi.co/api/v2/pokemon-species?limit=2000
GET https://pokeapi.co/api/v2/pokemon/{id}
```

### Etapa 2 — Desserialização e modelagem dos dados JSON

**Status:** Concluída.

Implementação da conversão da resposta JSON da PokéAPI em objetos C# utilizando `System.Text.Json`.

Funcionalidades implementadas:

- Criação de classes para representar os dados retornados pela API.
- Mapeamento das propriedades JSON utilizando `JsonPropertyName`.
- Desserialização da resposta com `JsonSerializer.Deserialize<Mascote>()`.
- Representação de objetos aninhados.
- Modelagem de listas de habilidades.
- Identificação de habilidades e suas propriedades.
- Exibição organizada das características do Pokémon selecionado.

**Características apresentadas:**

- Nome do Pokémon.
- Altura.
- Peso.
- Lista de habilidades.

**Estrutura dos modelos:**

```text
Models/
├── Mascote.cs
├── AbilitySlot.cs
└── AbilityInfo.cs
```

**Relacionamento entre os modelos:**

- `Mascote`: representa os dados principais do Pokémon.
- `AbilitySlot`: representa cada habilidade e suas propriedades.
- `AbilityInfo`: representa os dados internos de cada habilidade, incluindo nome e URL.

### Etapa 3 — Interface, menu e sistema de adoção

**Status:** Concluída.

Implementação da interface interativa do jogo, permitindo identificar o jogador, navegar pelo menu principal, consultar Pokémon e realizar adoções.

Funcionalidades implementadas:

- Tela inicial com apresentação do jogo.
- Solicitação e validação do nome do jogador.
- Menu principal com opções de adoção, consulta da coleção e saída.
- Consulta do catálogo de espécies pela PokéAPI.
- Seleção de Pokémon pelo ID.
- Opção de visualizar as características antes da adoção.
- Exibição dos detalhes do Pokémon selecionado.
- Confirmação ou cancelamento da adoção.
- Armazenamento dos Pokémon adotados em uma coleção `List<Mascote>`.
- Listagem dos mascotes adotados, incluindo nome, altura e peso.
- Exibição da quantidade total de mascotes adotados.
- Tratamento de exceções durante o processo de adoção.

**Organização das classes:**

- `Program.cs`: coordena o fluxo principal da aplicação e mantém a coleção de mascotes adotados.
- `InterfaceUsuario.cs`: concentra a interação com o jogador, menus e exibição de informações.
- `ServicoPokemon.cs`: centraliza as consultas à PokéAPI.
- `EspeciePokemon.cs`: representa os dados básicos das espécies disponíveis para seleção.

**Observação:** os mascotes adotados são armazenados apenas durante a execução da aplicação. Os dados ainda não são persistidos em arquivos ou banco de dados.

## 🛠️ Tecnologias utilizadas

| Tecnologia         | Aplicação                      |
| ------------------ | ------------------------------ |
| C#                 | Linguagem de programação       |
| .NET 10            | Plataforma de desenvolvimento  |
| RestSharp 114.1.0  | Consumo de APIs REST           |
| System.Text.Json   | Leitura e desserialização JSON |
| PokéAPI            | Fonte dos dados dos Pokémon    |
| Visual Studio Code | Ambiente de desenvolvimento    |
| Git e GitHub       | Controle de versão             |

## 📁 Estrutura do projeto

```text
tamagotchi-pokemon/
│
├── Models/
│   ├── Mascote.cs
│   ├── AbilitySlot.cs
│   ├── AbilityInfo.cs
│   └── EspeciePokemon.cs
│
├── Program.cs
├── InterfaceUsuario.cs
├── ServicoPokemon.cs
├── tamagotchi-pokemon.csproj
├── README.md
└── .gitignore
```

Os diretórios `bin/` e `obj/` são gerados automaticamente pelo .NET e não são versionados.

## ▶️ Como executar

**Pré-requisito:** .NET 10 SDK instalado.

Clone o repositório e acesse o diretório do projeto:

```bash
git clone URL_DO_REPOSITORIO
cd tamagotchi-pokemon
```

Restaure as dependências:

```bash
dotnet restore
```

Execute a aplicação:

```bash
dotnet run
```

## 🌱 Organização das branches

O desenvolvimento é separado por etapas, mantendo a branch `main` como referência principal do projeto.

| Branch                         | Etapa                               |
| ------------------------------ | ----------------------------------- |
| `main`                         | Versão principal                    |
| `etapa-1-consulta-pokeapi`     | Consulta à API e seleção por ID     |
| `etapa-2-desserializacao-json` | Modelagem e desserialização JSON    |
| `etapa-3-interface-adocao`     | Interface, menu e sistema de adoção |

## 📌 Próximas etapas

As funcionalidades abaixo fazem parte do planejamento e ainda não foram implementadas:

- [ ] Persistência dos mascotes adotados.
- [ ] Sistema de alimentação e necessidades.
- [ ] Sistema de felicidade e amizade.
- [ ] Mecânicas de evolução.
- [ ] Interações com os mascotes.
- [ ] Aprimoramento da navegação e apresentação do catálogo.

## 🎯 Objetivo do projeto

Construir progressivamente uma aplicação de mascote virtual Pokémon, aplicando conceitos de desenvolvimento C#, integração com APIs, orientação a objetos, manipulação de dados e organização de software.

O projeto possui caráter educacional e cada etapa é desenvolvida e validada individualmente.
