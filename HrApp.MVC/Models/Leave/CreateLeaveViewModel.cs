namespace HrApp.MVC.Models.Leave
{
    public class CreateLeaveViewModel
    {
        public string AppUserId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
