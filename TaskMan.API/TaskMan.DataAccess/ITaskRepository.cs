using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMan.ViewModels;

namespace TaskMan.DataAccess
{
    public interface ITaskRepository
    {
        IEnumerable<BusinessObjects.Task> GetTasks();
        BusinessObjects.Task GetTask(Guid id);
        void InsertTask(TaskViewModel taskViewModel);
        void UpdateTask(TaskViewModel taskViewModel);
        void DeleteTask(Guid id);
        void SaveChanges();
    }
}
