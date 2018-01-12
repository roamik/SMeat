using AutoMapper;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.MODELS.Profiles {
    public class GroupChatProfile : Profile {
        public GroupChatProfile () {
            CreateMap<GroupChat, GroupChatDTO>().MaxDepth(1);
        }
    }
}