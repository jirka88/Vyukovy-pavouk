﻿
namespace vyukovy_pavouk.Data
{
    public class Predmet
{
        public int Id { get; set; }
        public string Nazev { get; set; }
        public ICollection<Kapitola> Kapitoly { get; set; }
        public List<Skupina> Skupiny { get; set; }
    }
}
