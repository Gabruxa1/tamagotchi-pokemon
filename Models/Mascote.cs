namespace tamagotchi_pokemon.Models;

public class Mascote
{
	public string Name { get; set; } = string.Empty;
	public int Height { get; set; }
	public int Weight { get; set; }
	public List<AbilitySlot> Abilities { get; set; } = new();
	public int Alimentacao { get; set; }
	public int Humor { get; set; }
	public int Energia { get; set; }
	public decimal Amizade { get; set; } = 0m;
	public decimal DesgasteAlimentacao { get; set; }
	public decimal DesgasteHumor { get; set; }
	public decimal DesgasteEnergia { get; set; }
	public DateTimeOffset? UltimaAtualizacaoUtc { get; set; }
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

		if (Alimentacao == 0)
		{
			DesgasteAlimentacao = 0m;
		}

		if (Energia == 0)
		{
			DesgasteEnergia = 0m;
		}

		Amizade = Math.Clamp(Amizade + 1m, 0m, 10m);

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
		Amizade = Math.Clamp(Amizade + 0.5m, 0m, 10m);

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

		if (Alimentacao == 0)
		{
			DesgasteAlimentacao = 0m;
		}

		return true;
	}
}