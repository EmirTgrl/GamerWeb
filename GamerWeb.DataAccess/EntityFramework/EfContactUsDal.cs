using GamerWeb.DataAccess.Abstract;
using GamerWeb.DataAccess.Context;
using GamerWeb.DataAccess.Repositories;
using GamerWeb.Entity.Entities;

namespace GamerWeb.DataAccess.EntityFramework
{
	public class EfContactUsDal : GenericRepository<ContactUs>, IContactUsDal
	{
		public EfContactUsDal(GamerWebContext context) : base(context)
		{
		}
	}
}
