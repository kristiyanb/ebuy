namespace EBuy.Web
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.UI;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using EBuy.Data;
    using EBuy.Services;
    using EBuy.Models;
    using System;

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
            services.AddDefaultIdentity<User>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<EBuyDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IUserService, UserService>();
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

                    //Laptops
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
                    //    CategoryId = "599d6f59-ec2b-4c80-ac89-85793259ccf6",
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
                    //    CategoryId = "599d6f59-ec2b-4c80-ac89-85793259ccf6",
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
                    //    CategoryId = "599d6f59-ec2b-4c80-ac89-85793259ccf6",
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
                    //    CategoryId = "599d6f59-ec2b-4c80-ac89-85793259ccf6",
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
                    //    CategoryId = "599d6f59-ec2b-4c80-ac89-85793259ccf6",
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
                    //    CategoryId = "599d6f59-ec2b-4c80-ac89-85793259ccf6",
                    //});

                    //Smartphones
                    //context.Products.Add(new Product()
                    //{
                    //    Name = "iPhone XR",
                    //    Price = 1399,
                    //    Description = "All-screen design. Longest battery life ever in an iPhone. Fastest performance. Water and splash resistant. Studio-quality photos and 4K video. More secure with Face ID. The new iPhone XR. It’s a brilliant upgrade.",
                    //    ImageUrl = "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/image/AppleInc/aos/published/images/i/ph/iphone/xr/iphone-xr-black-select-201809?wid=940&hei=1112&fmt=png-alpha&qlt=80&.v=1551226038992",
                    //    VotesCount = 1344,
                    //    Score = 5779.2,
                    //    PurchasesCount = 1651,
                    //    InStock = 487,
                    //    CategoryId = "b68c1dda-ec63-401c-991d-d07498b45b63",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "iPhone XS",
                    //    Price = 1299,
                    //    Description = "Super Retina in two sizes — including the largest display ever on an iPhone. Even faster Face ID. The smartest, most powerful chip in a smartphone. And a breakthrough dual-camera system with Depth Control. iPhone XS is everything you love about iPhone. Taken to the extreme.",
                    //    ImageUrl = "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/image/AppleInc/aos/published/images/i/ph/iphone/xs/iphone-xs-silver-select-2018?wid=940&hei=1112&fmt=png-alpha&qlt=80&.v=1550795411708",
                    //    VotesCount = 1218,
                    //    Score = 5602.8,
                    //    PurchasesCount = 1321,
                    //    InStock = 645,
                    //    CategoryId = "b68c1dda-ec63-401c-991d-d07498b45b63",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "Samsung Galaxy S10",
                    //    Price = 1350,
                    //    Description = "The result of 10 years of pioneering mobile firsts, Galaxy S10e, S10, and S10+ introduce the next generation of mobile innovation.",
                    //    ImageUrl = "https://image-us.samsung.com/SamsungUS/mobile/phones/06102019-new/First_S10e_Lockup1_Blue_gallery.jpg?$product-details-jpg$",
                    //    VotesCount = 967,
                    //    Score = 4544.9,
                    //    PurchasesCount = 1131,
                    //    InStock = 436,
                    //    CategoryId = "b68c1dda-ec63-401c-991d-d07498b45b63",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "Samsung Galaxy A50",
                    //    Price = 799,
                    //    Description = "Introducing the new Galaxy A50, a phone designed for the way you live at the price you want. Everything you need, nothing you don't.",
                    //    ImageUrl = "https://image-us.samsung.com/SamsungUS/samsungbusiness/products/mobile/phones/galaxy-a/galaxy-a50/gallery/SM-A505_004_R-Perspective_Black_600x600.jpg?$product-details-jpg$",
                    //    VotesCount = 487,
                    //    Score = 1899.3,
                    //    PurchasesCount = 675,
                    //    InStock = 332,
                    //    CategoryId = "b68c1dda-ec63-401c-991d-d07498b45b63",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "Huawei Mate 10 Pro",
                    //    Price = 1149,
                    //    Description = "Designed to bring your vision to life: Enjoy an immersive viewing experience with HDR 10 technology.",
                    //    ImageUrl = "https://cdn2.gsmarena.com/vv/pics/huawei/huawei-mate10-pro-1.jpg",
                    //    VotesCount = 589,
                    //    Score = 2414.9,
                    //    PurchasesCount = 641,
                    //    InStock = 259,
                    //    CategoryId = "b68c1dda-ec63-401c-991d-d07498b45b63",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "Google Pixel 3",
                    //    Price = 1699,
                    //    Description = "Everything you wish a phone could be.",
                    //    ImageUrl = "https://images-na.ssl-images-amazon.com/images/I/61jEXWFEGOL._SX569_.jpg",
                    //    VotesCount = 348,
                    //    Score = 1635.6,
                    //    PurchasesCount = 487,
                    //    InStock = 143,
                    //    CategoryId = "b68c1dda-ec63-401c-991d-d07498b45b63",
                    //});

                    ////PCs
                    //context.Products.Add(new Product()
                    //{
                    //    Name = "ASUS G11CD",
                    //    Price = 1499,
                    //    Description = "ASUS G11 is designed for casual gamers who want a high performance gaming platform that doesn't break the bank. Powered by a 7th Generation Intel® Core™ processor and up to a NVIDIA® GeForce® GTX 1080 graphics card, the G11 brings exceptional processing power and mind-blowing visuals to the table. It looks the part, too — thanks to an aggressive design and customizable ASUS Aura lighting effects.",
                    //    ImageUrl = "https://www.asus.com/media/global/products/elYHDnAu2LRlmViE/P_setting_000_1_90_end_500.png",
                    //    VotesCount = 64,
                    //    Score = 307.2,
                    //    PurchasesCount = 75,
                    //    InStock = 29,
                    //    CategoryId = "ccd48d72-cf52-4edc-a3c1-d62c49993866",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "Mac Pro 8-Core Dual GPU",
                    //    Price = 6899,
                    //    Description = "With the power and performance to bring your biggest ideas to life, Mac Pro is built for creativity on an epic scale.",
                    //    ImageUrl = "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/image/AppleInc/aos/published/images/m/ac/mac/pro/mac-pro-config-hero-201505?wid=164&hei=259&fmt=jpeg&qlt=95&.v=0",
                    //    VotesCount = 21,
                    //    Score = 105,
                    //    PurchasesCount = 34,
                    //    InStock = 28,
                    //    CategoryId = "ccd48d72-cf52-4edc-a3c1-d62c49993866",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "ASUS TUF Gaming FX10CP",
                    //    Price = 1289,
                    //    Description = "When it comes to toughness, ASUS TUF Gaming FX10CP is designed to outlast all other desktop PCs in its class. This Windows 10 gaming desktop has been subjected to stringent quality testing for rock-solid stability and durability that goes far beyond standard. FX10CP is powered by up to an 8th Generation Intel® Core™ i7 processor with NVIDIA® GeForce® GTX 10-Series graphics, and features superfast Intel Wi-Fi for seamless online gaming.",
                    //    ImageUrl = "https://www.asus.com/media/global/products/HKpIJZy7vhOnLQqj/P_setting_fff_1_90_end_600.png",
                    //    VotesCount = 54,
                    //    Score = 268.92,
                    //    PurchasesCount = 63,
                    //    InStock = 13,
                    //    CategoryId = "ccd48d72-cf52-4edc-a3c1-d62c49993866",
                    //});

                    ////Monitors
                    //context.Products.Add(new Product()
                    //{
                    //    Name = "ROG Swift PG27UQ Gaming Monitor",
                    //    Price = 650,
                    //    Description = "ROG Swift PG27UQ is a 4K UHD G-SYNC™ HDR gaming monitor with an overclockable 144Hz refresh rate for buttery-smooth gameplay. This 27-inch IPS display with quantum-dot technology heralds a new generation of gaming. It provides a wide DCI-P3 color gamut for more realistic colors and smoother color gradation, and HDR technology to deliver vivid details and lifelike contrast. Additionally, a built-in light sensor automatically adjusts the brightness of the screen according to the light conditions of your environment. And with Aura Sync, ROG Light Signal and Light Signature, you can customize your gaming setup to suit your own style. See the future of gaming with ROG Swift PG27UQ.",
                    //    ImageUrl = "https://www.asus.com/media/global/products/fRoiTmXJNO2QtbzD/P_setting_000_1_90_end_500.png",
                    //    VotesCount = 52,
                    //    Score = 260,
                    //    PurchasesCount = 59,
                    //    InStock = 8,
                    //    CategoryId = "95c6e35c-7922-458f-9f65-1da8b2702bb2",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "ASUS VG279Q Gaming Monitor",
                    //    Price = 699,
                    //    Description = "Designed for intense, fast-paced games, ASUS VG279Q is a 27” Full HD gaming IPS display with an ultra-fast 1ms (MPRT) response time and blazing 144Hz refresh rate to give you super-smooth gameplay. VG279Q features Adaptive-Sync (FreeSync™) technology to eliminate screen tearing and choppy frame rate.",
                    //    ImageUrl = "https://www.asus.com/media/global/products/Go8cFldi9g1nNbP0/P_setting_000_1_90_end_500.png",
                    //    VotesCount = 39,
                    //    Score = 191.1,
                    //    PurchasesCount = 48,
                    //    InStock = 12,
                    //    CategoryId = "95c6e35c-7922-458f-9f65-1da8b2702bb2",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "LG UltraFine 4K ",
                    //    Price = 999,
                    //    Description = "With a stunning 3840-by-2160 resolution, the 23.7-inch LG UltraFine 4K Display brings your favorite photos and videos to life. So whether you’re watching a movie or editing an image, this high-performance monitor delivers immaculate 4K resolution for even the most pixel-packed visuals.",
                    //    ImageUrl = "https://store.storeimages.cdn-apple.com/4982/as-images.apple.com/is/image/AppleInc/aos/published/images/H/MU/HMUA2/HMUA2?wid=572&hei=572&fmt=jpeg&qlt=95&op_usm=0.5,0.5&.v=1556839766759",
                    //    VotesCount = 14,
                    //    Score = 67.2,
                    //    PurchasesCount = 18,
                    //    InStock = 4,
                    //    CategoryId = "95c6e35c-7922-458f-9f65-1da8b2702bb2",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "Samsung SF350 LED Monitor",
                    //    Price = 549,
                    //    Description = "Featuring an ultra-slim and sleek profile the Samsung SF350 monitor measures less than 0.4inch thick. Make a stylish statement while staying productive with the 24 inch screen.The simple circular stand will add a modern look to your space.",
                    //    ImageUrl = "https://image-us.samsung.com/SamsungUS/home/computing/monitors/flat-monitors/pd/ls24f350fhnxza/gallery/S27F350_1_102017.jpg?$product-details-jpg$",
                    //    VotesCount = 17,
                    //    Score = 78.2,
                    //    PurchasesCount = 23,
                    //    InStock = 16,
                    //    CategoryId = "95c6e35c-7922-458f-9f65-1da8b2702bb2",
                    //});

                    ////TVs

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "Samsung Class Q900 QLED Smart 8K UHD TV ",
                    //    Price = 5999.99M,
                    //    Description = "Samsung's best TV ever combines true-to-life 8K HDR picture quality, AI-powered intelligent upscaling², stunning design, and smart home ready features, for a revolutionary TV experience.",
                    //    ImageUrl = "https://image-us.samsung.com/SamsungUS/home/televisions-and-home-theater/tvs/qled-8k-tv/pdp/qn98q900rbfxza/gallery-65-75/01_65-75in_Q900R_Front-Logo_Black.jpg?$product-details-jpg$",
                    //    VotesCount = 177,
                    //    Score = 867.3,
                    //    PurchasesCount = 212,
                    //    InStock = 13,
                    //    CategoryId = "a4ea672a-1ecc-46c4-be98-13e376f4f3b3",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "Samsung Class Q7CN QLED Curved Smart 4K UHD TV",
                    //    Price = 4999.99M,
                    //    Description = "The beauty of the Q7C arc is a visual spectacle. Equipped with the features of the Q7 Flat, but with a bend for immersive viewing,  it combines Q Color and an anti-glare screen for a dazzling display unlike any other.",
                    //    ImageUrl = "https://image-us.samsung.com/SamsungUS/home/televisions-and-home-theater/tvs/qled-tvs/pdp/qn65q7cnafxza/gallery/01-QN65Q7CNAFXZA_004_L-Perspective_Eclipse-Silver-v01-20180223.jpg?$product-details-jpg$",
                    //    VotesCount = 14,
                    //    Score = 67.2,
                    //    PurchasesCount = 16,
                    //    InStock = 6,
                    //    CategoryId = "a4ea672a-1ecc-46c4-be98-13e376f4f3b3",
                    //});

                    ////Accessories

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "Razer Naga Trinity",
                    //    Price = 289,
                    //    Description = "Experience the power of total control in your hand, no matter what game you play. Designed to provide you that edge you need in MOBA/MMO gameplay, the Razer Naga Trinity lets you configure your mouse for everything from weapons to build customizations so you’ll always be ahead of the competition.",
                    //    ImageUrl = "https://d4kkpd69xt9l7.cloudfront.net/sys-master/root/he1/h79/8909576994846/Razer_Naga_Trinity_09.jpg",
                    //    VotesCount = 21,
                    //    Score = 96.6,
                    //    PurchasesCount = 23,
                    //    InStock = 27,
                    //    CategoryId = "ad3e8c65-9136-4012-8163-d905e1ef8a2a",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "Logitech M171",
                    //    Price = 29.99M,
                    //    Description = "Strong, consistent wireless connection from distances up to 10-meters (33-feet) away. With virtually no delays or dropouts, you’ll work and play with confidence.",
                    //    ImageUrl = "https://assets.logitech.com/assets/64193/32/m170-reliable-wireless-connectivity.png",
                    //    VotesCount = 183,
                    //    Score = 713.7,
                    //    PurchasesCount = 223,
                    //    InStock = 154,
                    //    CategoryId = "ad3e8c65-9136-4012-8163-d905e1ef8a2a",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "JBL LIVE 650BTNC",
                    //    Price = 199.99M,
                    //    Description = "Wireless Over-Ear NC Headphones. 40mm drivers and a sound signature that can be found in the most famous venues all around the world.",
                    //    ImageUrl = "https://www.jbl.com/dw/image/v2/AAUJ_PRD/on/demandware.static/-/Sites-masterCatalog_Harman/default/dw1ba5451f/JBL_LIVE650BTNC_Product-Image_Hero_Black_071_x1-1605x1605px.jpg?sw=626&sh=626&sm=fit&sfrm=png",
                    //    VotesCount = 148,
                    //    Score = 636.4,
                    //    PurchasesCount = 178,
                    //    InStock = 53,
                    //    CategoryId = "ad3e8c65-9136-4012-8163-d905e1ef8a2a",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "Sony MDR-XB550AP ",
                    //    Price = 79,
                    //    Description = "Let the music flow through you with the power of EXTRA BASS. And choose from four vibrant colors to best match your style.",
                    //    ImageUrl = "https://www.sony.com/image/3412d62cd8e8cdb24483ab1d73044d82?fmt=pjpeg&wid=1014&hei=396&bgcolor=F1F5F9&bgc=F1F5F9",
                    //    VotesCount = 43,
                    //    Score = 180.6,
                    //    PurchasesCount = 87,
                    //    InStock = 25,
                    //    CategoryId = "ad3e8c65-9136-4012-8163-d905e1ef8a2a",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "Razer BlackWidow Lite",
                    //    Price = 359.99M,
                    //    Description = "On average, you’re bound to spend 1/3 of your life working. It’s time to reconsider the tools you use every day—meet the Razer BlackWidow Lite. It melds the fast responsiveness for gaming with toned down features to be subtle for the office. High performance keys meets o-ring sound dampeners, and true white LED backlighting, keeping you focused and productive as you type away even into the late hours.",
                    //    ImageUrl = "https://assets2.razerzone.com/images/og-image/BWlite-OG.jpg",
                    //    VotesCount = 27,
                    //    Score = 126.9,
                    //    PurchasesCount = 39,
                    //    InStock = 11,
                    //    CategoryId = "ad3e8c65-9136-4012-8163-d905e1ef8a2a",
                    //});

                    //context.Products.Add(new Product()
                    //{
                    //    Name = "A4Tech KD-600L Black",
                    //    Price = 49.99M,
                    //    Description = "Illuminated keyboard with smart backlighting: Type comfortably, even in low light.",
                    //    ImageUrl = "http://a4tech.com/alanUpload/colorImg/img/201712/1405070877116684.jpg",
                    //    VotesCount = 188,
                    //    Score = 733.2,
                    //    PurchasesCount = 247,
                    //    InStock = 59,
                    //    CategoryId = "ad3e8c65-9136-4012-8163-d905e1ef8a2a",
                    //});

                    //context.Comments.Add(new Comment()
                    //{
                    //    Content = "Very nice.",
                    //    LastModified = DateTime.UtcNow,
                    //    ProductId = "25efbf72-7b16-4b09-8c6d-c6ea8d504838"
                    //});

                    //context.Comments.Add(new Comment()
                    //{
                    //    Content = "Veery nice.",
                    //    LastModified = DateTime.UtcNow,
                    //    ProductId = "25efbf72-7b16-4b09-8c6d-c6ea8d504838"
                    //});

                    //context.SaveChanges();
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
