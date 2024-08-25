using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Model
{
    public class Task
    {
        public int Id { get; private set; }
        public string Name { get; set; }
        public bool Starred { get; set; }
        public bool Checked { get; set; }
        public DateTime DueDate { get; set; }
        public int ListId { get; set; }

    }
}
