using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs
{
    public record CreateTaskDto([Required][MinLength(4)][MaxLength(80)] string Title);
}