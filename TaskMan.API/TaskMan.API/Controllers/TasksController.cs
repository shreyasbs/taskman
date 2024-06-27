using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskMan.BusinessLogic;
using TaskMan.ViewModels;

namespace TaskMan.API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]

    public class TasksController : ControllerBase
    {
        private ITaskBL taskBL;
        public TasksController(ITaskBL task)
        {
            taskBL = task;
        }
        [HttpGet]
        [Route("GetTasks")]
        [Authorize]
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

        [HttpGet]
        [Route("GetTaskStatus")]
        public ActionResult GetTaskStatus()
        {
            var taskStatus = Enum.GetNames(typeof(BusinessObjects.TaskStatus)).ToList();
            return Ok(taskStatus);
        }

        [HttpPost]
        [Route("PostTask")]
        public ActionResult PostTask([FromBody] TaskViewModel taskViewModel)
        {
            if (ModelState.IsValid)
            {
                if (taskViewModel == null) { return BadRequest(ModelState); }
                if (String.IsNullOrEmpty(taskViewModel.Id))
                {
                    taskBL.Insert(taskViewModel);
                }
                else
                {
                    taskBL.Update(taskViewModel);
                }

                return Ok();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete]
        [Route("DeleteTask")]
        public ActionResult DeleteTask(string id)
        {
            taskBL.DeleteTask(id);
            return Ok();
        }
    }
}
