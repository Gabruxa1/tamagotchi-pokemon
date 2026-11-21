using System.Text.Json.Serialization;

namespace tamagotchi_pokemon.Models;

public class AbilitySlot
{
	[JsonPropertyName("ability")]
	public AbilityInfo Ability { get; set; } = new();

	[JsonPropertyName("is_hidden")]
	public bool IsHidden { get; set; }

	[JsonPropertyName("slot")]
	public int Slot { get; set; }
}
