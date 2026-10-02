using Microsoft.AspNetCore.Mvc;

public class ClientController : Controller
{
    private readonly IHttpClientFactory _factory;

    public ClientController(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    [HttpGet("")]
    [HttpGet("client")]
    public async System.Threading.Tasks.Task<IActionResult> Index()
    {
        var client = _factory.CreateClient("TaskApi");
        var tasks = await client.GetFromJsonAsync<List<Task>>("api/Task");
        return View(tasks);
    }

    [HttpGet("client/details")]
    public async System.Threading.Tasks.Task<IActionResult> Details(int id)
    {
        var client = _factory.CreateClient("TaskApi");
        var response = await client.GetAsync($"api/Task/{id}");
        if (response.IsSuccessStatusCode)
        {
            ViewBag.FoundTask = await response.Content.ReadFromJsonAsync<Task>();
        }
        else
        {
            ViewBag.NotFound = true;
        }

        var tasks = await client.GetFromJsonAsync<List<Task>>("api/Task");
        return View("Index", tasks);
    }

    [HttpPost("client/create")]
    public async System.Threading.Tasks.Task<IActionResult> Create(Task t)
    {
        var client = _factory.CreateClient("TaskApi");
        await client.PostAsJsonAsync("api/Task", t);
        return RedirectToAction("Index");
    }

    [HttpPost("client/update")]
    public async System.Threading.Tasks.Task<IActionResult> Update(Task t)
    {
        var client = _factory.CreateClient("TaskApi");
        await client.PutAsJsonAsync($"api/Task/{t.Id}", t);
        return RedirectToAction("Index");
    }

    [HttpPost("client/delete/{id}")]
    public async System.Threading.Tasks.Task<IActionResult> Delete(int id)
    {
        var client = _factory.CreateClient("TaskApi");
        await client.DeleteAsync($"api/Task/{id}");
        return RedirectToAction("Index");
    }
}
