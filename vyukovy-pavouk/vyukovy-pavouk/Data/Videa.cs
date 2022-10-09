namespace vyukovy_pavouk.Data
{
    public class Videa
{
        public int Id { get; set; }
        public int IdKapitoly { get; set; }
        public string Odkaz { get; set; }
        public Kapitola kapitola { get; set; }
    }
}
