using AutoMapper;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.MODELS.Profiles {
    public class ContactsProfile : Profile {
        public ContactsProfile () {
            CreateMap<Contacts, ContactsDTO>().MaxDepth(1);
        }
    }
}