using AutoMapper;
using PeraInvest.API.Commands.Handlers;
using PeraInvest.Domain.CarteiraAggregate;

namespace PeraInvest.API.Controllers.Mappers {

    public class MappingProfile : Profile {
        public MappingProfile() {
            CreateMap<AtivoFinanceiro, CriarAtivoFinanceiroResponse>()
                .ForMember(dest => dest.Id, opt => opt.ConvertUsing(new GuidConverter()));
        }
    }

    public class GuidConverter : IValueConverter<byte[], string> {
        public string Convert(byte[] source, ResolutionContext context) => new Guid(source).ToString();
    }
}

