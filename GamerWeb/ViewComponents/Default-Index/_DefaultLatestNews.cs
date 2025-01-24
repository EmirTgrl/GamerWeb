using AutoMapper;
using GamerWeb.Business.Abstract;
using GamerWeb.Dto.Dtos.NewsDtos;
using Microsoft.AspNetCore.Mvc;

namespace GamerWeb.ViewComponents.Default_Index
{
	public class _DefaultLatestNews : ViewComponent
    {
        private readonly INewsService _newsService;
        private readonly IMapper _mapper;

		public _DefaultLatestNews(INewsService newsService, IMapper mapper)
		{
			_newsService = newsService;
			_mapper = mapper;
		}

		public async Task<IViewComponentResult> InvokeAsync()
        {
			var last4News = await _newsService.TGetLast4NewsAsync();
			var last4NewsDto = _mapper.Map<List<ResultNewsDto>>(last4News);
			return View(last4NewsDto);
		}
    }
}
