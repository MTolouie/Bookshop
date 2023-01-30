using Bookshop_DataLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookshop_DataLayer.Context;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
    {

    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<BookCategories> BookCategories{ get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Translator> Translators{ get; set; }
    public DbSet<Publisher> Publishers{ get; set; }
    public DbSet<Gallery> Gallery { get; set; }
    public DbSet<BookCoverFormat> BookCoverFormats { get; set; }
    public DbSet<ApplicationUser> ApplicationUser { get; set; }
    public DbSet<Address> Addresses{ get; set; }
    public DbSet<Slider> Sliders{ get; set; }
    public DbSet<Discount> Discounts{ get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<Cart> Cart { get; set; }
    public DbSet<CartDetail> CartDetail { get; set; }

}
