using MathHelperr.Model.Db;
using MathHelperr.Model.Db.DTO;

namespace MathHelperr.Model;

public class ExcerciseResult
{
    public List<string> Result { get; init; }
    public string? Question { get; init; }
    public int ExerciseId { get; set; }
}