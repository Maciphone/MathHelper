namespace MathHelperr.Service;

public interface IRepositoryUserData<T>  where T : class
{
    Task <T> GetByUserIdAsync(int id, int userId);
    Task<IEnumerable<T>> GetAllByUserIdAsync(int userId);
    Task<IEnumerable<T>> GetByUserIdIntervalAsync(int userId, DateTime fromDate, DateTime toDate);
}