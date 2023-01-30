
namespace BookShop_Common;

public class SD
{
    public const string Success = "Success";
    public const string Error = "Error";
    public const string Warning = "Warning";
    public const string Info = "Info";


    public const string AuthorAvatar = "AuthorAvatar";
    public const string BookAvatar = "BookAvatar";
    public const string GalleryAvatar = "GalleryAvatar";
    public const string SliderAvatar = "SliderAvatar";

    public const string Role_Admin = "Admin";
    public const string Role_Customer = "Customer";

    public static string BaseApiUrl = "https://localhost:44329/";
    public static string ClientUrl = "https://localhost:44377/";

    public static string AuthorsApiPath = BaseApiUrl + "api/Authors/";
    public static string SlidersApiPath = BaseApiUrl + "api/Sliders/";
    public static string BooksApiPath = BaseApiUrl + "api/Books/";
    public static string BookCoverFormatsApiPath = BaseApiUrl + "api/BookCoverFormats/";
    public static string CategoriesApiPath = BaseApiUrl + "api/Categories/";
    public static string PubnlishersApiPath = BaseApiUrl + "api/Publishers/";
    public static string TranslatorsApiPath = BaseApiUrl + "api/Translators/";
    public static string DiscountApiPath = BaseApiUrl + "api/Discounts/";
    public static string GalleryApiPath = BaseApiUrl + "api/Galleries/";
    public static string AccountApiPath = BaseApiUrl + "api/Account/";
    public static string WishlistApiPath = BaseApiUrl + "api/Wishlists/";
    public static string AddressApiPath = BaseApiUrl + "api/Addresses/";
    public static string CartApiPath = BaseApiUrl + "api/Carts/";
    public static string StripeApiPath = BaseApiUrl + "api/Stripe/";

    public static string Local_Token = "JWT Token";
    public static string Local_UserDetails = "User_Details";
    public static string Local_Cart_data = "Cart_Data";
    public static string Local_Stripe_Session_Id = "Stripe_Session_Id";

    //public static string ServerName = "localhost";
}
