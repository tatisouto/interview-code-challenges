namespace OneBeyondApi.Model
{
    public class Reservation
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Borrower Borrower { get; set; }
        public Book Book { get; set; }
        public DateTime ReservationDate { get; set; }
        public bool IsActive { get; set; }
    }
}
