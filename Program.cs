using RestSharp;
using System.Text.Json;
using tamagotchi_pokemon;
using tamagotchi_pokemon.Models;

var interfaceUsuario = new InterfaceUsuario();

string nomeJogador = interfaceUsuario.BoasVindas();

int opcao;

do
{
	opcao = interfaceUsuario.ExibirMenuPrincipal(nomeJogador);

	switch (opcao)
	{
		case 1:
			Console.WriteLine("Menu de adoção de mascotes.");
			break;

		case 2:
			Console.WriteLine("Lista de mascotes adotados.");
			break;

		case 3:
			Console.WriteLine($"Até a proxima, {nomeJogador}!");
			break;
	}

	if (opcao != 3)
	{
		Console.WriteLine();
		Console.WriteLine("Pressione ENTER para continuar...");
		Console.ReadLine();
	}
} while (opcao != 3);

return;

var client = new RestClient("https://pokeapi.co/api/v2/");

// 1. Consultar a lista de espécies
var request = new RestRequest("pokemon-species?limit=2000", Method.Get);

var response = await client.ExecuteAsync(request);

if (!response.IsSuccessful || string.IsNullOrWhiteSpace(response.Content))
{
	Console.WriteLine("Erro ao consultar as espécies.");
	return;
}

// 2. Interpretar o JSON recebido
using var documento = JsonDocument.Parse(response.Content);

var resultados = documento.RootElement.GetProperty("results");

var idsDisponiveis = new HashSet<int>();

// 3. Apresentar as opções ao jogador
Console.WriteLine("================================");
Console.WriteLine("     ESCOLHA SEU POKÉMON");
Console.WriteLine("================================");

foreach (var especie in resultados.EnumerateArray())
{
	string nome = especie.GetProperty("name").GetString()!;
	string url = especie.GetProperty("url").GetString()!;

	// Extrair o ID da URL da espécie
	int id = int.Parse(url.TrimEnd('/').Split('/').Last());

	idsDisponiveis.Add(id);

	Console.WriteLine($"{id} - {nome}");
}

// 4. Receber e validar a escolha
int escolha;

while (true)
{
	Console.WriteLine();
	Console.Write("Digite o ID do Pokémon desejado: ");

	if (!int.TryParse(Console.ReadLine(), out escolha) ||
		!idsDisponiveis.Contains(escolha))
	{
		Console.WriteLine("ID inválido. Tente novamente.");
		continue;
	}

	break;
}

// 5. Consultar os dados do Pokémon escolhido
var pokemonRequest = new RestRequest($"pokemon/{escolha}", Method.Get);

var pokemonResponse = await client.ExecuteAsync(pokemonRequest);

// 6. Converter o JSON em um objeto Mascote
if (pokemonResponse.IsSuccessful &&
	!string.IsNullOrWhiteSpace(pokemonResponse.Content))
{
	Mascote? mascote = JsonSerializer.Deserialize<Mascote>(
		pokemonResponse.Content
	);

	if (mascote == null)
	{
		Console.WriteLine("Não foi possível interpretar os dados do Pokémon.");
		return;
	}

	Console.WriteLine("\nPokémon selecionado:");
	Console.WriteLine($"Nome: {mascote.Name}");
	Console.WriteLine($"Altura: {mascote.Height}");
	Console.WriteLine($"Peso: {mascote.Weight}");

	Console.WriteLine("\nHabilidades:");

	foreach (var habilidade in mascote.Abilities)
	{
		Console.WriteLine($"{habilidade.Ability.Name}".ToUpper());

	}
}
else
{
	Console.WriteLine("Não foi possível consultar o Pokémon.");
}