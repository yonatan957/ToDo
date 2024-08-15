using System.ComponentModel.DataAnnotations;

namespace ToDo.Models
{
    public class ToDoModel
    {
        public int Id { get; set; }
        public string todo { get; set; }
        public bool Completed { get; set; }
        public int userid { get; set; }
    }
}
