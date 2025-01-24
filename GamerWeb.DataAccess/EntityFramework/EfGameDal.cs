using GamerWeb.DataAccess.Abstract;
using GamerWeb.DataAccess.Context;
using GamerWeb.DataAccess.Repositories;
using GamerWeb.Entity.Entities;

namespace GamerWeb.DataAccess.EntityFramework
{
	public class EfGameDal : GenericRepository<Game>, IGameDal
	{
		public EfGameDal(GamerWebContext context) : base(context)
		{
		}
	}
}
