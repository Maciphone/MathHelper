using System.Text.Json.Serialization;

namespace MathHelperr.Model.Db;

public class Result
{
    public int ResultId { get; set; }
    
    public List<int> ResultValues { get; set; } = new List<int>();
    public string ResultHash { get; set; }
    //public int ResultValue { get; set; }
    

}