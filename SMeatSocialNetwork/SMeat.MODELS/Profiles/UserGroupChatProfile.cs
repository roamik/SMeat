using AutoMapper;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.MODELS.Profiles {
    public class UserGroupChatProfile : Profile {
        public UserGroupChatProfile () {
            CreateMap<UserGroupChat, UserGroupChatDTO>().MaxDepth(1);
        }
    }
}