namespace tamagotchi_pokemon.Models;

public class EstadoJogo
{
	public string NomeTreinador { get; set; } = string.Empty;

	public List<Mascote> Mascotes { get; set; } = new();
}
