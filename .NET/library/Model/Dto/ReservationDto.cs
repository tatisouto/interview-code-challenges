using OneBeyondApi.Model;

namespace OneBeyondApi.Model.Dto
{
    public class ReservationDto
    {
        public Guid Id { get; set; }
        public string BorrowerName { get; set; }
        public string BookName { get; set; }
        public DateTime ReservationDate { get; set; }        
        public bool IsActive { get; set; }
    }
}

