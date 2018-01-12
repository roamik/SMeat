using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.MODELS.Profiles {
    public class ChatProfile : Profile {
        public ChatProfile () {
            CreateMap<Chat, ChatDTO>().MaxDepth( 1 );
        }
    }
}
