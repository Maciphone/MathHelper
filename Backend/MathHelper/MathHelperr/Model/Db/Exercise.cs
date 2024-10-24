using System.Text.Json.Serialization;

namespace MathHelperr.Model.Db;

public partial class Exercise
{
    public int ExerciseId { get; set; }
    public string Question { get; set; }
    
    public string Level { get; set; }
    
    public string MathType { get; set; }
    
    public int ResultId { get; set; } 
    public virtual Result Result { get; set; }
    
    public virtual ICollection<Solution> Solutions { get; set; }

}
