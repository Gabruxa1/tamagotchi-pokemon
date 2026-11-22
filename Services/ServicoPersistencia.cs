using System.Text.Json;
using tamagotchi_pokemon.Models;

namespace tamagotchi_pokemon.Services;

public class ServicoPersistencia
{
	private readonly string _caminhoArquivo;

	public ServicoPersistencia(string? caminhoArquivo = null)
	{
		_caminhoArquivo = caminhoArquivo ?? Path.Combine(
			Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
			"tamagotchi-pokemon",
			"jogo.json"
		);
	}

	public async Task<EstadoJogo> CarregarAsync()
	{
		if (!File.Exists(_caminhoArquivo))
		{
			return new EstadoJogo();
		}

		string json = await File.ReadAllTextAsync(_caminhoArquivo);

		EstadoJogo? estadoJogo = JsonSerializer.Deserialize<EstadoJogo>(json);

		return estadoJogo ?? new EstadoJogo();
	}

	public async Task SalvarAsync(EstadoJogo estadoJogo)
	{
		ArgumentNullException.ThrowIfNull(estadoJogo);

		string? diretorio = Path.GetDirectoryName(_caminhoArquivo);

		if (diretorio != null)
		{
			Directory.CreateDirectory(diretorio);
		}

		string json = JsonSerializer.Serialize(
			estadoJogo,
			new JsonSerializerOptions
			{
				WriteIndented = true
			}
		);

		string caminhoTemporario = $"{_caminhoArquivo}.tmp";

		try
		{
			await File.WriteAllTextAsync(caminhoTemporario, json);

			File.Move(caminhoTemporario, _caminhoArquivo, true);
		}
		finally
		{
			if (File.Exists(caminhoTemporario))
			{
				File.Delete(caminhoTemporario);
			}
		}
	}
}
