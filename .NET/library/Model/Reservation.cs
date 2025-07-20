namespace OneBeyondApi.Model
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public Borrower Borrower { get; set; }
        public Book Book { get; set; }
        public DateTime ReservationDate { get; set; } = DateTime.Now.Date;
        public bool IsActive { get; set; }
    }
}
