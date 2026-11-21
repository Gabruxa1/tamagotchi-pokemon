using RestSharp;
using System.Text.Json;
using tamagotchi_pokemon.Models;

namespace tamagotchi_pokemon.Services;

public class ServicoPokemon
{
	private readonly RestClient _client;

	public ServicoPokemon()
	{
		_client = new RestClient("https://pokeapi.co/api/v2/");
	}

	public async Task<List<EspeciePokemon>> ListarEspeciesAsync()
	{
		var request = new RestRequest("pokemon-species?limit=2000", Method.Get);

		var response = await _client.ExecuteAsync(request);

		if (!response.IsSuccessful ||
			string.IsNullOrWhiteSpace(response.Content))
		{
			throw new Exception("Erro ao consultar o catálogo de espécies.");
		}

		using var documento = JsonDocument.Parse(response.Content);

		var resultados = documento.RootElement.GetProperty("results");

		var especies = new List<EspeciePokemon>();

		foreach (var especie in resultados.EnumerateArray())
		{
			string nome = especie.GetProperty("name").GetString()!;
			string url = especie.GetProperty("url").GetString()!;

			int id = int.Parse(url.TrimEnd('/').Split('/').Last());

			especies.Add(new EspeciePokemon
			{
				Id = id,
				Nome = nome
			});
		}

		return especies;
	}

	public async Task<Mascote?> ObterPokemonAsync(int id)
	{
		var request = new RestRequest($"pokemon/{id}", Method.Get);

		var response = await _client.ExecuteAsync(request);

		if (!response.IsSuccessful ||
			string.IsNullOrWhiteSpace(response.Content))
		{
			return null;
		}

		Mascote? mascote = JsonSerializer.Deserialize<Mascote>(
			response.Content
		);

		return mascote;
	}
}
