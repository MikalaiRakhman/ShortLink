﻿using ShortLink.Domain.Entities;

namespace ShortLink.DAL.Identity
{
    public class RefreshToken
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
