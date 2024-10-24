using MathHelperr.Model.Db;
using MathHelperr.Utility;

namespace MathHelperr.Service;

public interface ICreatorRepository
{
    Task<Solution> GetSolution(IMathExcercise mathExcercise, MathTypeName mathTypeName, string level, string userId);
}