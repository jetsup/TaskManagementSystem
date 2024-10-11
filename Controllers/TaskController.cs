using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers;

public class TaskController : Controller
{
    private readonly ILogger<TaskController> _logger;
    private readonly TaskManagementDbContext _context;

    public TaskController(ILogger<TaskController> logger, TaskManagementDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    // Create form
    public IActionResult Add()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        string title = Request.Query["task-title"];
        string description = Request.Query["task-description"];
        DateTime date = DateTime.Parse(Request.Query["task-deadline"]);
        bool isCompleted = Request.Query["task-complete"] == "on";

        var task = new TaskModel
        {
            Title = title,
            Description = description,
            Date = date,
            IsCompleted = isCompleted
        };

        _context.Tasks.Add(task);
        _context.SaveChanges(); // Save changes to the database

        return RedirectToAction("Add");
    }

    // Read
    public IActionResult Items()
    {
        // fetch all tasks from the database
        var tasks = _context.Tasks.ToList();
        // print
        foreach (var task in tasks)
        {
            Console.WriteLine(task.Title);
        }

        return View(tasks);
    }

    // Update
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int id)
    {
        var task = _context.Tasks.Find(id);

        string title = Request.Form["task-title"].ToString();
        string description = Request.Form["task-description"].ToString();
        DateTime date = DateTime.Parse(Request.Form["task-deadline"]);
        bool isCompleted = Request.Form["task-complete"] == "on";

        if (task == null)
        {
            return NotFound();
        }

        if (string.IsNullOrEmpty(title) || string.IsNullOrEmpty(description) || string.IsNullOrEmpty(date.ToString()))
        {
            return BadRequest("Missing required parameters.");
        }

        task.Title = title;
        task.Description = description;
        task.Date = date;
        task.IsCompleted = isCompleted;

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(task);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tasks.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction("Details", new { id });
        }
        return View(task);
    }

    // GET: Tasks/Details/5
    public IActionResult Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    public IActionResult Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var task = _context.Tasks.FirstOrDefault(t => t.Id == id);
        if (task == null)
        {
            return NotFound();
        }

        return View(task);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        var task = _context.Tasks.Find(id);

        _context.Tasks.Remove(task);
        _context.SaveChanges();

        return RedirectToAction("Items");
    }
}
