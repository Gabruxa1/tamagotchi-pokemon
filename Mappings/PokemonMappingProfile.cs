using AutoMapper;
using tamagotchi_pokemon.Models;

namespace tamagotchi_pokemon.Mappings;

public class PokemonMappingProfile : Profile
{
	public PokemonMappingProfile()
	{
		CreateMap<Pokemon, Mascote>()
		.ForMember(dest => dest.Alimentacao, opt => opt.Ignore())
		.ForMember(dest => dest.Humor, opt => opt.Ignore())
		.ForMember(dest => dest.Energia, opt => opt.Ignore())
		.ForMember(dest => dest.Amizade, opt => opt.Ignore())
		.ForMember(dest => dest.DesgasteAlimentacao, opt => opt.Ignore())
		.ForMember(dest => dest.DesgasteHumor, opt => opt.Ignore())
		.ForMember(dest => dest.DesgasteEnergia, opt => opt.Ignore())
		.ForMember(dest => dest.UltimaAtualizacaoUtc, opt => opt.Ignore());
	}
}
