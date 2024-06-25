using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMan.BusinessLogic;
using TaskMan.ViewModels;

namespace TaskMan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TasksController : ControllerBase
    {
        private ITaskBL taskBL;
        public TasksController(ITaskBL task)
        {
            taskBL = task;
        }
        [HttpGet]
        [Route("GetTasks")]
        public ActionResult<List<TaskViewModel>> GetTasks()
        {
            var tasks = taskBL.GetAllTasks();
            return Ok(tasks);
        }

        [HttpGet]
        [Route("GetTaskById")]
        public ActionResult GetTask([FromRoute] string id)
        {
            var task = taskBL.GetTask(id);
            return Ok(task);
        }
    }
}
