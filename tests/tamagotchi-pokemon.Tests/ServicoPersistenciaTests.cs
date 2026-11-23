using tamagotchi_pokemon.Services;
using tamagotchi_pokemon.Models;
using System.Text.Json;

namespace tamagotchi_pokemon.Tests;

public class ServicoPersistenciaTests
{
	[Fact]
	public async Task CarregarAsync_ArquivoInexistente_RetornaEstadoVazio()
	{
		// Arrange
		string diretorioTemporario = Path.Combine(
			Path.GetTempPath(),
			Guid.NewGuid().ToString()
		);

		string caminhoArquivo = Path.Combine(
			diretorioTemporario,
			"jogo.json"
		);

		var servico = new ServicoPersistencia(caminhoArquivo);

		// Act
		EstadoJogo resultado = await servico.CarregarAsync();

		// Assert
		Assert.Empty(resultado.Mascotes);
		Assert.Equal(string.Empty, resultado.NomeTreinador);
		Assert.False(File.Exists(caminhoArquivo));
	}

	[Fact]
	public async Task SalvarAsync_GravarEstadoJogo_CarregaDadosCorretamente()
	{
		// Arrange
		string diretorioTemporario = Path.Combine(
			Path.GetTempPath(),
			Guid.NewGuid().ToString()
		);

		string caminhoArquivo = Path.Combine(
			diretorioTemporario,
			"jogo.json"
		);

		var servico = new ServicoPersistencia(caminhoArquivo);

		var mascote = new Mascote
		{
			DesgasteAlimentacao = 0.5m,
			DesgasteHumor = 0.2m,
			DesgasteEnergia = 0.4m,
			UltimaAtualizacaoUtc = new DateTimeOffset(2026, 11, 22, 12, 0, 0, TimeSpan.Zero),
			Name = "pikachu",
			Height = 4,
			Weight = 60,
			Alimentacao = 8,
			Humor = 9,
			Energia = 7,
			Amizade = 3.5m,
			Abilities = new List<AbilitySlot>
		{
			new()
			{
				Ability = new AbilityInfo
				{
					Name = "static",
					Url = "https://pokeapi.co/api/v2/ability/9/"
				},
				IsHidden = false,
				Slot = 1
			}
		}

		};

		var estadoJogo = new EstadoJogo
		{
			NomeTreinador = "Ash",
			Mascotes = new List<Mascote> { mascote }
		};

		// Act
		await servico.SalvarAsync(estadoJogo);

		EstadoJogo resultado = await servico.CarregarAsync();

		// Assert
		Assert.Equal("Ash", resultado.NomeTreinador);
		Assert.Single(resultado.Mascotes);

		Mascote mascoteCarregado = resultado.Mascotes[0];

		Assert.Equal(mascote.Name, mascoteCarregado.Name);
		Assert.Equal(mascote.Height, mascoteCarregado.Height);
		Assert.Equal(mascote.Weight, mascoteCarregado.Weight);
		Assert.Equal(mascote.Alimentacao, mascoteCarregado.Alimentacao);
		Assert.Equal(mascote.Humor, mascoteCarregado.Humor);
		Assert.Equal(mascote.Energia, mascoteCarregado.Energia);
		Assert.Equal(mascote.Amizade, mascoteCarregado.Amizade);

		Assert.Single(mascoteCarregado.Abilities);

		Assert.Equal(
			mascote.Abilities[0].Ability.Name,
			mascoteCarregado.Abilities[0].Ability.Name
		);

		Assert.Equal(
			mascote.Abilities[0].Ability.Url,
			mascoteCarregado.Abilities[0].Ability.Url
		);

		Assert.Equal(
			mascote.Abilities[0].IsHidden,
			mascoteCarregado.Abilities[0].IsHidden
		);

		Assert.Equal(
			mascote.Abilities[0].Slot,
			mascoteCarregado.Abilities[0].Slot
		);
		Assert.Equal(
				mascote.DesgasteAlimentacao,
				mascoteCarregado.DesgasteAlimentacao
		);

		Assert.Equal(
				mascote.DesgasteHumor,
				mascoteCarregado.DesgasteHumor
		);

		Assert.Equal(
				mascote.DesgasteEnergia,
				mascoteCarregado.DesgasteEnergia
		);

		Assert.Equal(
				mascote.UltimaAtualizacaoUtc,
				mascoteCarregado.UltimaAtualizacaoUtc
		);
	}

	[Fact]
	public async Task CarregarAsync_JsonCorrompido_LancaJsonException()
	{
		// Arrange
		string diretorioTemporario = Path.Combine(
			Path.GetTempPath(),
			Guid.NewGuid().ToString()
		);

		Directory.CreateDirectory(diretorioTemporario);

		string caminhoArquivo = Path.Combine(
			diretorioTemporario,
			"jogo.json"
		);

		string jsonCorrompido = "{ JSON INVALIDO";

		await File.WriteAllTextAsync(caminhoArquivo, jsonCorrompido);

		var servico = new ServicoPersistencia(caminhoArquivo);

		// Act
		// Assert
		await Assert.ThrowsAsync<JsonException>(
			() => servico.CarregarAsync()
		);
	}

	[Fact]
	public async Task CarregarAsync_JsonCorrompido_PreservaArquivoOriginal()
	{
		// Arrange
		string diretorioTemporario = Path.Combine(
			Path.GetTempPath(),
			Guid.NewGuid().ToString()
		);

		Directory.CreateDirectory(diretorioTemporario);

		string caminhoArquivo = Path.Combine(
			diretorioTemporario,
			"jogo.json"
		);

		string jsonCorrompido = "{ JSON INVALIDO";

		await File.WriteAllTextAsync(caminhoArquivo, jsonCorrompido);

		var servico = new ServicoPersistencia(caminhoArquivo);

		// Act
		await Assert.ThrowsAsync<JsonException>(
			() => servico.CarregarAsync()
		);

		// Assert
		string conteudoAtual = await File.ReadAllTextAsync(caminhoArquivo);

		Assert.Equal(jsonCorrompido, conteudoAtual);
	}

	[Fact]
	public async Task SalvarAsync_DiretorioInexistente_CriaArquivoERemoveTemporario()
	{
		// Arrange
		string diretorioTemporario = Path.Combine(
			Path.GetTempPath(),
			Guid.NewGuid().ToString()
		);

		string caminhoArquivo = Path.Combine(
			diretorioTemporario,
			"jogo.json"
		);

		var servico = new ServicoPersistencia(caminhoArquivo);

		// Act
		await servico.SalvarAsync(new EstadoJogo());

		// Assert
		Assert.True(Directory.Exists(diretorioTemporario));
		Assert.True(File.Exists(caminhoArquivo));
		Assert.False(File.Exists($"{caminhoArquivo}.tmp"));
	}

	[Fact]
	public async Task SalvarAsync_EstadoJogoNulo_LancaArgumentNullException()
	{
		// Arrange
		var servico = new ServicoPersistencia();

		// Act
		// Assert
		await Assert.ThrowsAsync<ArgumentNullException>(
			() => servico.SalvarAsync(null!)
		);
	}


	[Fact]
	public async Task CarregarAsync_SaveAntigoSemAmizade_InicializaAmizadeEmZero()
	{
		// Arrange
		string diretorioTemporario = Path.Combine(
				Path.GetTempPath(),
				Guid.NewGuid().ToString()
		);

		Directory.CreateDirectory(diretorioTemporario);

		string caminhoArquivo = Path.Combine(
				diretorioTemporario,
				"jogo.json"
		);

		string jsonLegado = """
        {
                "NomeTreinador": "Ash",
                "Mascotes": [
                        {
                                "Name": "pikachu",
                                "Height": 4,
                                "Weight": 60,
                                "Alimentacao": 8,
                                "Humor": 9,
                                "Energia": 7
                        }
                ]
        }
        """;

		await File.WriteAllTextAsync(caminhoArquivo, jsonLegado);

		var servico = new ServicoPersistencia(caminhoArquivo);

		// Act
		EstadoJogo resultado = await servico.CarregarAsync();

		// Assert
		Assert.Equal("Ash", resultado.NomeTreinador);
		Assert.Single(resultado.Mascotes);
		Assert.Equal(0m, resultado.Mascotes[0].Amizade);
	}
}
