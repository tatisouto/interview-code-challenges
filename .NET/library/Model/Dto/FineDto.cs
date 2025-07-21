namespace OneBeyondApi.Model.Dto
{
    public class FineDto
    {
        public Guid Id { get; set; }
        public string BorrowerName { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
