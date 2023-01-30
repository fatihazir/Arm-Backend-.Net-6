using System;
using Entities.DTOs;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
	public class RegisterValidation: AbstractValidator<UserForRegisterDto>
    {
		public RegisterValidation()
		{
			RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u => u.LastName).NotEmpty();
            RuleFor(u => u.Email).NotEmpty();
            RuleFor(u => u.Password).NotEmpty();
        }
	}
}

