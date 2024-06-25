using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMan.DataAccess
{
    public class TaskRepository : ITaskRepository, IDisposable
    {


        public void DeleteTask(Guid id)
        {
            using (var taskDataContext = new TaskDataContext())
            {
                var task = taskDataContext.Tasks.FirstOrDefault(x => x.Id == id);
                taskDataContext.Tasks.Remove(task);
            }

        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public BusinessObjects.Task GetTask(Guid id)
        {
            using (var taskDataContext = new TaskDataContext())
            {
                var task = taskDataContext.Tasks.FirstOrDefault(x => x.Id == id);
                return task;
            }

        }

        public  IEnumerable<BusinessObjects.Task> GetTasks()
        {
            using (var taskDataContext = new TaskDataContext())
            {
                return taskDataContext.Tasks.ToList();
            }
        }

        public void InsertTask(BusinessObjects.Task task)
        {
            using (var taskDataContext = new TaskDataContext())
            {
                taskDataContext.Tasks.Add(task);
            }
        }

        public void SaveChanges()
        {
            using (var taskDataContext = new TaskDataContext())
            {
                taskDataContext.SaveChanges();
            }
        }

        public void UpdateTask(BusinessObjects.Task task)
        {
            using (var taskDataContext = new TaskDataContext())
            {
                taskDataContext.Entry(task).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            }
        }
    }
}
