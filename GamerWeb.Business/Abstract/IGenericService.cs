namespace GamerWeb.Business.Abstract
{
	public interface IGenericService<T> where T : class
	{
		Task TCreateAsync(T entity);
		Task TUpdateAsync(T entity);
		Task TDeleteAsync(int id);
		Task<List<T>> TGetListAsync();
		Task<T> TGetByIdAsync(int id);
	}
}
