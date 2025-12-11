namespace UTHConfMS.Core.Entities
{
    public class Conference
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
