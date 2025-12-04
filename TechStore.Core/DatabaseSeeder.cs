using System.Collections.Generic;
using TechStore.Core.Interfaces;
using TechStore.Models;

namespace TechStore.Core
{
    public class DatabaseSeeder
    {
        public DatabaseSeeder(IProductBusinessService businessService) 
        {
            SeedSampleData(businessService);
        }

        private void SeedSampleData(IProductBusinessService businessService) 
        {
            // Используем метод GetAll из бизнес-сервиса
            if (businessService.GetAll().Count > 0) return;

            var sampleProducts = new List<Product>
            {
                new Product(0, "iPhone 15", 99999m, "Смартфон", 10, "Apple"),
                new Product(0, "Samsung Galaxy S24", 89999m, "Смартфон", 5, "Samsung"),
                new Product(0, "Xiaomi Robot Vacuum", 29999m, "Пылесос", 3, "Xiaomi"),
                new Product(0, "DEXP Холодильник", 45999m, "Холодильник", 0, "DEXP"),
                new Product(0, "MacBook Air", 129999m, "Ноутбук", 7, "Apple")
            };

            foreach (var product in sampleProducts)
                businessService.Create(product); // ← Используем Create из бизнес-сервиса
        }
    }
}