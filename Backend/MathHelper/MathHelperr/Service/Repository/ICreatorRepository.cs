using MathHelperr.Model.Db;
using MathHelperr.Utility;

namespace MathHelperr.Service;

public interface ICreatorRepository
{
    Task<int> GetExerciseId(IMathExcercise mathExcercise, MathTypeName mathTypeName, string level, string userId, string? aiTExt);
}