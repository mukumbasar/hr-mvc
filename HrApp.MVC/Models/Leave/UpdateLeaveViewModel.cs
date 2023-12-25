namespace HrApp.MVC.Models.Leave
{
    public class UpdateLeaveViewModel
    {
        public int Id { get; set; }
        public string AppUserId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
