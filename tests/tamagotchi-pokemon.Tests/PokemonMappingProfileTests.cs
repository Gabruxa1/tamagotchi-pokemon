using AutoMapper;
using tamagotchi_pokemon.Mappings;
using Microsoft.Extensions.Logging.Abstractions;

namespace tamagotchi_pokemon.Tests;

public class PokemonMappingProfileTests
{
	[Fact]
	public void Configuracao_ValidarMapeamento_NaoPossuiMembrosNaoMapeados()
	{
		// Arrange
		var configuration = new MapperConfiguration(cfg =>
		{
			cfg.AddProfile<PokemonMappingProfile>();
		}, NullLoggerFactory.Instance);

		// Act
		configuration.AssertConfigurationIsValid();

		// Assert
		Assert.NotNull(configuration.CreateMapper());
	}
}
