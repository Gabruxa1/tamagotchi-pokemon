# 🐾 Pokémon Tamagotchi

Projeto desenvolvido em C# com integração à [PokéAPI](https://pokeapi.co/), com o objetivo de construir uma aplicação de console inspirada em mascotes virtuais Pokémon.

O desenvolvimento é organizado em etapas, utilizando conceitos de programação orientada a objetos, consumo de APIs REST, manipulação de JSON, mapeamento de objetos e modelagem de dados.

## 🚀 Etapas do desenvolvimento

### Etapa 1 — Consulta à PokéAPI e seleção de Pokémon

**Status:** Concluída.

Implementação da integração com a PokéAPI para consultar e apresentar o catálogo de espécies disponíveis.

**Funcionalidades implementadas:**

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

**Funcionalidades implementadas:**

- Criação de classes para representar os dados retornados pela API.
- Mapeamento das propriedades JSON utilizando `JsonPropertyName`.
- Desserialização da resposta com `JsonSerializer.Deserialize<Mascote>()`, posteriormente reorganizada na Etapa 6 para utilizar o modelo `Pokemon`.
- Representação de objetos aninhados.
- Modelagem de listas de habilidades.
- Identificação de habilidades e suas propriedades.
- Exibição organizada das características do Pokémon selecionado.

**Características apresentadas:**

- Nome do Pokémon.
- Altura.
- Peso.
- Lista de habilidades.

**Estrutura dos modelos na implementação atual:**

```text
Models/
├── Pokemon.cs
├── Mascote.cs
├── AbilitySlot.cs
├── AbilityInfo.cs
└── EspeciePokemon.cs
```

**Relacionamento entre os modelos:**

- `Pokemon`: representa os dados detalhados recebidos da PokéAPI.
- `Mascote`: representa o mascote utilizado pelo domínio do jogo.
- `AbilitySlot`: representa cada habilidade e suas propriedades.
- `AbilityInfo`: representa os dados internos de cada habilidade, incluindo nome e URL.
- `EspeciePokemon`: representa os dados básicos das espécies disponíveis para seleção.

### Etapa 3 — Interface, menu e sistema de adoção

**Status:** Concluída.

Implementação da interface interativa do jogo, permitindo identificar o jogador, navegar pelo menu principal, consultar Pokémon e realizar adoções.

**Funcionalidades implementadas:**

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

**Organização das classes na Etapa 3:**

- `Program.cs`: ponto de entrada da aplicação.
- `InterfaceUsuario.cs`: concentra a interação com o jogador, menus e exibição de informações.
- `ServicoPokemon.cs`: centraliza as consultas à PokéAPI.
- `EspeciePokemon.cs`: representa os dados básicos das espécies disponíveis para seleção.

A partir da Etapa 4, essas classes foram reorganizadas em camadas específicas, e o fluxo principal passou a ser controlado por `TamagotchiController`.

**Observação:** os mascotes adotados são armazenados apenas durante a execução da aplicação. Os dados ainda não são persistidos em arquivos ou banco de dados.

### Etapa 4 — Organização da arquitetura MVC

**Status:** Concluída.

Reorganização da estrutura do projeto utilizando os conceitos do padrão arquitetural MVC (Model-View-Controller), separando as responsabilidades entre as classes.

**Funcionalidades e melhorias implementadas:**

- Criação da camada `Controllers` para centralizar o controle do jogo.
- Criação da camada `Views` para organizar a interface com o jogador.
- Criação da camada `Services` para separar a comunicação com a PokéAPI.
- Transferência do fluxo principal de execução para `TamagotchiController`.
- Implementação do método `Jogar()` para controlar o loop principal da aplicação.
- Implementação do método `AdotarPokemon()` para centralizar o processo de adoção.
- Inicialização dos serviços e da interface por meio do construtor do Controller.
- Simplificação do `Program.cs`, que passou a atuar apenas como ponto de entrada da aplicação.
- Utilização de operações assíncronas com `async` e `await`.
- Organização dos namespaces conforme a estrutura de diretórios.

**Responsabilidades das camadas:**

- `Models`: representação dos dados e entidades do jogo.
- `Views`: interação com o jogador, menus e apresentação das informações.
- `Controllers`: coordenação do fluxo do jogo e das regras de interação.
- `Services`: comunicação com a PokéAPI e processamento das respostas externas.

**Observação:** a arquitetura utiliza os conceitos do MVC adaptados a uma aplicação de console, com uma camada adicional de serviços para integração com a API.

### Etapa 5 — Interação e necessidades dos mascotes

**Status:** Concluída.

Implementação das mecânicas básicas de interação com os Pokémon adotados, permitindo acompanhar e modificar suas necessidades durante o jogo.

**Atributos implementados:**

- `Alimentacao`: representa o nível de alimentação do mascote.
- `Humor`: representa o estado de felicidade do mascote.
- `Energia`: representa o nível de disposição e descanso do mascote.

Os três atributos utilizam uma escala de 0 a 10 e recebem valores iniciais aleatórios no construtor da classe `Mascote`.

**Funcionalidades implementadas:**

- Inicialização aleatória das necessidades dos mascotes.
- Seleção de um Pokémon já adotado para interação.
- Consulta individual do estado do mascote.
- Exibição dos níveis de alimentação, humor e energia.
- Classificação textual das condições de cada atributo.
- Implementação das ações brincar, alimentar e dormir.
- Atualização dos atributos conforme as ações executadas.
- Validação das condições necessárias para executar cada ação.
- Bloqueio de ações quando as necessidades correspondentes impedem sua execução.
- Proteção dos atributos para mantê-los no intervalo de 0 a 10.
- Retorno ao menu principal sem perder o estado dos mascotes durante a execução.

**Regras de interação:**

| Ação      | Alimentação |         Humor |       Energia |
| --------- | ----------: | ------------: | ------------: |
| Brincar   |          -1 |            +2 |            -2 |
| Alimentar |          +3 |            +1 | Sem alteração |
| Dormir    |          -1 | Sem alteração |            +4 |

**Restrições das ações:**

- Brincar é bloqueado quando a alimentação ou a energia está em zero.
- Alimentar é bloqueado quando a alimentação já está em 10.
- Dormir é bloqueado quando a energia já está em 10.
- Os atributos nunca ultrapassam os limites de 0 a 10.

**Responsabilidades das classes:**

- `Mascote`: mantém os atributos e implementa as regras de negócio das interações.
- `InterfaceUsuario`: apresenta os menus, permite selecionar mascotes e exibe seus estados.
- `TamagotchiController`: coordena as interações e apresenta o resultado das ações ao jogador.

**Observação:** os atributos e estados dos mascotes permanecem em memória durante a execução da aplicação. A persistência dos dados ainda não foi implementada.

### Etapa 6 — Mapeamento de objetos com AutoMapper

**Status:** Concluída.

Implementação do mapeamento automático entre os objetos que representam os dados recebidos da PokéAPI e os objetos utilizados pelo domínio do jogo.

**Funcionalidades implementadas:**

- Instalação da biblioteca AutoMapper 17.0.0.
- Criação do modelo `Pokemon` para representar a resposta detalhada da PokéAPI.
- Separação entre o modelo de dados externos (`Pokemon`) e o modelo de domínio (`Mascote`).
- Atualização do serviço `ServicoPokemon` para desserializar as respostas em objetos `Pokemon`.
- Criação do diretório `Mappings` para centralizar as configurações de mapeamento.
- Implementação de `PokemonMappingProfile` utilizando `Profile`.
- Configuração do mapeamento entre `Pokemon` e `Mascote`.
- Preservação dos atributos de necessidades do mascote durante o mapeamento.
- Integração do `IMapper` ao `TamagotchiController`.
- Validação da configuração do AutoMapper com `AssertConfigurationIsValid()`.

**Fluxo de conversão dos dados:**

```text
PokéAPI
   ↓
Resposta JSON
   ↓
Desserialização com System.Text.Json
   ↓
Modelo Pokemon
   ↓
AutoMapper
   ↓
Modelo Mascote
   ↓
Regras de negócio e interações do jogo
```

**Responsabilidades dos modelos:**

- `Pokemon`: representa os dados recebidos da PokéAPI, incluindo nome, altura, peso e habilidades.
- `Mascote`: representa o mascote no domínio do jogo, mantendo os dados do Pokémon e os atributos próprios de alimentação, humor e energia.
- `PokemonMappingProfile`: define as regras de conversão entre `Pokemon` e `Mascote`.

**Configuração do mapeamento:**

O AutoMapper realiza a correspondência automática das propriedades com nomes compatíveis entre os modelos.

Os atributos `Alimentacao`, `Humor` e `Energia` não existem na resposta da PokéAPI. Por isso, são ignorados explicitamente durante o mapeamento, preservando os valores inicializados pelo construtor de `Mascote`.

**Observação:** a validação da configuração do AutoMapper permite identificar propriedades sem mapeamento, garantindo maior consistência na conversão entre os objetos.

## 🛠️ Tecnologias utilizadas

| Tecnologia         | Aplicação                      |
| ------------------ | ------------------------------ |
| C#                 | Linguagem de programação       |
| .NET 10            | Plataforma de desenvolvimento  |
| RestSharp 114.1.0  | Consumo de APIs REST           |
| System.Text.Json   | Leitura e desserialização JSON |
| AutoMapper 17.0.0  | Mapeamento entre objetos C#    |
| PokéAPI            | Fonte dos dados dos Pokémon    |
| Visual Studio Code | Ambiente de desenvolvimento    |
| Git e GitHub       | Controle de versão             |

## 📁 Estrutura do projeto

```text
tamagotchi-pokemon/
│
├── Controllers/
│   └── TamagotchiController.cs
│
├── Mappings/
│   └── PokemonMappingProfile.cs
│
├── Models/
│   ├── Mascote.cs
│   ├── Pokemon.cs
│   ├── AbilitySlot.cs
│   ├── AbilityInfo.cs
│   └── EspeciePokemon.cs
│
├── Services/
│   └── ServicoPokemon.cs
│
├── Views/
│   └── InterfaceUsuario.cs
│
├── Program.cs
├── tamagotchi-pokemon.csproj
├── README.md
└── .gitignore
```

**Responsabilidade dos diretórios:**

- `Controllers/`: controle do fluxo e das funcionalidades do jogo.
- `Mappings/`: configura as conversões entre os modelos de dados e os modelos de domínio.
- `Models/`: representação dos dados e entidades.
- `Services/`: integração com serviços externos, como a PokéAPI.
- `Views/`: interface de comunicação com o jogador.

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

| Branch                          | Etapa                                 |
| ------------------------------- | ------------------------------------- |
| `main`                          | Versão principal                      |
| `etapa-1-consulta-pokeapi`      | Consulta à API e seleção por ID       |
| `etapa-2-desserializacao-json`  | Modelagem e desserialização JSON      |
| `etapa-3-interface-adocao`      | Interface, menu e sistema de adoção   |
| `etapa-4-arquitetura-mvc`       | Organização da arquitetura MVC        |
| `etapa-5-interacao-mascotes`    | Interação e necessidades dos mascotes |
| `etapa-6-mapeamento-automapper` | Mapeamento de objetos com AutoMapper  |

## 📌 Próximas etapas

As funcionalidades abaixo fazem parte do planejamento e ainda não foram implementadas:

- [ ] Persistência dos mascotes adotados.
- [ ] Sistema de amizade e evolução.
- [ ] Mecânicas de evolução dos Pokémon.
- [ ] Aprimoramento da navegação e apresentação do catálogo.

## 🎯 Objetivo do projeto

Construir progressivamente uma aplicação de mascote virtual Pokémon, aplicando conceitos de desenvolvimento C#, integração com APIs, orientação a objetos, manipulação de dados, mapeamento de objetos e organização de software.

O projeto possui caráter educacional e cada etapa é desenvolvida e validada individualmente.
