using AutoMapper;
using ToDo.Model.DTO;
using ToDo.Model;

namespace WebApplication1
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<ToDo.List, GetListDto>().ReverseMap();
            CreateMap<ToDo.List, CreateListDto>().ReverseMap();
            CreateMap<ToDo.List, UpdateListDto>().ReverseMap();

            CreateMap<Task, GetTaskDto>().ReverseMap();
            CreateMap<Task, CreateTaskDto>().ReverseMap();
            CreateMap<Task, UpdateTaskDto>().ReverseMap();
        }
    }

}

