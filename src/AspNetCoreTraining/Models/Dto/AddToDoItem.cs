using System.ComponentModel.DataAnnotations;

namespace AspNetCoreTraining.Models.Dto
{
    public class AddToDoItem
    {
        [Required]
        public string Title { get; set; }
    }
}