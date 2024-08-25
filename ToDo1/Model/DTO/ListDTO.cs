using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Model.DTO
{
    public class CreateListDto
    {
        public string Name { get; set; }
        public List<CreateTaskDto> Tasks { get; set; }
    }

    public class UpdateListDto
    {
        public string Name { get; set; }
    }

    public class GetListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GetTaskDto> Tasks { get; set; }
    }
}
