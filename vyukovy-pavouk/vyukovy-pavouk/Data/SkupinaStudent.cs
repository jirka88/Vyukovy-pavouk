namespace vyukovy_pavouk.Data
{
    public class SkupinaStudent
{
        public int SkupinaId { get; set; }
        public Skupina Skupina { get; set; }
        public int StudentId { get; set; }
        public Student Student { get; set; }
    }
}
