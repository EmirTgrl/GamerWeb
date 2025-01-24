using AutoMapper;
using GamerWeb.Business.Abstract;
using GamerWeb.Dto.Dtos.ContactUsDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamerWeb.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    [Area("Admin")]
    [Route("Admin/Message")]
    public class MessageController : Controller
    {
        private readonly IContactUsService _contactUsService;
        private readonly IMapper _mapper;

        public MessageController(IContactUsService contactUsService, IMapper mapper)
        {
            _contactUsService = contactUsService;
            _mapper = mapper;
        }

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var values = await _contactUsService.TGetListAsync();
            var valueList = _mapper.Map<List<ResultContactUsDto>>(values);
            return View(valueList);
        }

        [Route("DeleteMessage/{id}")]
        public async Task<IActionResult> DeleteMessage(int id)
        {
            await _contactUsService.TDeleteAsync(id);
            return RedirectToAction("Index");
        }

        [Route("MessageDetails")]
        public async Task<IActionResult> MessageDetails(int id)
        {
            var value = await _contactUsService.TGetByIdAsync(id);
            var messageValue = _mapper.Map<ResultContactUsDto>(value);
            return View(messageValue);
        }
    }
}
