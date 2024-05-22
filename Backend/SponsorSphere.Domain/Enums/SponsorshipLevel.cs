using System.ComponentModel.DataAnnotations;

namespace SponsorSphere.Domain.Enums
{
    public enum SponsorshipLevel
    {
        [Display(Name = "Monthly")] 
        Monthly, 
        
        [Display(Name = "Annual")] 
        Annual, 
        
        [Display(Name = "SinglePayment")] 
        SinglePayment, 
        
        [Display(Name = "Fund Raiser")] 
        FundRaiser
    }
}
