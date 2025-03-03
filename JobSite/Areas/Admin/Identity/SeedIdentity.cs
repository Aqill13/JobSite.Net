using EntityLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace JobSite.Areas.Admin.Identity
{
    public static class SeedIdentity
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedService = app.ApplicationServices.CreateScope();

            var serciceProvider = scopedService.ServiceProvider;

            await Seed(serciceProvider);

            return app;
        }
        public static async Task Seed(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<Users>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var admin = new Users()
            {
                Fullname = "Aqil Kalbiyev",
                UserName = "admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "0503730035",
                Status = "Online"
            };

            if (await userManager.FindByNameAsync(admin.UserName) == null)
            {
                var createAdmin = await userManager.CreateAsync(admin, "admin01");
            }

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                IdentityRole role = new IdentityRole()
                {
                    Name = "admin"
                };
                var createRole = await roleManager.CreateAsync(role);
                var result = await userManager.AddToRoleAsync(admin, role.Name);
            }
        }
    }
}
