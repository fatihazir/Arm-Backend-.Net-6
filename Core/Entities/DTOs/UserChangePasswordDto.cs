using System;
using Core.Entities.Abstract;

namespace Core.Entities.DTOs
{
	public class UserChangePasswordDto : IDto
	{
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}

