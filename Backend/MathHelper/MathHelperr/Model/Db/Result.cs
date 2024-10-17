using System.Text.Json.Serialization;

namespace MathHelperr.Model.Db;

public class Result
{
    public int ResultId { get; set; }
    
    public int ResultValue { get; set; }

    [JsonIgnore]
    public virtual ICollection<Solution> Solutions { get; set; }

}