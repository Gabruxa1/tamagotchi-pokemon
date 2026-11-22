using tamagotchi_pokemon.Services;
using tamagotchi_pokemon.Models;
using Microsoft.Extensions.Time.Testing;

namespace tamagotchi_pokemon.Tests;

public class ServicoTempoTests
{
	[Fact]
	public void Atualizar_TimestampNulo_InicializaRelogioSemDesgaste()
	{
		// Arrange
		var instanteAtual = new DateTimeOffset(
				2026, 11, 22, 12, 0, 0, TimeSpan.Zero
		);

		var relogio = new FakeTimeProvider(instanteAtual);

		var mascote = new Mascote
		{
			Name = "pikachu",
			Alimentacao = 8,
			Humor = 9,
			Energia = 7
		};

		var estadoJogo = new EstadoJogo
		{
			Mascotes = new List<Mascote> { mascote }
		};

		var servico = new ServicoTempo(relogio);

		// Act
		servico.Atualizar(estadoJogo);

		// Assert
		Assert.Equal(instanteAtual, mascote.UltimaAtualizacaoUtc);

		Assert.Equal(8, mascote.Alimentacao);
		Assert.Equal(9, mascote.Humor);
		Assert.Equal(7, mascote.Energia);

		Assert.Equal(0m, mascote.DesgasteAlimentacao);
		Assert.Equal(0m, mascote.DesgasteHumor);
		Assert.Equal(0m, mascote.DesgasteEnergia);
	}

	[Fact]
	public void Atualizar_UmaHoraDecorrida_AcumulaDesgasteFracionario()
	{
		// Arrange
		var instanteInicial = new DateTimeOffset(
				2026, 11, 22, 12, 0, 0, TimeSpan.Zero
		);

		var relogio = new FakeTimeProvider(instanteInicial);

		var mascote = new Mascote
		{
			Name = "pikachu",
			Alimentacao = 8,
			Humor = 9,
			Energia = 7,
			UltimaAtualizacaoUtc = instanteInicial
		};

		var estadoJogo = new EstadoJogo
		{
			Mascotes = new List<Mascote> { mascote }
		};

		var servico = new ServicoTempo(relogio);

		// Act
		relogio.Advance(TimeSpan.FromHours(1));

		servico.Atualizar(estadoJogo);

		// Assert
		Assert.Equal(8, mascote.Alimentacao);
		Assert.Equal(9, mascote.Humor);
		Assert.Equal(7, mascote.Energia);

		Assert.Equal(0.5m, mascote.DesgasteAlimentacao);
		Assert.Equal(0.2m, mascote.DesgasteHumor);
		Assert.Equal(0.4m, mascote.DesgasteEnergia);

		Assert.Equal(relogio.GetUtcNow(), mascote.UltimaAtualizacaoUtc);
	}

	[Fact]
	public void Atualizar_DuasAtualizacoesDeUmaHora_AplicaDesgasteAcumulado()
	{
		// Arrange
		var instanteInicial = new DateTimeOffset(
				2026, 11, 22, 12, 0, 0, TimeSpan.Zero
		);

		var relogio = new FakeTimeProvider(instanteInicial);

		var mascote = new Mascote
		{
			Name = "pikachu",
			Alimentacao = 8,
			Humor = 9,
			Energia = 7,
			UltimaAtualizacaoUtc = instanteInicial
		};

		var estadoJogo = new EstadoJogo
		{
			Mascotes = new List<Mascote> { mascote }
		};

		var servico = new ServicoTempo(relogio);

		// Act
		relogio.Advance(TimeSpan.FromHours(1));
		servico.Atualizar(estadoJogo);

		relogio.Advance(TimeSpan.FromHours(1));
		servico.Atualizar(estadoJogo);

		// Assert
		Assert.Equal(7, mascote.Alimentacao);
		Assert.Equal(9, mascote.Humor);
		Assert.Equal(7, mascote.Energia);

		Assert.Equal(0m, mascote.DesgasteAlimentacao);
		Assert.Equal(0.4m, mascote.DesgasteHumor);
		Assert.Equal(0.8m, mascote.DesgasteEnergia);

		Assert.Equal(
				instanteInicial.AddHours(2),
				mascote.UltimaAtualizacaoUtc
		);
	}

	[Fact]
	public void Atualizar_DesgasteUltrapassaNecessidade_ZeraAtributoEAcumulador()
	{
		// Arrange
		var instanteInicial = new DateTimeOffset(
				2026, 11, 22, 12, 0, 0, TimeSpan.Zero
		);

		var relogio = new FakeTimeProvider(instanteInicial);

		var mascote = new Mascote
		{
			Name = "pikachu",
			Alimentacao = 1,
			Humor = 1,
			Energia = 1,
			UltimaAtualizacaoUtc = instanteInicial
		};

		var estadoJogo = new EstadoJogo
		{
			Mascotes = new List<Mascote> { mascote }
		};

		var servico = new ServicoTempo(relogio);

		// Act
		relogio.Advance(TimeSpan.FromHours(10));

		servico.Atualizar(estadoJogo);

		// Assert
		Assert.Equal(0, mascote.Alimentacao);
		Assert.Equal(0, mascote.Humor);
		Assert.Equal(0, mascote.Energia);

		Assert.Equal(0m, mascote.DesgasteAlimentacao);
		Assert.Equal(0m, mascote.DesgasteHumor);
		Assert.Equal(0m, mascote.DesgasteEnergia);
	}

	[Fact]
	public void Atualizar_NecessidadeJaZerada_NaoAcumulaDesgaste()
	{
		// Arrange
		var instanteInicial = new DateTimeOffset(
				2026, 11, 22, 12, 0, 0, TimeSpan.Zero
		);

		var relogio = new FakeTimeProvider(instanteInicial);

		var mascote = new Mascote
		{
			Name = "pikachu",
			Alimentacao = 0,
			Humor = 5,
			Energia = 0,
			DesgasteAlimentacao = 0m,
			DesgasteHumor = 0.3m,
			DesgasteEnergia = 0m,
			UltimaAtualizacaoUtc = instanteInicial
		};

		var estadoJogo = new EstadoJogo
		{
			Mascotes = new List<Mascote> { mascote }
		};

		var servico = new ServicoTempo(relogio);

		// Act
		relogio.Advance(TimeSpan.FromHours(5));

		servico.Atualizar(estadoJogo);

		// Assert
		Assert.Equal(0, mascote.Alimentacao);
		Assert.Equal(0, mascote.Energia);

		Assert.Equal(0m, mascote.DesgasteAlimentacao);
		Assert.Equal(0m, mascote.DesgasteEnergia);

		Assert.Equal(4, mascote.Humor);
		Assert.Equal(0.3m, mascote.DesgasteHumor);
	}

	[Fact]
	public void Atualizar_MascotesComTimestampsDiferentes_AplicaDesgasteIndividual()
	{
		// Arrange
		var instanteInicial = new DateTimeOffset(
				2026, 11, 22, 12, 0, 0, TimeSpan.Zero
		);

		var relogio = new FakeTimeProvider(instanteInicial);

		var pikachu = new Mascote
		{
			Name = "pikachu",
			Alimentacao = 10,
			Humor = 10,
			Energia = 10,
			UltimaAtualizacaoUtc = instanteInicial
		};

		var estadoJogo = new EstadoJogo
		{
			Mascotes = new List<Mascote> { pikachu }
		};

		var servico = new ServicoTempo(relogio);

		// Act
		relogio.Advance(TimeSpan.FromHours(3));

		var eevee = new Mascote
		{
			Name = "eevee",
			Alimentacao = 10,
			Humor = 10,
			Energia = 10,
			UltimaAtualizacaoUtc = relogio.GetUtcNow()
		};

		estadoJogo.Mascotes.Add(eevee);

		relogio.Advance(TimeSpan.FromHours(2));

		servico.Atualizar(estadoJogo);

		// Assert
		// Pikachu: 5 horas
		Assert.Equal(8, pikachu.Alimentacao);
		Assert.Equal(9, pikachu.Humor);
		Assert.Equal(8, pikachu.Energia);

		Assert.Equal(0.5m, pikachu.DesgasteAlimentacao);
		Assert.Equal(0m, pikachu.DesgasteHumor);
		Assert.Equal(0m, pikachu.DesgasteEnergia);

		// Eevee: 2 horas
		Assert.Equal(9, eevee.Alimentacao);
		Assert.Equal(10, eevee.Humor);
		Assert.Equal(10, eevee.Energia);

		Assert.Equal(0m, eevee.DesgasteAlimentacao);
		Assert.Equal(0.4m, eevee.DesgasteHumor);
		Assert.Equal(0.8m, eevee.DesgasteEnergia);
	}

	[Fact]
	public void Atualizar_RelogioRetrocede_ReinicializaTimestampSemDesgaste()
	{
		// Arrange
		var instanteInicial = new DateTimeOffset(
				2026, 11, 22, 14, 0, 0, TimeSpan.Zero
		);

		var instanteAtual = new DateTimeOffset(
				2026, 11, 22, 12, 0, 0, TimeSpan.Zero
		);

		var relogio = new FakeTimeProvider(instanteAtual);

		var mascote = new Mascote
		{
			Name = "pikachu",
			Alimentacao = 8,
			Humor = 9,
			Energia = 7,
			DesgasteAlimentacao = 0.3m,
			DesgasteHumor = 0.2m,
			DesgasteEnergia = 0.1m,
			UltimaAtualizacaoUtc = instanteInicial
		};

		var estadoJogo = new EstadoJogo
		{
			Mascotes = new List<Mascote> { mascote }
		};

		var servico = new ServicoTempo(relogio);

		// Act
		servico.Atualizar(estadoJogo);

		// Assert
		Assert.Equal(8, mascote.Alimentacao);
		Assert.Equal(9, mascote.Humor);
		Assert.Equal(7, mascote.Energia);

		Assert.Equal(0.3m, mascote.DesgasteAlimentacao);
		Assert.Equal(0.2m, mascote.DesgasteHumor);
		Assert.Equal(0.1m, mascote.DesgasteEnergia);

		Assert.Equal(instanteAtual, mascote.UltimaAtualizacaoUtc);
	}

	[Fact]
	public void Atualizar_EstadoJogoNulo_LancaArgumentNullException()
	{
		// Arrange
		var relogio = new FakeTimeProvider(
				new DateTimeOffset(2026, 11, 22, 12, 0, 0, TimeSpan.Zero)
		);

		var servico = new ServicoTempo(relogio);

		// Act
		var excecao = Assert.Throws<ArgumentNullException>(
				() => servico.Atualizar(null!)
		);

		// Assert
		Assert.Equal("estadoJogo", excecao.ParamName);
	}

	[Fact]
	public void Atualizar_SessentaHorasOffline_AplicaTodoDesgaste()
	{
		// Arrange
		var instanteInicial = new DateTimeOffset(
				2026, 11, 22, 12, 0, 0, TimeSpan.Zero
		);

		var relogio = new FakeTimeProvider(instanteInicial);

		var mascote = new Mascote
		{
			Name = "pikachu",
			Alimentacao = 10,
			Humor = 10,
			Energia = 10,
			UltimaAtualizacaoUtc = instanteInicial
		};

		var estadoJogo = new EstadoJogo
		{
			Mascotes = new List<Mascote> { mascote }
		};

		var servico = new ServicoTempo(relogio);

		// Act
		relogio.Advance(TimeSpan.FromHours(60));

		servico.Atualizar(estadoJogo);

		// Assert
		Assert.Equal(0, mascote.Alimentacao);
		Assert.Equal(0, mascote.Humor);
		Assert.Equal(0, mascote.Energia);

		Assert.Equal(0m, mascote.DesgasteAlimentacao);
		Assert.Equal(0m, mascote.DesgasteHumor);
		Assert.Equal(0m, mascote.DesgasteEnergia);

		Assert.Equal(
				instanteInicial.AddHours(60),
				mascote.UltimaAtualizacaoUtc
		);
	}

	[Fact]
	public void InicializarMascote_DefineTimestampComHorarioDoRelogio()
	{
		// Arrange
		var instanteEsperado = new DateTimeOffset(
				2026, 11, 22, 12, 0, 0, TimeSpan.Zero
		);

		var relogio = new FakeTimeProvider(instanteEsperado);
		var servico = new ServicoTempo(relogio);

		var mascote = new Mascote
		{
			Alimentacao = 8,
			Humor = 7,
			Energia = 9
		};

		// Act
		servico.InicializarMascote(mascote);

		// Assert
		Assert.Equal(instanteEsperado, mascote.UltimaAtualizacaoUtc);
	}
}