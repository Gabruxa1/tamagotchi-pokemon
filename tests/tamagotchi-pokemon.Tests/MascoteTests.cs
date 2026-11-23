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
			Amizade = 3m,
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
		Assert.Equal(3m, mascote.Amizade);
	}


	[Fact]
	public void Construtor_AmizadeInicializaEmZero()
	{
		// Arrange
		var mascote = new Mascote();

		// Assert
		Assert.Equal(0m, mascote.Amizade);
	}

	[Fact]
	public void Brincar_AcaoBemSucedida_AumentaAmizadeEmUm()
	{
		// Arrange
		var mascote = new Mascote
		{
			Alimentacao = 5,
			Humor = 5,
			Energia = 5,
			Amizade = 2m
		};

		// Act
		bool resultado = mascote.Brincar();

		// Assert
		Assert.True(resultado);
		Assert.Equal(3m, mascote.Amizade);
	}

	[Fact]
	public void Alimentar_AcaoBemSucedida_AumentaAmizadeEmMeioPonto()
	{
		// Arrange
		var mascote = new Mascote
		{
			Alimentacao = 5,
			Humor = 5,
			Energia = 5,
			Amizade = 2m
		};

		// Act
		bool resultado = mascote.Alimentar();

		// Assert
		Assert.True(resultado);
		Assert.Equal(2.5m, mascote.Amizade);
	}

	[Fact]
	public void Dormir_AcaoBemSucedida_NaoAlteraAmizade()
	{
		// Arrange
		var mascote = new Mascote
		{
			Alimentacao = 5,
			Humor = 5,
			Energia = 5,
			Amizade = 4m
		};

		// Act
		bool resultado = mascote.Dormir();

		// Assert
		Assert.True(resultado);
		Assert.Equal(4m, mascote.Amizade);
	}

	[Fact]
	public void Brincar_AmizadeNoMaximo_NaoUltrapassaDez()
	{
		// Arrange
		var mascote = new Mascote
		{
			Alimentacao = 5,
			Humor = 5,
			Energia = 5,
			Amizade = 10m
		};

		// Act
		bool resultado = mascote.Brincar();

		// Assert
		Assert.True(resultado);
		Assert.Equal(10m, mascote.Amizade);
	}

	[Fact]
	public void Alimentar_AmizadeProximaDoMaximo_LimitaEmDez()
	{
		// Arrange
		var mascote = new Mascote
		{
			Alimentacao = 5,
			Humor = 5,
			Energia = 5,
			Amizade = 9.5m
		};

		// Act
		bool resultado = mascote.Alimentar();

		// Assert
		Assert.True(resultado);
		Assert.Equal(10m, mascote.Amizade);
	}
}
