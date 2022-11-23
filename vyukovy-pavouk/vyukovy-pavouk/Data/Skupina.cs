namespace vyukovy_pavouk.Data
{
    public class Skupina
{
        public int Id { get; set; }
        public string TmSkupina { get; set; }
        public int IDPredmetu { get; set; }
        public Predmet predmet { get; set; }
        public ICollection<SkupinaStudent> Student { get; set; }
    }
}
