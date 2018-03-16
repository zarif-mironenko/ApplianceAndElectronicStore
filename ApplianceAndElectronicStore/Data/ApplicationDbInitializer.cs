using ApplianceAndElectronicStore.Infrastructure;
using ApplianceAndElectronicStore.Models;
using ApplianceAndElectronicStore.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ApplianceAndElectronicStore.Data
{
    public class ApplicationDbInitializer : DbInitializer<ApplicationDbContext>
    {
        readonly IConfiguration configuration;
        readonly UserManager<ApplicationUser> userManager;
        readonly RoleManager<IdentityRole> roleManager;

        readonly string roleAdmin = "admin",
                        roleManag = "manager",
                        roleUser = "user";

        public ApplicationDbInitializer(ApplicationDbContext contextService,
                                        IConfiguration config,
                                        UserManager<ApplicationUser> userManager,
                                        RoleManager<IdentityRole> roleManager)
            : base(contextService)
        {
            configuration = config;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }

        public override void SeedData()
        {
            context.Database.Migrate();

            // Создаем роли
            CreateRoles();

            // Создаем пользователей
            CreateUsers();

            context.SaveChanges();

            if (context.Products.Any()) return;

            AddManufacturers();
            AddCategories();
            AddProductSpecifications();

            context.SaveChanges();

            AddProducts();

            AddCategoriesProductSpecifications(categoryId: 1);
            AddCategoriesProductSpecifications(categoryId: 2);

            context.SaveChanges();
        }

        void CreateRoles()
        {
            CreateRole(roleAdmin);
            CreateRole(roleManag);
            CreateRole(roleUser);
        }

        void CreateUsers()
        {
            var testUsersSection = configuration.GetSection("TestUsers");

            if (testUsersSection.Exists()) {
                var userInfoes = testUsersSection.Get<UserInfo[]>();

                foreach (var item in userInfoes) CreateUser(item);
            } else {
                var userInfo = new UserInfo {
                    Login = "admin@gmail.com",
                    Password = "Aa1234",
                    Role = roleAdmin
                };

                CreateUser(userInfo);
            } // if
        }

        void CreateRole(string roleName)
        {
            if (!roleManager.RoleExistsAsync(roleName).Result)
                roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
        }

        void CreateUser(UserInfo userInfo)
        {
            ApplicationUser user = userManager.FindByNameAsync(userInfo.Login).Result;

            if (user != null) return;

            user = new ApplicationUser { UserName = userInfo.Login, Email = userInfo.Login };

            userManager.CreateAsync(user, userInfo.Password).Wait();
            userManager.AddToRoleAsync(user, userInfo.Role).Wait();

            if (userInfo.Role == "user") {
                context.Customers.Add(new Customer {
                    ApplicationUserId = userManager.FindByNameAsync(userInfo.Login).Result.Id,
                });
            } // if
        }

        void AddCategories()
        {
            context.Categories.AddRange(
                 new Category { Name = "Ноутбуки", NameForUrl = "Notebooks" },
                 new Category { Name = "Телевизоры", NameForUrl = "TVs" },
                 new Category { Name = "Стиральные машины", NameForUrl = "WashingMachines" },
                 new Category { Name = "Холодильники", NameForUrl = "Fridges" }
            );
        }

        void AddManufacturers()
        {
            context.Manufacturers.AddRange(
                 new Manufacturer { Name = "Asus" },
                 new Manufacturer { Name = "Acer" },
                 new Manufacturer { Name = "LG" },
                 new Manufacturer { Name = "Samsung" }
            );
        }

        void AddProducts()
        {
            Random rnd = new Random();

            string imagesFolder = "/images/products/";

            for (int i = 1; i <= 40; i++) {
                int rndCategoryId = rnd.Next(1, 5),
                    rndManufactId = 0;

                string imageFile = "";

                switch (rndCategoryId) {
                    case 1:
                        imageFile = Path.Combine(imagesFolder, "product-photo-228x228.jpeg");
                        rndManufactId = rnd.Next(1, 3);
                        break;

                    case 2:
                        imageFile = Path.Combine(imagesFolder, "TV-BBK 50LEX-5026 FT2C-500x500.jpeg");
                        rndManufactId = rnd.Next(3, 5);
                        break;

                    case 3:
                        imageFile = Path.Combine(imagesFolder, "WM LG FH096ND3 500x500.jpg");
                        rndManufactId = rnd.Next(3, 5);
                        break;

                    case 4:
                        imageFile = Path.Combine(imagesFolder, "Fridge SAMSUNG RT46K6340EF 500x500.jpg");
                        rndManufactId = rnd.Next(3, 5);
                        break;
                } // switch

                var product = new Product {
                    ProductModel = $"Продукт {i}",
                    ImagePath = imageFile,
                    CategoryId = rndCategoryId,
                    ManufacturerId = rndManufactId,
                    Description = $"Описание продукта {i}",
                    Price = rnd.Next(15000, 80001),
                    Quantity = rnd.Next(10)
                };

                AddProductSpecificationsValues(product);

                context.Products.Add(product);
            } // for i
        }

        void AddProductSpecifications()
        {
            context.ProductSpecifications.AddRange(
                new ProductSpecification {
                    Name = "Диагональ",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 1,
                            NumberInOrder = 1
                        },
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 2,
                            NumberInOrder = 1
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Процессор",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 1,
                            NumberInOrder = 2
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Оперативная память",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 1,
                            NumberInOrder = 3
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Объём жеского диска",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 1,
                            NumberInOrder = 4
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Видеокарта",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 1,
                            NumberInOrder = 5
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Интернет/передача данных",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 1,
                            NumberInOrder = 6
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Привод",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 1,
                            NumberInOrder = 7
                        }
                    }
                },
                new ProductSpecification {
                    Name = "ПО",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 1,
                            NumberInOrder = 8
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Цвет",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 1,
                            NumberInOrder = 9
                        },
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 2,
                            NumberInOrder = 9
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Стандарт HDTV",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 2,
                            NumberInOrder = 2
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Тип экрана",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 2,
                            NumberInOrder = 3
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Разрешение экрана",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 2,
                            NumberInOrder = 4
                        }
                    }
                },
                new ProductSpecification {
                    Name = "3D",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 2,
                            NumberInOrder = 5
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Звук",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 2,
                            NumberInOrder = 6
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Smart TV",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 2,
                            NumberInOrder = 7
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Wi-Fi",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 2,
                            NumberInOrder = 8
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Тюнер",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 2,
                            NumberInOrder = 10
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Интерфейсы",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 2,
                            NumberInOrder = 11
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Габариты",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 2,
                            NumberInOrder = 12
                        }
                    }
                },
                new ProductSpecification {
                    Name = "Дополнительно",
                    ProductSpecificationNumbersInOrder = new List<ProductSpecificationNumberInOrder> {
                        new ProductSpecificationNumberInOrder {
                            CategoryId = 2,
                            NumberInOrder = 13
                        }
                    }
                }
            );
        }

        void AddProductSpecificationsValues(Product product)
        {
            switch (product.CategoryId) {
                case 1:
                    AddNotebookSpecsValues(product);
                    break;

                case 2:
                    AddTvSpecsValues(product);
                    break;

                case 3:
                    break;

                case 4:
                    break;
            } // switch
        }

        void AddNotebookSpecsValues(Product product)
        {
            product.ProductSpecificationValues.AddRange(new[] {
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Диагональ").Id,
                            Value = "15.6\""
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Процессор").Id,
                            Value = "Intel Celeron N3060 2x1.6 ГГц"
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Оперативная память").Id,
                            Value = "4 Гб"
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Объём жеского диска").Id,
                            Value = "500 Гб"
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Видеокарта").Id,
                            Value = "HD Graphics 400"
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Интернет/передача данных").Id,
                            Value = "Wi-Fi,BT"
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Привод").Id,
                            Value = "DVD-RW"
                        }
                        //new ProductSpecificationValue {
                        //    ProductId = product.Id,
                        //    ProductSpecificationId = 
                                //context.ProductSpecifications
                                //    .First(ps => ps.Name == "ПО").Id,
                        //    Value = "Linux"
                        //},
                        //new ProductSpecificationValue {
                        //    ProductId = product.Id,
                        //    ProductSpecificationId = 
                                //context.ProductSpecifications
                                //    .First(ps => ps.Name == "Цвет").Id,
                        //    Value = "черный"
                        //},
            });
        }

        void AddTvSpecsValues(Product product)
        {
            product.ProductSpecificationValues.AddRange(new[] {
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Диагональ").Id,
                            Value = "50\""
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Стандарт HDTV").Id,
                            Value = "1080i, 1080p"
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Тип экрана").Id,
                            Value = "LED"
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Разрешение экрана").Id,
                            Value = "1920x1080"
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "3D").Id,
                            Value = "-"
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Звук").Id,
                            Value = "16 Вт"
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Smart TV").Id,
                            Value = "Smart TV (Android)"
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Тюнер").Id,
                            Value = "DVB-T/T2/С"
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(p => p.Name == "Wi-Fi").Id,
                            Value = "Wi-Fi"
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Интерфейсы").Id,
                            Value = "2xHDMI,VGA,3xUSB,RJ-45"
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Цвет").Id,
                            Value = "черный"
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Габариты").Id,
                            Value = "1134x704x250мм"
                        },
                        new ProductSpecificationValue {
                            ProductId = product.Id,
                            ProductSpecificationId =
                                context.ProductSpecifications
                                    .First(ps => ps.Name == "Дополнительно").Id,
                            Value = "-"
                        }
            });
        }

        void AddCategoriesProductSpecifications(int categoryId)
        {
            Category category = context.Categories.Find(categoryId);

            var catProdSpecs = context.ProductSpecificationNumbersInOrder
                .Where(n => n.CategoryId == category.Id)
                .Select(n => new CategoryProductSpecification {
                    CategoryId = category.Id,
                    ProductSpecificationId = n.ProductSpecificationId
                }).ToList();

            category.CategoriesProductSpecifications.AddRange(catProdSpecs);
        }
    }
}
