using System.Text.Json.Serialization;

namespace tamagotchi_pokemon.Models;

public class Pokemon
{
	[JsonPropertyName("id")]
	public int Id { get; set; }

	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	[JsonPropertyName("height")]
	public int Height { get; set; }

	[JsonPropertyName("weight")]
	public int Weight { get; set; }

	[JsonPropertyName("abilities")]
	public List<AbilitySlot> Abilities { get; set; } = new();
}
