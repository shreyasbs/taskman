using TaskMan.DataAccess;
using TaskMan.ViewModels;

namespace TaskMan.BusinessLogic
{
    public class TaskBL : ITaskBL
    {
        private readonly ITaskRepository _taskRepository;
        public TaskBL(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public List<TaskViewModel> GetAllTasks()
        {

            var tasks = _taskRepository.GetTasks().ToList();
            List<TaskViewModel> taskViewModels = new List<TaskViewModel>();
            foreach (var task in tasks)
            {
                var taskViewModel = new TaskViewModel();
                taskViewModel.Id = task.Id.ToString();
                taskViewModel.Title = task.Title;
                taskViewModel.Description = task.Description;
                taskViewModel.Status = task.Status.ToString();
                taskViewModels.Add(taskViewModel);
            }
            return taskViewModels;

        }

        public TaskViewModel GetTask(string id)
        {
            var task = _taskRepository.GetTask(Guid.Parse(id));
            var taskViewModel = new TaskViewModel();
            taskViewModel.Id = task.Id.ToString();
            taskViewModel.Title = task.Title;
            taskViewModel.Description = task.Description;
            taskViewModel.Status = task.Status.ToString();
            return taskViewModel;
        }

        public void Insert(TaskViewModel taskViewModel)
        {
            _taskRepository.InsertTask(taskViewModel);
        }

        public void Update(TaskViewModel taskViewModel)
        {
            _taskRepository.UpdateTask(taskViewModel);
        }

        public void DeleteTask(string id)
        {
            _taskRepository.DeleteTask(Guid.Parse(id));
        }
    }
}
