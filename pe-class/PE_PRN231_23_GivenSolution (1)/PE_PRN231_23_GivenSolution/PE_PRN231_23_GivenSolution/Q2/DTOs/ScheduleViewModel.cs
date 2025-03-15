namespace Q2.DTOs
{
    public class ScheduleViewModel
    {
        public int Id { get; set; }
        public string MovieName { get; set; }
        public string RoomTitle { get; set; }
        public string TimeSlotName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Note { get; set; }
    }
}
