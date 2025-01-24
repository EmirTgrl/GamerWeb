using GamerWeb.Business.Abstract;
using GamerWeb.DataAccess.Abstract;
using GamerWeb.Entity.Entities;

namespace GamerWeb.Business.Concrete
{
	public class GameManager : GenericManager<Game>, IGameService
	{
		public GameManager(IGenericDal<Game> genericDal) : base(genericDal)
		{
		}
	}
}
