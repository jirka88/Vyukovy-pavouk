namespace vyukovy_pavouk.Data
{
    public class Student
{
        public int Id { get; set; } 
        public string Jmeno { get; set; }
        public string Prijmeni { get; set; }
        public string Email { get; set; }
        public List<SkupinaStudent> SkupinaStudent { get; set; }
        public List<StudentSplneni> StudentSplneni { get; set; }

    }
}
