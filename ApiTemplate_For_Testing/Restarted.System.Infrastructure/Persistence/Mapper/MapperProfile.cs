using AutoMapper;
using Restarted.System.Infrastructure.Persistence.Entities;
using System.Runtime.CompilerServices;

namespace Restarted.System.Infrastructure.Persistence.Mapper
{
    public partial class MapperProfile : Profile
    {
        public partial void GeneratedCodeMapping() ;
        public MapperProfile()
        {



            KnownTypeMapping();

            GeneratedCodeMapping();

        }

        void KnownTypeMapping()
        {
            //CreateMap<AddressDTO, Address>().ReverseMap();
            //CreateMap<ContactDTO, Contact>().ReverseMap();
            //// CreateMap<AttributeMasterDTO, AttributeMaster>().ReverseMap();
            //CreateMap<UserDTO, User>().ReverseMap();
            ////CreateMap<UserAuthDTO, User>().ReverseMap();
            ////CreateMap<UserAttributeDTO, UserAttribute>().ReverseMap();

            //CreateMap<RoleDTO, Role>().ReverseMap();

            //CreateMap<User, UserListDTO>()
            //   .ForMember(destination => destination.Address, Source => Source.MapFrom(d => d.Address!=null ? d.Address.Line1 : null))
            //   .ForMember(destination => destination.Email, Source => Source.MapFrom(d => d.Contact!= null ? d.Contact.PrimaryEmail : null))
            //   .ForMember(destination => destination.Phone, Source => Source.MapFrom(d => d.Contact != null ? d.Contact.PrimaryPhone : null));

            //CreateMap<Organization, OrganizationDTO>().ReverseMap();

            //CreateMap<Organization, OrganizationListDTO>()
            //   .ForMember(destination => destination.Address, Source => Source.MapFrom(d => d.Address!=null ? d.Address.Line1 : null))
            //   .ForMember(destination => destination.Email, Source => Source.MapFrom(d => d.Contact!= null ? d.Contact.PrimaryEmail : null))
            //   .ForMember(destination => destination.Phone, Source => Source.MapFrom(d => d.Contact != null ? d.Contact.PrimaryPhone : null));
        }
    }

}




















    