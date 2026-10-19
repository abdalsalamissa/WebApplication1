using WebApplication1.Models;
using WebApplication1.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services
{
    public class TaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskItem>> GetAllTasks()
        {
            return await _context.Tasks.ToListAsync();
        }

        public async Task<TaskItem?> GetTaskById(int id)
        {
            return await _context.Tasks.FindAsync(id);
        }

        public async Task<TaskItem> CreateTask(string title)
        {
            var newTask = new TaskItem{ Title = title, Completed = false };
            _context.Tasks.Add(newTask);
            await _context.SaveChangesAsync();
            return newTask;
        }

        public async Task<TaskItem?> UpdateTask(int id, string title, bool completed)
        {
            var task = await GetTaskById(id);
            if (task is null)
            {
                return null;
            }

            task.Title = title;
            task.Completed = completed;

            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteTask(int id)
        {
            var task = await GetTaskById(id);
            if (task is null)
            {
                return false;
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}