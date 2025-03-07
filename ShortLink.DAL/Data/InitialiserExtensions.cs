﻿using Microsoft.Extensions.DependencyInjection;

namespace ShortLink.DAL.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDbAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<DbContextInitialiser>();

            await initialiser.InitialiseAsync();

            await initialiser.SeedAsync();
        }
    }
}
