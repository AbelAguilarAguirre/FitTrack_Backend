namespace Backend.Models
{
    public class RoutineDto
    {
        public int Id { get; set; }
        public int user_id { get; set; }
        public int activity_id { get; set; }
        public int value { get; set; }
        public string unit { get; set; } = string.Empty;
        public int repetitions { get; set; }
        public double progress { get; set; } = 0;
        public DateTime? Date { get; set; }
        public string Type { get; set; } = string.Empty; // "Goal" or "Done"
    }
}
