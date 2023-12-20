namespace HrApp.MVC.Models
{
    public class PersonelHomeViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string? SecondName { get; set; }
        public string Surname { get; set; }
        public string? SecondSurname { get; set; }
        public string Department { get; set; }
        public string Occupation { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public byte[] ImageData { get; set; }
    }
}
