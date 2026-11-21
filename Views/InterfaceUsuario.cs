using tamagotchi_pokemon.Models;

namespace tamagotchi_pokemon.Views;

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
			Console.WriteLine("  [3] Interagir com mascote");
			Console.WriteLine("  [4] Sair do jogo");
			Console.WriteLine();
			Console.Write("  Escolha uma opção: ");

			if (!int.TryParse(Console.ReadLine(), out opcao) ||
				opcao < 1 || opcao > 4)
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

	public int? SelecionarEspecie(List<EspeciePokemon> especies)
	{
		int escolha;

		while (true)
		{
			Console.Clear();

			Console.WriteLine("========================================");
			Console.WriteLine("          ADOÇÃO DE MASCOTES");
			Console.WriteLine("========================================");
			Console.WriteLine();

			foreach (var especie in especies)
			{
				Console.WriteLine($"{especie.Id} - {especie.Nome.ToUpper()}");
			}

			Console.WriteLine();
			Console.WriteLine("[0] Voltar ao menu principal");
			Console.WriteLine();
			Console.Write("Digite o ID da espécie desejada: ");

			if (!int.TryParse(Console.ReadLine(), out escolha))
			{
				Console.WriteLine("Entrada inválida!");
				Console.WriteLine("Pressione ENTER para continuar...");
				Console.ReadLine();
				continue;
			}

			if (escolha == 0)
			{
				return null;
			}

			if (!especies.Any(e => e.Id == escolha))
			{
				Console.WriteLine("Espécie não encontrada!");
				Console.WriteLine("Pressione ENTER para continuar...");
				Console.ReadLine();
				continue;
			}

			return escolha;
		}
	}

	public void ExibirDetalhesPokemon(Mascote mascote)
	{
		Console.Clear();

		Console.WriteLine("========================================");
		Console.WriteLine("          DETALHES DO MASCOTE");
		Console.WriteLine("========================================");
		Console.WriteLine();

		Console.WriteLine($"Nome: {mascote.Name.ToUpper()}");
		Console.WriteLine($"Altura: {mascote.Height}");
		Console.WriteLine($"Peso: {mascote.Weight}");

		Console.WriteLine();
		Console.WriteLine("Habilidades:");

		foreach (var habilidade in mascote.Abilities)
		{
			Console.WriteLine($"- {habilidade.Ability.Name.ToUpper()}");
		}

		Console.WriteLine();
	}

	public void ExibirStatusMascote(Mascote mascote)
	{
		Console.Clear();

		Console.WriteLine("========================================");
		Console.WriteLine("          ESTADO DO MASCOTE");
		Console.WriteLine("========================================");
		Console.WriteLine();

		string estadoAlimentacao = mascote.Alimentacao switch
		{
			<= 2 => "Muita fome",
			<= 5 => "Precisa comer",
			<= 8 => "Bem alimentado",
			_ => "Satisfeito"
		};

		string estadoHumor = mascote.Humor switch
		{
			<= 2 => "Muito triste",
			<= 5 => "Desanimado",
			<= 8 => "Feliz",
			_ => "Muito alegre"
		};

		string estadoEnergia = mascote.Energia switch
		{
			<= 2 => "Exausto",
			<= 5 => "Cansado",
			<= 8 => "Disposto",
			_ => "Descansado"
		};

		Console.WriteLine($"Mascote: {mascote.Name.ToUpper()}");
		Console.WriteLine();

		Console.WriteLine($"Alimentação: {mascote.Alimentacao}/10 - {estadoAlimentacao}");
		Console.WriteLine($"Humor:       {mascote.Humor}/10 - {estadoHumor}");
		Console.WriteLine($"Energia:     {mascote.Energia}/10 - {estadoEnergia}");

		Console.WriteLine();
	}

	public bool DesejaVerDetalhes()
	{
		while (true)
		{
			Console.WriteLine();
			Console.WriteLine("Deseja conhecer as características deste Pokémon?");
			Console.WriteLine("[1] Sim, mostrar detalhes");
			Console.WriteLine("[2] Não, quero continuar");
			Console.Write("Sua escolha: ");

			string resposta = Console.ReadLine() ?? string.Empty;

			if (resposta == "1")
			{
				return true;
			}

			if (resposta == "2")
			{
				return false;
			}

			Console.WriteLine("Opção inválida! Escolha 1 ou 2.");
		}
	}
	public bool ConfirmarAdocao(string nomePokemon)
	{
		string resposta;

		while (true)
		{
			Console.WriteLine();
			Console.WriteLine("========================================");
			Console.WriteLine($"Deseja adotar {nomePokemon.ToUpper()}?");
			Console.WriteLine("========================================");
			Console.WriteLine("[1] Sim, quero adotar!");
			Console.WriteLine("[2] Não, cancelar adoção");
			Console.WriteLine();
			Console.Write("Sua escolha: ");

			resposta = Console.ReadLine() ?? string.Empty;

			if (resposta == "1")
			{
				return true;
			}

			if (resposta == "2")
			{
				return false;
			}

			Console.WriteLine("Opção inválida! Escolha 1 ou 2.");
		}
	}
	public void ExibirMascotesAdotados(List<Mascote> mascotesAdotados)
	{
		Console.Clear();

		Console.WriteLine("========================================");
		Console.WriteLine("          MEUS MASCOTES");
		Console.WriteLine("========================================");
		Console.WriteLine();

		if (!mascotesAdotados.Any())
		{
			Console.WriteLine("Você ainda não adotou nenhum Pokémon.");
			return;
		}

		for (int i = 0; i < mascotesAdotados.Count; i++)
		{
			var mascote = mascotesAdotados[i];

			Console.WriteLine($"{i + 1}. {mascote.Name.ToUpper()}");
			Console.WriteLine($"   Altura: {mascote.Height}");
			Console.WriteLine($"   Peso: {mascote.Weight}");
			Console.WriteLine();
		}

		Console.WriteLine($"Total de mascotes adotados: {mascotesAdotados.Count}");
	}

	public int? SelecionarMascote(List<Mascote> mascotesAdotados)
	{
		if (!mascotesAdotados.Any())
		{
			Console.WriteLine("Você ainda não adotou nenhum Pokémon.");
			return null;
		}

		while (true)
		{
			Console.Clear();

			Console.WriteLine("========================================");
			Console.WriteLine("       SELECIONE SEU MASCOTE");
			Console.WriteLine("========================================");
			Console.WriteLine();

			for (int i = 0; i < mascotesAdotados.Count; i++)
			{
				Console.WriteLine($"[{i + 1}] {mascotesAdotados[i].Name.ToUpper()}");
			}

			Console.WriteLine();
			Console.WriteLine("[0] Voltar");
			Console.WriteLine();
			Console.Write("Escolha um mascote: ");

			if (!int.TryParse(Console.ReadLine(), out int escolha))
			{
				Console.WriteLine("Entrada inválida!");
				Console.ReadLine();
				continue;
			}

			if (escolha == 0)
			{
				return null;
			}

			if (escolha < 1 || escolha > mascotesAdotados.Count)
			{
				Console.WriteLine("Mascote não encontrado!");
				Console.ReadLine();
				continue;
			}

			return escolha - 1;
		}
	}

	public int ExibirMenuInteracao(Mascote mascote)
	{
		int opcao;

		while (true)
		{
			Console.Clear();

			Console.WriteLine("========================================");
			Console.WriteLine($"       INTERAGINDO COM {mascote.Name.ToUpper()}");
			Console.WriteLine("========================================");
			Console.WriteLine();
			Console.WriteLine("[1] Consultar estado");
			Console.WriteLine("[2] Brincar");
			Console.WriteLine("[3] Alimentar");
			Console.WriteLine("[4] Dormir");
			Console.WriteLine("[5] Voltar");
			Console.WriteLine();
			Console.Write("Escolha uma opção: ");

			if (!int.TryParse(Console.ReadLine(), out opcao) ||
					opcao < 1 || opcao > 5)
			{
				Console.WriteLine();
				Console.WriteLine("Opção inválida! Tente novamente.");
				Console.ReadLine();
				continue;
			}

			return opcao;
		}
	}
}
