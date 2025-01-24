namespace GamerWeb.DataAccess.Abstract
{
	public interface IGenericDal<T> where T : class
	{
		Task CreateAsync(T entity);
		Task UpdateAsync(T entity);
		Task DeleteAsync(int id);
		Task<List<T>> GetListAsync();
		Task<T> GetByIdAsync(int id);
	}
}
