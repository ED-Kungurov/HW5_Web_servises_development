using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[Controller]")]
public class TaskController : ControllerBase
{
    private static readonly List<Task> tasks = new();

    [HttpGet]
    public ActionResult<List<Task>> GetTasks()
    {
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public ActionResult<Task> GetTaskById(int id)
    {
        foreach(Task i in tasks)
        {
            if(i.Id == id)
            {
                return Ok(i);
            }
        }
        return NotFound();
    }

    [HttpPost]
    public ActionResult<Task> CreateTask(Task t)
    {
        tasks.Add(t);
        return Ok(t);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateTask(int id, Task u)
    {
        foreach(Task i in tasks)
        {
            if(i.Id == id)
            {
                i.Title = u.Title;
                i.IsCompleted = u.IsCompleted;
                i.Description = u.Description;
                return Ok();
            }
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteTask(int id)
    {
        foreach(Task i in tasks)
        {
            if(i.Id == id)
            {
                tasks.Remove(i);
                return Ok();
            }
        }
        return BadRequest();
    }
}