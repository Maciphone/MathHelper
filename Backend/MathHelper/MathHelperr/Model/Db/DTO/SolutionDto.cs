namespace MathHelperr.Model.Db.DTO;

public class SolutionDto
{
    public int SolutionId { get; set; }
    public string? MathTypeName { get; set; }
    public string? Level { get; set; }
    public List<int>? ResultValue { get; set; }
    public int ElapsedTime { get; set; }
    public DateTime SolvedAt { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? UserId { get; set; }
    public string Exercise { get; set; }
}