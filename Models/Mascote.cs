using System.Text.Json.Serialization;

namespace tamagotchi_pokemon.Models;

public class Mascote
{
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	[JsonPropertyName("height")]
	public int Height { get; set; }

	[JsonPropertyName("weight")]
	public int Weight { get; set; }

	[JsonPropertyName("abilities")]
	public List<AbilitySlot> Abilities { get; set; } = new();

	public int Alimentacao { get; set; }
	public int Humor { get; set; }
	public int Energia { get; set; }

	public Mascote()
	{
		Random random = new();

		Alimentacao = random.Next(0, 11);
		Humor = random.Next(0, 11);
		Energia = random.Next(0, 11);
	}

	public bool Brincar()
	{
		if (Energia <= 0 || Alimentacao <= 0)
		{
			return false;
		}

		Alimentacao = Math.Max(0, Alimentacao - 1);
		Humor = Math.Min(10, Humor + 2);
		Energia = Math.Max(0, Energia - 2);

		return true;
	}

	public bool Alimentar()
	{
		if (Alimentacao >= 10)
		{
			return false;
		}

		Alimentacao = Math.Min(10, Alimentacao + 3);
		Humor = Math.Min(10, Humor + 1);

		return true;
	}

	public bool Dormir()
	{
		if (Energia >= 10)
		{
			return false;
		}

		Energia = Math.Min(10, Energia + 4);
		Alimentacao = Math.Max(0, Alimentacao - 1);

		return true;
	}
}