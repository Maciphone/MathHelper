using MathHelperr.Model.Db;
using MathHelperr.Model.Db.DTO;

namespace MathHelperr.Model;

public class ExcerciseResult
{
    public List<int> Result { get; init; }
    public string? Question { get; init; }
    public Solution Solution { get; set; }
}