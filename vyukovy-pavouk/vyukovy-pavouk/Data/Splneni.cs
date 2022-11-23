namespace vyukovy_pavouk.Data
{
    public class Splneni
{
        public int Id { get; set; }
        public int IdSkupiny { get; set; }
        public int IdKapitoly { get; set; }
        public List<StudentSplneni> StudentSplneni { get; set; }
    }
}
