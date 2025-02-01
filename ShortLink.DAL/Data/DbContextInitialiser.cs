using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShortLink.DAL.Identity.Enums;
using ShortLink.Domain.Entities;
using ILogger = Serilog.ILogger;

namespace ShortLink.DAL.Data
{
    public class DbContextInitialiser
    {
        private readonly ApplicationDbContext _context;        
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;
        private readonly ILogger _logger;

        public DbContextInitialiser(ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager, ILogger logger)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task InitialiseAsync()
        {
            _logger.Information("Initializing database...");
            await _context.Database.MigrateAsync();
            _logger.Information("Database initialized successfully.");
        }

        public async Task SeedAsync()
        {
            _logger.Information("Seeding roles...");
            foreach (var role in Enum.GetNames(typeof(Role)))
            {
                if (!await _roleManager.RoleExistsAsync(role))
                {
                    _logger.Information($"Creating role: {role}");
                    await _roleManager.CreateAsync(new IdentityRole<Guid> { Name = role });
                }
            }

            var adminEmail = "admin@test.com";

            _logger.Information($"Checking if admin user exists: {adminEmail}");
            var adminUser = await _userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                _logger.Information($"Creating admin user: {adminEmail}");
                adminUser = new User
                {
                    UserName = adminEmail,
                    Email = adminEmail
                };

                var result = await _userManager.CreateAsync(adminUser, "AdminPassword!1");

                if (result.Succeeded)
                {
                    _logger.Information("Admin user created successfully.");
                    await _userManager.AddToRoleAsync(adminUser, Role.Admin.ToString());
                    _logger.Information("Admin user assigned to Admin role.");
                }
            }

            _logger.Information($"Checking if user exists in Users table: {adminEmail}");
            var theSameUserInDatabase = await _context.Users.FirstOrDefaultAsync(u => u.Email == adminEmail);

            if (theSameUserInDatabase == null)
            {
                _logger.Information("Converting ApplicationUser to User entity and adding to database.");
                var userConvertFromAppUserToUser = new User
                {
                    Email = adminUser.Email
                };

                await _context.AddAsync(userConvertFromAppUserToUser);
                await _context.SaveChangesAsync();
                _logger.Information("User entity added to database successfully.");
            }
        }
    }
}
