using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDo.Model
{
    public class List
    {
        //private static int _lastId = 0;

        public string Name { get; set; }
        public int Id { get; private set; }
        public ObservableCollection<Task> Tasks { get; set; }

        public List()
        {

            Tasks = new ObservableCollection<Task>();
        }
    }
}
