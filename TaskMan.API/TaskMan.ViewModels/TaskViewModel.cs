using System.ComponentModel.DataAnnotations;

namespace TaskMan.ViewModels
{
    public class TaskViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public Guid UserId { get; set; }
    }
}
