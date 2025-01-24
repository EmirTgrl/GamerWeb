using GamerWeb.Business.Abstract;
using GamerWeb.DataAccess.Abstract;
using GamerWeb.Entity.Entities;

namespace GamerWeb.Business.Concrete
{
	public class ContactManager : GenericManager<Contact>, IContactService
	{
		public ContactManager(IGenericDal<Contact> genericDal) : base(genericDal)
		{
		}
	}
}
