using AutoMapper;
using SMeat.MODELS.BindingModels;
using SMeat.MODELS.DTO;
using SMeat.MODELS.Entities;

namespace SMeat.MODELS.Profiles {
    public class WorkplaceProfile : Profile {
        public WorkplaceProfile () {
            CreateMap<Workplace, WorkplaceDTO>().MaxDepth(1);
            CreateMap<WorkplaceCreateBindingModel, Workplace>().MaxDepth(1);
        }
    }
}