using System.ComponentModel.DataAnnotations;

namespace HrApp.MVC.Models.Advance
{
    public class UpdateAdvanceViewModel
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int AdvanceTypeId { get; set; }
        public int CurrencyId { get; set; }
        public decimal Amount { get; set; }
        public DateTime RequestDate { get; set; }
        [MaxLength(100, ErrorMessage = "Description must be maximum of 100 characters.")]
        [MinLength(5, ErrorMessage = "Description must be minimum of 5 characters.")]
        public string Description { get; set; }
    }
}
