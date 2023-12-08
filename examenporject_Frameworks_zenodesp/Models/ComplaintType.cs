namespace examenporject_Frameworks_zenodesp.Models
{
    public class ComplaintType
    {
        public string Id { get; set; }
        public string TypeName { get; set; }

        public DateTime created { get; set; } = DateTime.Now;
        public DateTime deleted { get; set; } = DateTime.MaxValue;
    }
}
