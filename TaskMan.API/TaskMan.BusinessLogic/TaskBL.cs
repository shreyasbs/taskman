using TaskMan.DataAccess;
using TaskMan.ViewModels;

namespace TaskMan.BusinessLogic
{
    public class TaskBL : ITaskBL
    {
        public List<TaskViewModel> GetAllTasks()
        {
            using (var taskAuth = new TaskRepository())
            {
                var tasks = taskAuth.GetTasks().ToList();
                List<TaskViewModel> taskViewModels = new List<TaskViewModel>();
                foreach (var task in tasks)
                {
                    var taskViewModel = new TaskViewModel();
                    taskViewModel.Id = task.Id;
                    taskViewModel.Title = task.Title;
                    taskViewModel.Description = task.Description;
                    taskViewModel.Status = task.Status.ToString();
                    taskViewModels.Add(taskViewModel);
                }
                return taskViewModels;
            }
        }

        public TaskViewModel GetTask(string id)
        {

            using (var taskAuth = new TaskRepository())
            {
                var task = taskAuth.GetTask(Guid.Parse(id));
                var taskViewModel = new TaskViewModel();
                taskViewModel.Id = task.Id;
                taskViewModel.Title = task.Title;
                taskViewModel.Description = task.Description;
                taskViewModel.Status = task.Status.ToString();
                return taskViewModel;
            }
        }
    }
}
