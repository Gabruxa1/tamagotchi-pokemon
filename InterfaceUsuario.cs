
namespace tamagotchi_pokemon;

public class InterfaceUsuario
{
	public string BoasVindas()
	{
		Console.Clear();

		Console.WriteLine("========================================");
		Console.WriteLine("          POKÉMON TAMAGOTCHI");
		Console.WriteLine("========================================");
		Console.WriteLine();
		Console.WriteLine("             (\\__/)");
		Console.WriteLine("             (='.'=)");
		Console.WriteLine("             (\")_(\")");
		Console.WriteLine();
		Console.WriteLine("  Um pequeno mundo espera por você!");
		Console.WriteLine();

		Console.Write("Digite seu nome, treinador(a): ");

		string nome = Console.ReadLine() ?? string.Empty;

		while (string.IsNullOrWhiteSpace(nome))
		{
			Console.WriteLine("Você precisa informar um nome!");
			Console.Write("Digite seu nome: ");

			nome = Console.ReadLine() ?? string.Empty;
		}

		Console.WriteLine();
		Console.WriteLine($"Olá, {nome}! Sua jornada está começando!");
		Console.WriteLine();

		return nome.Trim();
	}

	public int ExibirMenuPrincipal(string nomeJogador)
	{
		int opcao;

		while (true)
		{
			Console.Clear();

			Console.WriteLine("========================================");
			Console.WriteLine($"       OLÁ, {nomeJogador.ToUpper()}!");
			Console.WriteLine("========================================");
			Console.WriteLine();
			Console.WriteLine("       O QUE DESEJA FAZER?");
			Console.WriteLine();
			Console.WriteLine("  [1] Adoção de mascotes");
			Console.WriteLine("  [2] Ver mascotes adotados");
			Console.WriteLine("  [3] Sair do jogo");
			Console.WriteLine();
			Console.Write("  Escolha uma opção: ");

			if (!int.TryParse(Console.ReadLine(), out opcao) ||
				opcao < 1 || opcao > 3)
			{
				Console.WriteLine();
				Console.WriteLine("Opção inválida! Tente novamente.");
				Console.WriteLine("Pressione ENTER para continuar...");
				Console.ReadLine();

				continue;
			}

			return opcao;
		}
	}
}
