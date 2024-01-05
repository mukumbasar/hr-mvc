namespace HrApp.MVC.Models.Leave
{
    public class LeaveTypeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumDays { get; set; }
        public int LeaveTypeFocusId { get; set; }
    }
}
