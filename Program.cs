using tamagotchi_pokemon;
using tamagotchi_pokemon.Models;

var interfaceUsuario = new InterfaceUsuario();

List<Mascote> mascotesAdotados = new();

var servicoPokemon = new ServicoPokemon();

string nomeJogador = interfaceUsuario.BoasVindas();

int opcao;

do
{
	opcao = interfaceUsuario.ExibirMenuPrincipal(nomeJogador);

	switch (opcao)
	{
		case 1:
			try
			{
				Console.WriteLine("Consultando catálogo de Pokémon...");

				var especies = await servicoPokemon.ListarEspeciesAsync();

				int? idSelecionado = interfaceUsuario.SelecionarEspecie(especies);

				if (idSelecionado == null)
				{
					break;
				}

				Mascote? mascote = await servicoPokemon.ObterPokemonAsync(
					idSelecionado.Value
				);

				if (mascote == null)
				{
					Console.WriteLine("Não foi possível consultar este Pokémon.");
					break;
				}

				if (interfaceUsuario.DesejaVerDetalhes())
				{
					interfaceUsuario.ExibirDetalhesPokemon(mascote);
				}

				if (interfaceUsuario.ConfirmarAdocao(mascote.Name))
				{
					mascotesAdotados.Add(mascote);

					Console.WriteLine();
					Console.WriteLine($"{mascote.Name.ToUpper()} foi adotado com sucesso!");
				}
				else
				{
					Console.WriteLine();
					Console.WriteLine("Adoção cancelada.");
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Erro durante a adoção: {ex.Message}");
			}

			break;

		case 2:
			interfaceUsuario.ExibirMascotesAdotados(mascotesAdotados);
			break;

		case 3:
			Console.WriteLine($"Até a proxima, {nomeJogador}!");
			break;
	}

	if (opcao != 3)
	{
		Console.WriteLine();
		Console.WriteLine("Pressione ENTER para continuar...");
		Console.ReadLine();
	}
} while (opcao != 3);

return;