using System;
using Core.Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class ChangePasswordValidation : AbstractValidator<UserChangePasswordDto>
	{
		public ChangePasswordValidation()
		{
			RuleFor(u => u.Email).NotEmpty();
			RuleFor(u => u.OldPassword).NotEmpty();
			RuleFor(u => u.NewPassword).NotEmpty().MinimumLength(8);
		}
	}
}

