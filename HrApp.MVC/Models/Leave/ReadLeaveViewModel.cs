namespace HrApp.MVC.Models.Leave
{
    public class ReadLeaveViewModel
    {
        public int Id { get; set; }
        public string LeaveTypeName { get; set; }
        public DateTime StartDate { get; set; }
        public int NumDays { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime RequestDate { get; set; }
        public string ApprovalStatus { get; set; }
        public DateTime ApprovalDate { get; set; }
        public string AppUserId { get; set; }
    }
}
