using AutoMapper;
using GamerWeb.Business.Abstract;
using GamerWeb.Dto.Dtos.ContactUsDtos;
using GamerWeb.Entity.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GamerWeb.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactUsService _contactUsService;
        private readonly IMapper _mapper;

        public ContactController(IContactUsService contactUsService, IMapper mapper)
        {
            _contactUsService = contactUsService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CreateContactUsDto createContactUsDto)
        {
            createContactUsDto.Date = DateTime.Now;
            var newContact = _mapper.Map<ContactUs>(createContactUsDto);
            await _contactUsService.TCreateAsync(newContact);
            return RedirectToAction("Index", "Default");
        }
    }
}
