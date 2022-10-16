namespace vyukovy_pavouk.Data
{
    public class Splneni
{
        public int Id { get; set; }
        public int Id_studenta { get; set; }
        public int Id_skupiny { get; set; }
        public int Id_kapitoly { get; set; }
        public Student student { get; set; }
    }
}
