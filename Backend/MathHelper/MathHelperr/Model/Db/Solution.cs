using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;

namespace MathHelperr.Model.Db;

public class Solution
{
    public int SolutionId { get; set; }
   
    public int ExerciseId { get; set; }
    
   [JsonIgnore]
    public Exercise Exercise { get; set; }
    
    
    public string UserId { get; set; }
    
    
    public int ElapsedTime { get; set; }
    public DateTime SolvedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}