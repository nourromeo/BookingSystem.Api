namespace BookingSystem.Api.Model
{
    public class booking
    {
        public int Id { get; set; }
        public string RoomName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
