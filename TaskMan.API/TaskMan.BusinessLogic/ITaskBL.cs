using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMan.ViewModels;

namespace TaskMan.BusinessLogic
{
    public interface ITaskBL
    {
        List<TaskViewModel> GetAllTasks();
        TaskViewModel GetTask(string id);
        void Insert(TaskViewModel taskViewModel);
        void Update(TaskViewModel taskViewModel);
        void DeleteTask(string id);
    }
}
