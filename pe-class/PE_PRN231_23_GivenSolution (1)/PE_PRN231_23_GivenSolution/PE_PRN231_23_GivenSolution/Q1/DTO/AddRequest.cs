namespace Q1.DTO
{
    public class AddRequest
    {
        public int MovieId { get; set; }
        public int RoomId { get; set; }

        public int TimeSlotId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
