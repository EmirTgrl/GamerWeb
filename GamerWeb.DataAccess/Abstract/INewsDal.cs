using GamerWeb.Entity.Entities;

namespace GamerWeb.DataAccess.Abstract
{
	public interface INewsDal : IGenericDal<News>
	{
		Task<List<News>> GetLast4NewsAsync();
	}
}
