namespace MathHelperr.Service;

public interface IRepositoryUserData<T>  where T : class
{
    Task <T> GetByUserIdAsync(int id, string userId);
    Task<IEnumerable<T>> GetAllByUserIdAsync(string userId);
    Task<IEnumerable<T>> GetByUserIdIntervalAsync(string userId, DateTime fromDate, DateTime toDate);
}