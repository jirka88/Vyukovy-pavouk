namespace vyukovy_pavouk.Data
{
    public class StudentSplneni
{
        public int StudentID { get; set; }
        public Student student { get; set; }
        public int SplneniID { get; set; }
        public Splneni splneni { get; set; }
        public bool Uspech { get; set; }

}
}
