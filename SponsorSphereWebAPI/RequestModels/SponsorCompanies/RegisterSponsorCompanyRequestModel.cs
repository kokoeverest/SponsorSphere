﻿using SponsorSphere.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SponsorSphereWebAPI.RequestModels.SponsorCompanies
{
    public class RegisterSponsorCompanyRequestModel
    {
        [Required]
        [StringLength(maximumLength: 200)]
        public string Name { get; set; } = string.Empty;

        [Required]
        public CountryEnum Country { get; set; }

        [Required]
        [StringLength(maximumLength: 16)]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        [StringLength(maximumLength: 100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        [MaxLength(200)]
        public string Password { get; set; } = string.Empty;

        [Required]
        [StringLength(maximumLength: 34)]
        public string IBAN { get; set; } = string.Empty;
    }
}