using System;
using Core.Utilities.Security.JWT;

namespace Entities.DTOs
{
	public class UserLoginCustomDto : AccessToken
	{
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhotoUrl { get; set; }
    }
}

