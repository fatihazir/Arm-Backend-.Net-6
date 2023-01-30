using System;
using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class LoginValidation : AbstractValidator<UserForLoginDto>
	{
		public LoginValidation()
		{
			RuleFor(u => u.Email).NotEmpty();
			RuleFor(u => u.Password).NotEmpty();
		}
	}
}

