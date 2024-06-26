﻿namespace SponsorSphere.Domain.Models
{
    public class Picture
    {
        public int Id { get; set; }
        public string? Url { get; set; } = string.Empty;
        public byte[]? Content { get; set; }
        public DateTime Modified { get; set; }
    }
}
