using tamagotchi_pokemon.Models;
using tamagotchi_pokemon.Services;
using tamagotchi_pokemon.Views;
using AutoMapper;
using tamagotchi_pokemon.Mappings;
using Microsoft.Extensions.Logging.Abstractions;
using System.Text.Json;

namespace tamagotchi_pokemon.Controllers;

public class TamagotchiController
{
	private readonly InterfaceUsuario _interfaceUsuario;
	private readonly ServicoPokemon _servicoPokemon;
	private readonly IMapper _mapper;
	private EstadoJogo _estadoJogo = new();
	private readonly ServicoPersistencia _servicoPersistencia;


	public TamagotchiController()
	{
		_interfaceUsuario = new InterfaceUsuario();
		_servicoPokemon = new ServicoPokemon();
		_servicoPersistencia = new ServicoPersistencia();

		var configuration = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile<PokemonMappingProfile>();
		}, NullLoggerFactory.Instance);

		_mapper = configuration.CreateMapper();
		_mapper.ConfigurationProvider.AssertConfigurationIsValid();

	}


	public async Task Jogar()
	{
		try
		{
			_estadoJogo = await _servicoPersistencia.CarregarAsync();
		}
		catch (JsonException)
		{
			Console.WriteLine("Erro: o arquivo de persistência está corrompido.");
			Console.WriteLine("Os dados existentes foram preservados.");
			return;
		}

		_interfaceUsuario.ExibirBoasVindas();

		if (string.IsNullOrWhiteSpace(_estadoJogo.NomeTreinador))
		{
			_estadoJogo.NomeTreinador =
				_interfaceUsuario.SolicitarNomeTreinador();

			await _servicoPersistencia.SalvarAsync(_estadoJogo);
		}

		string nomeJogador = _estadoJogo.NomeTreinador;

		int opcao;

		do
		{
			opcao = _interfaceUsuario.ExibirMenuPrincipal(nomeJogador);

			switch (opcao)
			{
				case 1:
					await AdotarPokemon();
					break;

				case 2:
					_interfaceUsuario.ExibirMascotesAdotados(
						_estadoJogo.Mascotes
					);
					break;

				case 3:
					await InteragirComMascote();
					break;

				case 4:
					Console.WriteLine($"Até a próxima, {nomeJogador}!");
					break;
			}

			if (opcao != 4)
			{
				Console.WriteLine();
				Console.WriteLine("Pressione ENTER para continuar...");
				Console.ReadLine();
			}

		} while (opcao != 4);
	}

	private async Task AdotarPokemon()
	{
		try
		{
			Console.WriteLine("Consultando catálogo de Pokémon...");

			var especies = await _servicoPokemon.ListarEspeciesAsync();

			int? idSelecionado = _interfaceUsuario.SelecionarEspecie(especies);

			if (idSelecionado == null)
			{
				return;
			}

			Pokemon? pokemon = await _servicoPokemon.ObterPokemonAsync(
				idSelecionado.Value
			);

			if (pokemon == null)
			{
				Console.WriteLine("Não foi possível consultar este Pokémon.");
				return;
			}

			Mascote mascote = _mapper.Map<Mascote>(pokemon);

			if (_interfaceUsuario.DesejaVerDetalhes())
			{
				_interfaceUsuario.ExibirDetalhesPokemon(mascote);
			}

			if (_interfaceUsuario.ConfirmarAdocao(mascote.Name))
			{
				_estadoJogo.Mascotes.Add(mascote);

				await _servicoPersistencia.SalvarAsync(_estadoJogo);

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
	}

	private async Task InteragirComMascote()
	{
		int? indiceSelecionado = _interfaceUsuario.SelecionarMascote(
				_estadoJogo.Mascotes
		);

		if (indiceSelecionado == null)
		{
			return;
		}

		Mascote mascote = _estadoJogo.Mascotes[indiceSelecionado.Value];

		int opcao;

		do
		{

			opcao = _interfaceUsuario.ExibirMenuInteracao(mascote);
			bool houveAlteracao = false;

			switch (opcao)
			{
				case 1:
					_interfaceUsuario.ExibirStatusMascote(mascote);
					break;

				case 2:
					if (mascote.Brincar())
					{
						houveAlteracao = true;
						Console.WriteLine($"{mascote.Name} brincou e se divertiu!");
					}
					else
					{
						Console.WriteLine(
								$"{mascote.Name} está sem energia ou com fome e não pode brincar!"
						);
					}
					break;

				case 3:
					if (mascote.Alimentar())
					{
						houveAlteracao = true;
						Console.WriteLine($"{mascote.Name} recebeu comida!");
					}
					else
					{
						Console.WriteLine($"{mascote.Name} já está completamente alimentado!");
					}
					break;

				case 4:
					if (mascote.Dormir())
					{
						houveAlteracao = true;
						Console.WriteLine($"{mascote.Name} foi dormir!");
					}
					else
					{
						Console.WriteLine($"{mascote.Name} já está completamente descansado!");
					}
					break;
			}

			if (houveAlteracao)
			{
				await _servicoPersistencia.SalvarAsync(_estadoJogo);
			}

			if (opcao != 5)
			{
				Console.WriteLine();
				Console.WriteLine("Pressione ENTER para continuar...");
				Console.ReadLine();
			}

		} while (opcao != 5);
	}
}
