namespace vyukovy_pavouk.Data
{
    public class SkupinaStudent
{
        public int SkupinaID { get; set; }
        public Skupina Skupina { get; set; }
        public int StudentID { get; set; }
        public Student Student { get; set; }
    }
}
