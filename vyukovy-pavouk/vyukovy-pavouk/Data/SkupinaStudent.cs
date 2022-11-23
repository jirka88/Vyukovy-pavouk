namespace vyukovy_pavouk.Data
{
    public class SkupinaStudent
{
        public int IdSkupina { get; set; }
        public Skupina Skupina { get; set; }
        public int IdStudent { get; set; }
        public Student Student { get; set; }
    }
}
