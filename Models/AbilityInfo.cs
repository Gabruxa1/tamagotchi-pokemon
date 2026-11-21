using System.Text.Json.Serialization;

namespace tamagotchi_pokemon.Models;

public class AbilityInfo
{
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	[JsonPropertyName("url")]
	public string Url { get; set; } = string.Empty;
}
