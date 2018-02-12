using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.MODELS.Profiles {
    public class ChatProfile : Profile {
        public ChatProfile () {
            CreateMap<Chat, ChatDTO>()
                //.ForMember( dest=>dest.Users, opt => opt.MapFrom( src=>src.UserChats.SelectMany(uc=>Mapper.Map<IEnumerable<UserDTO>>(uc.User)).ToList()))
                .MaxDepth(1);
        }
    }
}
