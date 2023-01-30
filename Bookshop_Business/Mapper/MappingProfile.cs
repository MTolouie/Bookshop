
namespace Bookshop_Business.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Category, CategoryDTO>().ReverseMap();
        CreateMap<Book, BookDTO>().ReverseMap();
        CreateMap<Author, AuthorDTO>().ReverseMap();
        CreateMap<Publisher, PublisherDTO>().ReverseMap();
        CreateMap<Gallery, GalleryDTO>().ReverseMap();
        CreateMap<Translator, TranslatorDTO>().ReverseMap();
        CreateMap<BookCoverFormat, BookCoverFormatDTO>().ReverseMap();
        CreateMap<BookCategories, BookCategoriesDTO>().ReverseMap();
        CreateMap<ApplicationUser, ApplicationUserDTO>().ReverseMap();
        CreateMap<Address, AddressDTO>().ReverseMap();
        CreateMap<Slider, SlidersDTO>().ReverseMap();
        CreateMap<Discount, DiscountDTO>().ReverseMap();
        CreateMap<Wishlist, WishlistDTO>().ReverseMap();
        CreateMap<Cart, CartDTO>().ReverseMap();
        CreateMap<CartDetail, CartDetailsDTO>().ReverseMap();


    }
}
