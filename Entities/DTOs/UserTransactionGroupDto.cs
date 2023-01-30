using System;
using Core.Entities.Abstract;

namespace Entities.DTOs
{
	public class UserTransactionGroupDto : IDto
	{
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Alias { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int ResultCount { get; set; }
    }
}

