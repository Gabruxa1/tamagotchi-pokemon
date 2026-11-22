using tamagotchi_pokemon.Models;

namespace tamagotchi_pokemon.Services;

public class ServicoTempo
{
	private readonly TimeProvider _relogio;

	public ServicoTempo(TimeProvider? relogio = null)
	{
		_relogio = relogio ?? TimeProvider.System;
	}

	public void InicializarMascote(Mascote mascote)
	{
		ArgumentNullException.ThrowIfNull(mascote);

		mascote.UltimaAtualizacaoUtc = _relogio.GetUtcNow();
	}

	public void Atualizar(EstadoJogo estadoJogo)
	{
		ArgumentNullException.ThrowIfNull(estadoJogo);

		DateTimeOffset instanteAtual = _relogio.GetUtcNow();

		foreach (Mascote mascote in estadoJogo.Mascotes)
		{
			if (mascote.UltimaAtualizacaoUtc is null)
			{
				mascote.UltimaAtualizacaoUtc = instanteAtual;
				continue;
			}

			TimeSpan tempoDecorrido = instanteAtual - mascote.UltimaAtualizacaoUtc.Value;

			if (tempoDecorrido < TimeSpan.Zero)
			{
				mascote.UltimaAtualizacaoUtc = instanteAtual;
				continue;
			}

			decimal horasDecorridas =
					(decimal)tempoDecorrido.Ticks / TimeSpan.TicksPerHour;

			AplicarDesgaste(
					mascote,
					horasDecorridas,
					0.5m,
					0.2m,
					0.4m
			);

			mascote.UltimaAtualizacaoUtc = instanteAtual;
		}
	}
	private static void AplicarDesgaste(
		Mascote mascote,
		decimal horasDecorridas,
		decimal taxaAlimentacao,
		decimal taxaHumor,
		decimal taxaEnergia)
	{
		// Alimentação
		if (mascote.Alimentacao > 0)
		{
			decimal desgaste = (taxaAlimentacao * horasDecorridas)
					+ mascote.DesgasteAlimentacao;

			int reducao = (int)decimal.Floor(desgaste);

			mascote.Alimentacao = Math.Max(0, mascote.Alimentacao - reducao);

			mascote.DesgasteAlimentacao = mascote.Alimentacao > 0
					? desgaste - reducao
					: 0m;
		}

		// Humor
		if (mascote.Humor > 0)
		{
			decimal desgaste = (taxaHumor * horasDecorridas)
					+ mascote.DesgasteHumor;

			int reducao = (int)decimal.Floor(desgaste);

			mascote.Humor = Math.Max(0, mascote.Humor - reducao);

			mascote.DesgasteHumor = mascote.Humor > 0
					? desgaste - reducao
					: 0m;
		}

		// Energia
		if (mascote.Energia > 0)
		{
			decimal desgaste = (taxaEnergia * horasDecorridas)
					+ mascote.DesgasteEnergia;

			int reducao = (int)decimal.Floor(desgaste);

			mascote.Energia = Math.Max(0, mascote.Energia - reducao);

			mascote.DesgasteEnergia = mascote.Energia > 0
					? desgaste - reducao
					: 0m;
		}
	}
}