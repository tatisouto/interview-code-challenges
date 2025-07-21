namespace OneBeyondApi.Model.Dto
{
    public class BookStockDto
    {
        public Guid Id { get; set; }
        public BookDto Book { get; set; }
        public DateTime? LoanEndDate { get; set; }
        public BorrowerDto? OnLoanTo { get; set; }
    }
}
