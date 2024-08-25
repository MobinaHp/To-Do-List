using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo
{
    public class Task
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }
        public bool Starred { get; set; }
        public bool Checked { get; set; }
        public DateTime DueDate { get; set; }

        [ForeignKey("List")]
        public int ListId { get; set; }

        public List List { get; set; }
    }
}
