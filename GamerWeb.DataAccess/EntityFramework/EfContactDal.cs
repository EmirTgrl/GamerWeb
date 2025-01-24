using GamerWeb.DataAccess.Abstract;
using GamerWeb.DataAccess.Context;
using GamerWeb.DataAccess.Repositories;
using GamerWeb.Entity.Entities;

namespace GamerWeb.DataAccess.EntityFramework
{
	public class EfContactDal : GenericRepository<Contact>, IContactDal
	{
		public EfContactDal(GamerWebContext context) : base(context)
		{
		}
	}
}
