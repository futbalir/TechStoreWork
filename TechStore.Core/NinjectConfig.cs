using Ninject.Modules;
using TechStore.Core.Interfaces;
using TechStore.DataAccessLayer.Dapper;
using TechStore.DataAccessLayer.EF;

namespace TechStore.Core
{
    /// <summary>
    /// Конфигурация DI контейнера Ninject
    /// Реализует принцип DIP - инверсии зависимостей
    /// </summary>

    public class NinjectConfig : NinjectModule
    {
        private readonly bool _useEntityFramework;

        /// <summary>
        /// Инициализирует конфигурацию DI контейнера
        /// </summary>
        /// <param name="useEntityFramework">true - использовать Entity Framework, false - Dapper</param>
        public NinjectConfig(bool useEntityFramework = false)
        {
            _useEntityFramework = useEntityFramework;
        }

        /// <summary>
        /// Загружает привязки зависимостей в контейнер
        /// </summary>
        public override void Load()
        {
            string connectionString = "Server=DESKTOP-3MM42VI\\SQLEXPRESS;Database=TechStoreDb;Trusted_Connection=true;TrustServerCertificate=true;";

            
            Bind<string>().ToConstant(connectionString).InSingletonScope();

            
            if (_useEntityFramework)
            {
                Bind<AppDbContext>().ToSelf()
                    .WithConstructorArgument("connectionString", connectionString);
                Bind<IProductRepository>().To<EntityRepository>();
            }
            else
            {
                Bind<IProductRepository>().To<DapperRepository>();
            }

            
            Bind<IProductBusinessService>().To<ProductBusinessService>();


            
            Bind<DatabaseSeeder>().ToSelf().InSingletonScope();
        }
    }
}