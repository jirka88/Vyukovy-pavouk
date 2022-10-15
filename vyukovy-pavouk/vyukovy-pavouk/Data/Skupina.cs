namespace vyukovy_pavouk.Data
{
    public class Skupina
{
        public int Id { get; set; }
        public string TmSkupina { get; set; }
        public ICollection<SkupinaPredmet> SkupinaPredmet { get; set; }
}
}
