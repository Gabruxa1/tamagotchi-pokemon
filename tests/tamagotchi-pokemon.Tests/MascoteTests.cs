using tamagotchi_pokemon.Models;

namespace tamagotchi_pokemon.Tests;

public class MascoteTests
{
	[Fact]
	public void Brincar_AtributosChegamAZero_ZeraAcumuladoresCorrespondentes()
	{
		// Arrange
		var mascote = new Mascote
		{
			Alimentacao = 1,
			Humor = 5,
			Energia = 2,
			DesgasteAlimentacao = 0.5m,
			DesgasteHumor = 0.2m,
			DesgasteEnergia = 0.4m
		};

		// Act
		bool resultado = mascote.Brincar();

		// Assert
		Assert.True(resultado);

		Assert.Equal(0, mascote.Alimentacao);
		Assert.Equal(7, mascote.Humor);
		Assert.Equal(0, mascote.Energia);

		Assert.Equal(0m, mascote.DesgasteAlimentacao);
		Assert.Equal(0.2m, mascote.DesgasteHumor);
		Assert.Equal(0m, mascote.DesgasteEnergia);
	}

	[Fact]
	public void Dormir_AlimentacaoChegaAZero_ZeraAcumuladorAlimentacao()
	{
		// Arrange
		var mascote = new Mascote
		{
			Alimentacao = 1,
			Humor = 5,
			Energia = 5,
			DesgasteAlimentacao = 0.5m,
			DesgasteHumor = 0.2m,
			DesgasteEnergia = 0.4m
		};

		// Act
		bool resultado = mascote.Dormir();

		// Assert
		Assert.True(resultado);

		Assert.Equal(0, mascote.Alimentacao);
		Assert.Equal(9, mascote.Energia);

		Assert.Equal(0m, mascote.DesgasteAlimentacao);
		Assert.Equal(0.2m, mascote.DesgasteHumor);
		Assert.Equal(0.4m, mascote.DesgasteEnergia);
	}

	[Fact]
	public void Alimentar_RecuperaAtributos_PreservaAcumuladores()
	{
		// Arrange
		var mascote = new Mascote
		{
			Alimentacao = 5,
			Humor = 5,
			Energia = 5,
			DesgasteAlimentacao = 0.5m,
			DesgasteHumor = 0.2m,
			DesgasteEnergia = 0.4m
		};

		// Act
		bool resultado = mascote.Alimentar();

		// Assert
		Assert.True(resultado);

		Assert.Equal(8, mascote.Alimentacao);
		Assert.Equal(6, mascote.Humor);

		Assert.Equal(0.5m, mascote.DesgasteAlimentacao);
		Assert.Equal(0.2m, mascote.DesgasteHumor);
		Assert.Equal(0.4m, mascote.DesgasteEnergia);
	}

	[Theory]
	[InlineData(0, 5, 5)]
	[InlineData(5, 5, 0)]
	public void Brincar_NecessidadeZerada_BloqueiaSemAlterarEstado(
		int alimentacao,
		int humor,
		int energia)
	{
		// Arrange
		var mascote = new Mascote
		{
			Alimentacao = alimentacao,
			Humor = humor,
			Energia = energia,
			DesgasteAlimentacao = 0.5m,
			DesgasteHumor = 0.2m,
			DesgasteEnergia = 0.4m
		};

		// Act
		bool resultado = mascote.Brincar();

		// Assert
		Assert.False(resultado);

		Assert.Equal(alimentacao, mascote.Alimentacao);
		Assert.Equal(humor, mascote.Humor);
		Assert.Equal(energia, mascote.Energia);

		Assert.Equal(0.5m, mascote.DesgasteAlimentacao);
		Assert.Equal(0.2m, mascote.DesgasteHumor);
		Assert.Equal(0.4m, mascote.DesgasteEnergia);
	}
}