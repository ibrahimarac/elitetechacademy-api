﻿using Elitetech.Academy.Application.Dto.Request;
using FluentValidation;

namespace Elitetech.Academy.Application.Validators
{
    public class AnnouncementUpdateRequestValidator : AbstractValidator<AnnouncementUpdateRequestDto>
    {
        public AnnouncementUpdateRequestValidator()
        {
            RuleFor(x => x.Detail).MaximumLength(4000).WithMessage("Detay bilgisi en fazla 4000 karakter olabilir.");
            RuleFor(announcement => announcement.EndDate)
            .GreaterThan(announcement => announcement.StartDate)
                .When(announcement => announcement.EndDate.HasValue)
                .WithMessage("Bitiş tarihi başlangıç tarihinden büyük olmalıdır.");
        }
    }
}