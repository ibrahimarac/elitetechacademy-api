using Elitetech.Academy.Application.Dto.Request;
using FluentValidation;

namespace Elitetech.Academy.Application.Validators
{
    public class AnnouncementCreateRequestValidator : AbstractValidator<AnnouncementCreateRequestDto>
    {
        public AnnouncementCreateRequestValidator()
        {
            RuleFor(x => x.Title).MaximumLength(150).WithMessage("Başlık en fazla 150 karakter olabilir.");
            RuleFor(x => x.Detail).MaximumLength(4000).WithMessage("Detay bilgisi en fazla 4000 karakter olabilir.");
            RuleFor(announcement => announcement.EndDate)
            .GreaterThan(announcement => announcement.StartDate)
                .When(announcement => announcement.EndDate.HasValue)
                .WithMessage("Bitiş tarihi başlangıç tarihinden büyük olmalıdır.");
        }
    }
}
