using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EBuy.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EBuy.Models;

namespace EBuy.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<EBuyDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<EBuyDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<EBuyDbContext>())
                {
                    //context.Categories.Add(new Category() { Name = "Laptops" });
                    //context.Categories.Add(new Category() { Name = "Phones" });
                    //context.Categories.Add(new Category() { Name = "Computers" });
                    //context.Categories.Add(new Category() { Name = "Monitors" });
                    //context.Categories.Add(new Category() { Name = "TVs" });
                    //context.Categories.Add(new Category() { Name = "Accessories" });

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "ASUS E402WA",
                    //    Price = 1300,
                    //    Description = "A value notebook doesn't have to mean settling for something basic. ASUS Laptop E402 gives you all the notebook essentials with a premium feel. ASUS Laptop E402 features compact dimensions with streamlined curves.",
                    //    ImageUrl = "https://www.asus.com/us/Laptops/ASUS-Laptop-E402WA/websites/global/products/3tcXKZ5TbCLp2epf/img/01/fg.png",
                    //    VotesCount = 256,
                    //    Score = 1100.8,
                    //    PurchasesCount = 564,
                    //    InStock = 153,
                    //    CategoryId = "94fe90a5-e62b-4d64-a0d8-17cadd63bebc",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "ROG Zephyrus G GA502",
                    //    Price = 2450,
                    //    Description = "The all-new ROG Zephyrus G deftly balances exceptional portability with potent performance to bring ultra-slim Windows 10 gaming laptops to a bigger audience. Its AMD® Ryzen™ 7 processor and GeForce® GTX 1660 Ti graphics power through work and play, while up to 8.8 hours of battery life keeps you running on the road. Immerse yourself in up to a 120Hz display framed by super-narrow bezels that practically melt away, and turn up the volume with smart amp technology that gets louder with less distortion.",
                    //    ImageUrl = "https://www.notebookcheck.net/fileadmin/Notebooks/News/_nc3/Picture32.png",
                    //    VotesCount = 133,
                    //    Score = 638.4,
                    //    PurchasesCount = 245,
                    //    InStock = 24,
                    //    CategoryId = "94fe90a5-e62b-4d64-a0d8-17cadd63bebc",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "ASUS VivoBook Pro 15 N580GD",
                    //    Price = 1750,
                    //    Description = "The ASUS VivoBook Pro 15 is a slim and lightweight high-performance laptop that’s powered by a 8th Generation Intel® Core™ processor. It features a 4K UHD display with 100% sRGB color gamut, high-quality NVIDIA® GeForce® GTX 1050 gaming-grade graphics, audio co-developed by Harman Kardon, and the latest cooling and fast-charge technologies.",
                    //    ImageUrl = "https://www.asus.com/media/global/products/t4fNeygwPZ6EeO3p/P_setting_fff_1_90_end_600.png",
                    //    VotesCount = 356,
                    //    Score = 1637.6,
                    //    PurchasesCount = 756,
                    //    InStock = 17,
                    //    CategoryId = "94fe90a5-e62b-4d64-a0d8-17cadd63bebc",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "DELL Inspiron 15 3000",
                    //    Price = 1450,
                    //    Description = "Stay powered up with this lightweight, 15” laptop featuring Intel® processors, excellent battery life and sound by Waves MaxxAudio® Pro.",
                    //    ImageUrl = "https://i.dell.com/das/xa.ashx/global-site-design%20WEB/cbb1f783-ba8e-af84-fb31-347569327dc4/1/OriginalPng?id=Dell/Product_Images/Dell_Client_Products/Notebooks/Inspiron_Notebooks/Inspiron_15_3559/Non_Touch/global_spi/laptop-inspiron-15-3559-black-left-generic-bestof-500-ng.psd",
                    //    VotesCount = 856,
                    //    Score = 3338.4,
                    //    PurchasesCount = 1537,
                    //    InStock = 189,
                    //    CategoryId = "94fe90a5-e62b-4d64-a0d8-17cadd63bebc",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "HP Gaming 15",
                    //    Price = 1899,
                    //    Description = "The HP Pavilion Gaming Laptop is designed so you can excel at any task. Gaming. Creation. Entertainment. An Intel® Core™ processor and discrete graphics work together to jumpstart everything you do. Don’t settle for average performance. Prepare yourself for a new level of power.",
                    //    ImageUrl = "https://ssl-product-images.www8-hp.com/digmedialib/prodimg/lowres/c05974469.png",
                    //    VotesCount = 26,
                    //    Score = 84.4,
                    //    PurchasesCount = 31,
                    //    InStock = 69,
                    //    CategoryId = "94fe90a5-e62b-4d64-a0d8-17cadd63bebc",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "DELL XPS 13",
                    //    Price = 2099,
                    //    Description = "The world's smallest 13-inch laptop with captivating Dell Cinema and next-gen InfinityEdge. Featuring an 8th Gen Intel® Quad Core processor in a stunning new look.",
                    //    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/916lcfG4SnL._SL1500_.jpg",
                    //    VotesCount = 266,
                    //    Score = 1117.2,
                    //    PurchasesCount = 298,
                    //    InStock = 39,
                    //    CategoryId = "94fe90a5-e62b-4d64-a0d8-17cadd63bebc",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "",
                    //    Price = 1899,
                    //    Description = "",
                    //    ImageUrl = "",
                    //    VotesCount = 26,
                    //    Score = 84.4,
                    //    PurchasesCount = 31,
                    //    InStock = 69,
                    //    CategoryId = "94fe90a5-e62b-4d64-a0d8-17cadd63bebc",
                    //});

                    context.SaveChanges();
                }
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
