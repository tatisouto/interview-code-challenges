using AutoMapper;

namespace OneBeyondApi.Model.Dto.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.BookName, opt => opt.MapFrom(src => src.Book.Name))
                .ForMember(dest => dest.BorrowerName, opt => opt.MapFrom(src => src.Borrower.Name))
                .ForMember(dest => dest.ReservationDate, opt => opt.MapFrom(src => src.ReservationDate))
                .ForMember(dest => dest.IsActive, opt => opt.MapFrom(src => src.IsActive));


            CreateMap<ReserveAvailable, ReserveAvailableDto>();

            CreateMap<Fine, FineDto>();
            CreateMap<FineDto, FineDto>();

            CreateMap<Book, BookDto>();
            CreateMap<BookDto, Book>();

            CreateMap<BookFormat, BookFormatDto>();
            CreateMap<BookFormatDto, BookFormat>();


            CreateMap<BookStock, BookStockDto>();
            CreateMap<BookStockDto, BookStock>();

            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, Author>();

            CreateMap<Borrower, BorrowerDto>();
            CreateMap<BorrowerDto, Borrower>();

            CreateMap<CatalogueSearch, CatalogueSearchRequestDto>();
            CreateMap<CatalogueSearchRequestDto, CatalogueSearch>();


        }
    }

}
