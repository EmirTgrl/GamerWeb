using GamerWeb.Business.Abstract;
using GamerWeb.DataAccess.Abstract;
using GamerWeb.Entity.Entities;

namespace GamerWeb.Business.Concrete
{
	public class ContactUsManager : GenericManager<ContactUs>, IContactUsService
	{
		public ContactUsManager(IGenericDal<ContactUs> genericDal) : base(genericDal)
		{
		}
	}
}
