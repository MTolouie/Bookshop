using Blazored.LocalStorage;
using Bookshop_BlazorClient;
using HashidsNet;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient();
builder.Services.AddScoped<ISliderRepository,SliderRepository>();
builder.Services.AddScoped<IBookRepository,BookRepository>();
builder.Services.AddScoped<ICategoryRepository,CategoryRepository>();
builder.Services.AddScoped<IDiscountRepository,DiscountRepository>();
builder.Services.AddScoped<IPublisherRepository,PublisherRepository>();
builder.Services.AddScoped<IGalleryRepository,GalleryRepository>();
builder.Services.AddScoped<ITranslatorRepository,TranslatorRepository>();
builder.Services.AddScoped<IBookCoverFormatRepository,BookCoverFormatRepository>();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IWishlistRepository, WishlistRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IStripePaymentRepository, StripePaymentRepository>();
builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<IHashids>(_ => new Hashids("Bookshop", 11));
builder.Services.AddHttpClient();
await builder.Build().RunAsync();
