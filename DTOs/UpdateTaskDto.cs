using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public record UpdateTaskDto([Required][MinLength(4)][MaxLength(80)] string Title, bool Completed);
}