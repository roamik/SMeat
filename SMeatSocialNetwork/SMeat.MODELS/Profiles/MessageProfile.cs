using AutoMapper;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.MODELS.Profiles {
    public class MessageProfile : Profile {
        public MessageProfile () {
            CreateMap<Message, MessageDTO>().MaxDepth(1);
            CreateMap<MessageDTO, Message>().MaxDepth(1);
        }
    }
}