using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMan.DataAccess
{
    public interface ITaskRepository
    {
        IEnumerable<BusinessObjects.Task> GetTasks();
        BusinessObjects.Task GetTask(Guid id);
        void InsertTask(BusinessObjects.Task task);
        void UpdateTask(BusinessObjects.Task task);
        void DeleteTask(Guid id);
        void SaveChanges();
    }
}
