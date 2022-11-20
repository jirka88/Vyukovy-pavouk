using FluentValidation;

namespace vyukovy_pavouk.Data
{
    public class Prerekvizity
{
        public int? Id { get; set; } 
        public int IdPrerekvizity { get; set; }
        public List<KapitolaPrerekvizita> KapitolaPrerekvizita { get; set; }
}
    
}
