using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMan.ViewModels;

namespace TaskMan.DataAccess
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDataContext taskDataContext;
        public TaskRepository(TaskDataContext _taskDataContext)
        {
            taskDataContext = _taskDataContext;
        }   

        public BusinessObjects.Task GetTask(Guid id)
        {

            var task = taskDataContext.Tasks.FirstOrDefault(x => x.Id == id);
            return task;

        }

        public IEnumerable<BusinessObjects.Task> GetTasks()
        {

            return taskDataContext.Tasks.ToList();
        }

        public void InsertTask(TaskViewModel taskViewModel)
        {
            var task = new BusinessObjects.Task();
            task.Title = taskViewModel.Title;
            task.Description = taskViewModel.Description;
            task.Status = (BusinessObjects.TaskStatus) Enum.Parse(typeof(BusinessObjects.TaskStatus), taskViewModel.Status);
            taskDataContext.Tasks.Add(task);
            SaveChanges();
        }

        public void SaveChanges()
        {

            taskDataContext.SaveChanges();
        }

        public void UpdateTask(TaskViewModel taskViewModel)
        {
            var task = taskDataContext.Tasks.FirstOrDefault(x=>x.Id == Guid.Parse(taskViewModel.Id));
            if(task != null)
            {
                task.Title = taskViewModel.Title;
                task.Description = taskViewModel.Description;
                task.Status = (BusinessObjects.TaskStatus)Enum.Parse(typeof(BusinessObjects.TaskStatus), taskViewModel.Status);
                taskDataContext.Entry(task).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                SaveChanges();
            }

            
        }

        public void DeleteTask(Guid guid)
        {
            var task = taskDataContext.Tasks.FirstOrDefault(x =>  x.Id == guid);
            if(task != null)
            {
                taskDataContext.Remove(task);
                SaveChanges();
            }
        }
    }
}
