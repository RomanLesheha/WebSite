using WebSite.Areas.Identity.Data;
using WebSite.Models.Constants;
using Microsoft.AspNetCore.Identity;
using WebSite.Models;
using Microsoft.EntityFrameworkCore;

namespace WebSite.Data
{
    public class DBSeeder
    {
        public static async Task SeedDefaultData(IServiceProvider service)
        {
            var userMgr = service.GetService<UserManager<ApplicationUser>>();
            var roleMgr = service.GetService<RoleManager<IdentityRole>>();
            //adding some roles to db
            await roleMgr.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleMgr.CreateAsync(new IdentityRole(Roles.User.ToString()));

            // create admin user

            var admin = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            var userInDb = await userMgr.FindByEmailAsync(admin.Email);
            if (userInDb is null)
            {
                await userMgr.CreateAsync(admin, "Admin@123");
                await userMgr.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
        }
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                if (context.Products.Any() || context.Categories.Any())
                {
                    context.Products.RemoveRange(context.Products);
                    context.Categories.RemoveRange(context.Categories);
                    context.SaveChanges();
                }


                var categories = new List<Category>
                {
                    new Category {Name = "Shirts", Products = new List<Product>(), Description = "Casual and formal shirts for men and women."},
                    new Category {Name = "Pants", Products = new List<Product>(), Description = "Stylish pants and trousers for all occasions."},
                    new Category {Name = "Dresses", Products = new List<Product>(), Description = "Elegant dresses for women."},
                    new Category {Name = "Outerwear", Products = new List<Product>(), Description = "Jackets, coats, and outer layers to keep you warm."},
                    new Category {Name = "Activewear", Products = new List<Product>(), Description = "Sporty and comfortable clothing for active lifestyles."},
                    new Category {Name = "Accessories", Products = new List<Product>(), Description = "Fashionable accessories to complement your outfit."},
                    new Category {Name = "Footwear", Products = new List<Product>(), Description = "Stylish shoes for every occasion."},
                    new Category {Name = "Undergarments", Products = new List<Product>(), Description = "Comfortable and supportive undergarments."},
                    new Category {Name = "Sleepwear", Products = new List<Product>(), Description = "Cozy sleepwear for a good night's rest."},
                    new Category {Name = "Special Occasion", Products = new List<Product>(), Description = "Dress up for special events with our exclusive collection."}
                };

                context.Categories.AddRange(categories);

                var products = new List<Product>
                {
                    new Product
                    {
                        Name = "Blue Casual Jeans",
                        Description = "Classic blue jeans for a stylish and casual look.",
                        Price = 799,
                        DiscountedPrice = 599,
                        Photo = "https://5.imimg.com/data5/YM/QG/MY-45492291/mens-blue-jeans-500x500.jpg",
                        CategoryId = 1,
                        Category = categories[1],
                        Variants = new List<ClothingVariant>
                        {
                            new ClothingVariant { Size = ClothingSize.S, Quantity = 20 },
                            new ClothingVariant { Size = ClothingSize.M, Quantity = 25 },
                            new ClothingVariant { Size = ClothingSize.L, Quantity = 30 }
                        }
                    },
                    new Product
                    {
                        Name = "Knitted V-Neck Sweater",
                        Description = "Stay warm and stylish with this knitted V-neck sweater. Length: S-60 cm; M-61 cm; L-62 cm; XL-63 cm. Shoulder Width: S-45 cm; M-46 cm; L-47 cm; XL-48 cm. Sleeve Length: S-58 cm; M-59 cm; L-59 cm; XL-60 cm. Chest Circumference: S-50 cm; M-52 cm; L-54 cm; XL-56 cm.",
                        Price = 699,
                        DiscountedPrice = 549,
                        Photo = "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcR12-JGO7oFabU_jaG66p1Pshsj_g2BjnD2iKrqF6_tjeegQV-e75gBvfIQFbL6vf7qTxL0-fXZcw15f6OZ4yc5AKQvs0nWytYSAFkC3mghCt5kMX-0CG5kgw&usqp=CAE",
                        CategoryId = 1,
                        Category = categories[0],
                        Variants = new List<ClothingVariant>
                        {
                            new ClothingVariant { Size = ClothingSize.S, Quantity = 12 },
                            new ClothingVariant { Size = ClothingSize.L, Quantity = 15 }
                        }
                    },
                    new Product
                    {
                        Name = "Floral Maxi Dress",
                        Description = "Make a statement with this elegant floral maxi dress. Length: S-140 cm; M-142 cm; L-144 cm; XL-146 cm. Shoulder Width: S-38 cm; M-39 cm; L-40 cm; XL-41 cm. Sleeve Length: Sleeveless. Chest Circumference: S-82 cm; M-86 cm; L-90 cm; XL-94 cm.",
                        Price = 1299,
                        DiscountedPrice = 999,
                        Photo = "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcSVPnYnAMYTNekRsvx_au8TehDNhkPt6tyJRrAKUBpAX1KFHiiuoACi5KEpd6rUMtel-A4fxb5YuwOjrOVhnFVB1rRDhg96Kvl-UWIy6-_LEXq0t-JKwCQR&usqp=CAE",
                        CategoryId = 3,
                        Category = categories[2],
                        Variants = new List<ClothingVariant>
                        {
                             new ClothingVariant { Size = ClothingSize.S, Quantity = 12 },
                            new ClothingVariant { Size = ClothingSize.M, Quantity = 10 },
                            new ClothingVariant { Size = ClothingSize.XL, Quantity = 12 }
                        }
                    },
                     new Product
                    {
                        Name = "Quilted Puffer Jacket",
                        Description = "Stay cozy and stylish in this quilted puffer jacket. Length: S-70 cm; M-72 cm; L-74 cm; XL-76 cm. Shoulder Width: S-48 cm; M-50 cm; L-52 cm; XL-54 cm. Sleeve Length: S-61 cm; M-62 cm; L-63 cm; XL-64 cm. Chest Circumference: S-54 cm; M-56 cm; L-58 cm; XL-60 cm.",
                        Price = 899,
                        Photo = "https://encrypted-tbn1.gstatic.com/shopping?q=tbn:ANd9GcQuJh_Q0jwHpUrXm-CQywr3R0Mrqci-SgArm6SVU13T7S6G8aZS1_cckOUMH5Cu7ei1hPBYOfWT_zZUK9UVO1TlFUwmtK8QAv0i_ODJDcQB&usqp=CAE0",
                        CategoryId = 4,
                        Category = categories[3],
                        Variants = new List<ClothingVariant>
                        {
                            new ClothingVariant { Size = ClothingSize.M, Quantity = 15 },
                            new ClothingVariant { Size = ClothingSize.XL, Quantity = 20 },
                            new ClothingVariant { Size = ClothingSize.L, Quantity = 15 }
                        }
                    },
                    new Product
                    {
                        Name = "Cropped Wide-Leg Pants",
                        Description = "Elevate your casual style with these cropped wide-leg pants. Length: S-85 cm; M-87 cm; L-89 cm; XL-91 cm. Waist Circumference: S-68 cm; M-72 cm; L-76 cm; XL-80 cm. Hip Circumference: S-94 cm; M-98 cm; L-102 cm; XL-106 cm.",
                        Price = 499,
                        DiscountedPrice = 399,
                        Photo = "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcQp8cTJIRXaeaeyJgM_wRyvZHR0ER21c30x0WtfBe8CrePfMaekjxmM-2GAOzm17IkvfgRRONAkEewxkfskS1-gFvfAssPbRXQCd9ResDhGakStaqWFMlZ0&usqp=CAE",
                        CategoryId = 2,
                        Category = categories[1],
                        Variants = new List<ClothingVariant>
                        {
                            new ClothingVariant { Size = ClothingSize.S, Quantity = 18 },
                            new ClothingVariant { Size = ClothingSize.L, Quantity = 22 }
                        }
                    },
                       new Product
                        {
                            Name = "Classic Wool Blend Coat",
                            Description = "Add sophistication to your wardrobe with this classic wool blend coat. Length: S-85 cm; M-87 cm; L-89 cm; XL-91 cm. Shoulder Width: S-40 cm; M-42 cm; L-44 cm; XL-46 cm. Sleeve Length: S-60 cm; M-61 cm; L-62 cm; XL-63 cm. Chest Circumference: S-90 cm; M-94 cm; L-98 cm; XL-102 cm.",
                            Price = 1299,
                            DiscountedPrice = 999,
                            Photo = "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcS3q5Vt5bmHNO31uv6KdkoArOg0Z9UqI4VJNT7-mOE5BYEtiNvmhR08nbHxvlNoPVtBPk2k2gM8umPeY0pexFYUE5CnEYeq7vI37ftRJq1oablpfpgqhXVkvw&usqp=CAE",
                            CategoryId = 4,
                            Category = categories[3],
                            Variants = new List<ClothingVariant>
                            {
                                new ClothingVariant { Size = ClothingSize.M, Quantity = 15 },
                                new ClothingVariant { Size = ClothingSize.XL, Quantity = 2 },
                                new ClothingVariant { Size = ClothingSize.S, Quantity = 10 },
                                new ClothingVariant { Size = ClothingSize.L, Quantity = 15 }
                            }
                        },
                    new Product
                    {
                        Name = "High-Neck Cable Knit Sweater",
                        Description = "Stay warm in style with this high-neck cable knit sweater. Length: S-65 cm; M-66 cm; L-67 cm; XL-68 cm. Shoulder Width: S-42 cm; M-44 cm; L-46 cm; XL-48 cm. Sleeve Length: S-58 cm; M-59 cm; L-60 cm; XL-61 cm. Chest Circumference: S-92 cm; M-96 cm; L-100 cm; XL-104 cm.",
                        Price = 799,
                        Photo = "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcR_VHuz5n7cC8BUrOdPM2q_NoI7M_t03VbtEhGjSmuWuWD7DoOmjYsKKeuiVaS6g1TGzMMlwC7I5_uJy_UI3OZQRCXBWfwArYw_hdIqqb3s-LssNdF8yPOI&usqp=CAE",
                        CategoryId = 1,
                        Category = categories[0],
                        Variants = new List<ClothingVariant>
                        {
                            new ClothingVariant { Size = ClothingSize.M, Quantity = 12 },
                            new ClothingVariant { Size = ClothingSize.S, Quantity = 14 },
                            new ClothingVariant { Size = ClothingSize.XL, Quantity = 18 }
                        }
                    },
                    new Product
                    {
                        Name = "Casual Striped Shirt",
                        Description = "Achieve a laid-back look with this casual striped shirt. Length: S-70 cm; M-72 cm; L-74 cm; XL-76 cm. Shoulder Width: S-48 cm; M-50 cm; L-52 cm; XL-54 cm. Sleeve Length: S-63 cm; M-64 cm; L-65 cm; XL-66 cm. Chest Circumference: S-52 cm; M-54 cm; L-56 cm; XL-58 cm.\n\n" +
                                      "Specifications:\n" +
                                      "Brand: CasualStyle\n" +
                                      "Material: 100% Cotton\n" +
                                      "Season: All Season\n" +
                                      "Color: Blue and White Stripes\n" +
                                      "Fit: Regular\n" +
                                      "Pattern: Striped\n" +
                                      "Country of Manufacture: Turkey\n" +
                                      "Weight: 0.2 kg",
                        Price = 499,
                        DiscountedPrice = 399,
                        Photo = "https://encrypted-tbn2.gstatic.com/shopping?q=tbn:ANd9GcQeNKyDfmu5usmntfveMCNwq68hMoXhBj_revKMpT1GiYdYmPwSlfr8ygcLHwGAlow9iJlC7iqjCwLLch3GKd9sqPwP2lceSHfaBlLmFMw&usqp=CAE",
                        CategoryId = 1,
                        Category = categories[0],
                        Variants = new List<ClothingVariant>
                        {
                            new ClothingVariant { Size = ClothingSize.M, Quantity = 20 },
                            new ClothingVariant { Size = ClothingSize.L, Quantity = 25 }
                        }
                    },
                    new Product
                    {
                        Name = "Elegant Lace Cocktail Dress",
                        Description = "Make a statement at your next event with this elegant lace cocktail dress. Length: S-90 cm; M-92 cm; L-94 cm; XL-96 cm. Shoulder Width: S-36 cm; M-38 cm; L-40 cm; XL-42 cm. Sleeve Length: Sleeveless. Chest Circumference: S-80 cm; M-84 cm; L-88 cm; XL-92 cm.\n\n" +
                                      "Specifications:\n" +
                                      "Brand: Glamorous\n" +
                                      "Material: Lace\n" +
                                      "Season: Special Occasion\n" +
                                      "Color: Black\n" +
                                      "Fit: Slim\n" +
                                      "Pattern: Floral Lace\n" +
                                      "Country of Manufacture: China\n" +
                                      "Weight: 0.2 kg",
                        Price = 1499,
                        Photo = "https://encrypted-tbn0.gstatic.com/shopping?q=tbn:ANd9GcTkwnFSYTreXH-bOgkAk19v7W5O7T0gRtxn7MT5G3Ljy5b66YgtDB9rNZjb0wmNVieXfJq2y-EStI8yh4wHKd5AH8W_DYtrwX2V0O3ofJditnsQM1Bnip8pLg&usqp=CAE",
                        CategoryId = 3,
                        Category = categories[2],
                        Variants = new List<ClothingVariant>
                        {
                            new ClothingVariant { Size = ClothingSize.S, Quantity = 15 },
                            new ClothingVariant { Size = ClothingSize.L, Quantity = 20 }
                        }
                    },
                    new Product
                    {
                        Name = "Sporty Hooded Sweatshirt",
                        Description = "Stay comfortable and stylish with this sporty hooded sweatshirt. Length: S-68 cm; M-70 cm; L-72 cm; XL-74 cm. Shoulder Width: S-46 cm; M-48 cm; L-50 cm; XL-52 cm. Sleeve Length: S-59 cm; M-60 cm; L-61 cm; XL-62 cm. Chest Circumference: S-54 cm; M-56 cm; L-58 cm; XL-60 cm.\n\n" +
                                      "Specifications:\n" +
                                      "Brand: ActiveLife\n" +
                                      "Material: 80% Cotton, 20% Polyester\n" +
                                      "Season: All Season\n" +
                                      "Color: Grey\n" +
                                      "Fit: Relaxed\n" +
                                      "Pattern: Solid\n" +
                                      "Country of Manufacture: Vietnam\n" +
                                      "Weight: 0.2 kg",
                        Price = 699,
                        Photo = "https://encrypted-tbn3.gstatic.com/shopping?q=tbn:ANd9GcQ9beIqUPNme8-7IHMsPeOAzUryxXnPa_7uD8ybMMHdVdIiuf94fbvuyAcsJ2InCLQP4qZjAqfMYmXKcv21Fq8yrbEV-ZRF1r2COJQ2Q6fZ&usqp=CAE",
                        CategoryId = 1,
                        Category = categories[0],
                        Variants = new List<ClothingVariant>
                        {
                            new ClothingVariant { Size = ClothingSize.M, Quantity = 18 },
                            new ClothingVariant { Size = ClothingSize.XL, Quantity = 22 }
                        }
                    },
                };
                
                context.Products.AddRange(products);

                context.SaveChanges();
            }
        }
    }
}
