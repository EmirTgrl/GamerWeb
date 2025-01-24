using FluentValidation;
using GamerWeb.Entity.Entities;

namespace GamerWeb.Business.Validators
{
    public class NewsValidator : AbstractValidator<News>
    {
        public NewsValidator()
        {
            RuleFor(x=>x.Title).NotEmpty().WithMessage("Başlık Boş Bırakılamaz");
            RuleFor(x=>x.Description).NotEmpty().WithMessage("Açıklama Boş Bırakılamaz");
            RuleFor(x=>x.Subtitle).NotEmpty().WithMessage("Üst Başlık Boş Bırakılamaz");
            RuleFor(x=>x.GameName).NotEmpty().WithMessage("Oyun Adı Boş Bırakılamaz");
            RuleFor(x=>x.ImageUrl).NotEmpty().WithMessage("Görsel Boş Bırakılamaz");
        }
    }
}
