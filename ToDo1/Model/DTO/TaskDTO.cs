using System;
using System.ComponentModel.DataAnnotations;

namespace ToDo.Model.DTO
{
    public class CreateTaskDto
    {
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public int ListId { get; set; }
        public bool Starred { get; set; }
    }

    public class UpdateTaskDto
    {
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public bool Starred { get; set; }
        public bool Checked { get; set; }
    }

    public class GetTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DueDate { get; set; }
        public bool Starred { get; set; }
    }
}
