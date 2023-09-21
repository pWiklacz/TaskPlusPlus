namespace TaskPlusPlus.Application.DTOs.Project;
public interface IProjectDto
{
    public string Name { get; set; } 
    public string Notes { get; set; }
    public DateTime? DueDate { get; set; }
}
