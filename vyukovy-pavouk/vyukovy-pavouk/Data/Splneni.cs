namespace vyukovy_pavouk.Data
{
    public class Splneni
{
        public int Id { get; set; }
        public int SkupinaID { get; set; }
        public int KapitolaID { get; set; }
        public List<StudentSplneni> StudentSplneni { get; set; }
    }
}
