using AutoMapper;
using GamerWeb.Dto.Dtos.CommentDtos;
using GamerWeb.Dto.Dtos.ContactDtos;
using GamerWeb.Dto.Dtos.ContactUsDtos;
using GamerWeb.Dto.Dtos.GameDtos;
using GamerWeb.Dto.Dtos.NewsDtos;
using GamerWeb.Dto.Dtos.ReviewDtos;
using GamerWeb.Entity.Entities;

namespace GamerWeb.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<ResultContactDto, Contact>().ReverseMap();
            CreateMap<UpdateContactDto, Contact>().ReverseMap();
            CreateMap<CreateContactDto, Contact>().ReverseMap();

            CreateMap<ResultContactUsDto, ContactUs>().ReverseMap();
            CreateMap<UpdateContactUsDto, ContactUs>().ReverseMap();
            CreateMap<CreateContactUsDto, ContactUs>().ReverseMap();

            CreateMap<ResultGameDto, Game>().ReverseMap();
            CreateMap<UpdateGameDto, Game>().ReverseMap();
            CreateMap<CreateGameDto, Game>().ReverseMap();

            CreateMap<ResultNewsDto, News>().ReverseMap();
            CreateMap<UpdateNewsDto, News>().ReverseMap();
            CreateMap<CreateNewsDto, News>().ReverseMap();

            CreateMap<ResultReviewDto, Review>().ReverseMap();
            CreateMap<UpdateReviewDto, Review>().ReverseMap();
            CreateMap<CreateReviewDto, Review>().ReverseMap();

            CreateMap<ResultCommentDto, Comment>().ReverseMap();
            CreateMap<UpdateCommentDto, Comment>().ReverseMap();
            CreateMap<CreateCommentDto, Comment>().ReverseMap();
        }
    }
}
