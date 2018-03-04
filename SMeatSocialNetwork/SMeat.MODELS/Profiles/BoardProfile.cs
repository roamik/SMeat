using AutoMapper;
using SMeat.MODELS.BindingModels;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMeat.MODELS.Profiles
{
    class BoardProfile : Profile
    {
        public BoardProfile()
        {
            CreateMap<Board, BoardDTO>().MaxDepth(1);
            CreateMap<BoardCreateBindingModel, Board>().MaxDepth(1);
        }
    }
}
