using System.Text.Json.Serialization;

namespace MathHelperr.Model.Db;

public partial class Exercise
{
    public int ExerciseId { get; set; }
    public string Question { get; set; }
    
    public int ResultId { get; set; } 
    
    [JsonIgnore]
    public virtual Result Result { get; set; }
    [JsonIgnore]
    public virtual ICollection<Solution> Solutions { get; set; }

}