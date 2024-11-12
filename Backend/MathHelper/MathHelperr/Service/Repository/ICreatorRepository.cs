using MathHelperr.Model.Db;
using MathHelperr.Utility;

namespace MathHelperr.Service.Repository;


public interface ICreatorRepository
{
    Task<int> GetExerciseId(IMathExcercise mathExcercise, MathTypeName mathTypeName, string level, string userId, string? aiTExt);
}