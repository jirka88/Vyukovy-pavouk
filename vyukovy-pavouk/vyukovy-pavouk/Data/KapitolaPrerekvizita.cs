namespace vyukovy_pavouk.Data
{
    public class KapitolaPrerekvizita
{
        public int KapitolaId  { get; set; }
        public Kapitola kapitola { get; set; }
        public int PrerekvizitaId { get; set; }      
        public Prerekvizity prerekvizita { get; set; }
}
}
