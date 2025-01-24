using FluentValidation;
using GamerWeb.Entity.Entities;

namespace GamerWeb.Business.Validators
{
    public class ReviewValidator : AbstractValidator<Review>
    {
        public ReviewValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık Boş Bırakılamaz");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama Boş Bırakılamaz");
            RuleFor(x => x.Subtitle).NotEmpty().WithMessage("Üst Başlık Boş Bırakılamaz");
            RuleFor(x => x.GameName).NotEmpty().WithMessage("Oyun Adı Boş Bırakılamaz");
            RuleFor(x => x.TopImage).NotEmpty().WithMessage("Üst Görsel Boş Bırakılamaz");
            RuleFor(x => x.GameImage).NotEmpty().WithMessage("Oyun Görseli Boş Bırakılamaz");
            RuleFor(x => x.Rating).NotEmpty().WithMessage("Puan Boş Bırakılamaz");
        }
    }
}
