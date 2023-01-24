namespace vyukovy_pavouk.Data
{
    public class GroupStudent
{
        public int GroupID { get; set; }
        public Group Group { get; set; }
        public int StudentID { get; set; }
        public Student Student { get; set; }
    }
}
