namespace OneBeyondApi.Model.Dto
{
    public class BookDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public AuthorDto Author { get; set; }
        public BookFormatDto Format { get; set; }
        public string ISBN { get; set; }
    }
}
