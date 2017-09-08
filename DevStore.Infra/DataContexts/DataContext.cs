using DevStore.Domain;
using DevStore.Infra.Mappings;
using System.Data.Entity;

namespace DevStore.Infra.DataContexts {

    [DbConfigurationType(typeof(MySql.Data.Entity.MySqlEFConfiguration))]
    public class DataContext : DbContext {

        //  Informa o nome da string de conexão
        public DataContext() : base("DevStoreConnectionString") {
            // Adiciona o inicializador 
            Database.SetInitializer<DataContext>(new DataContextInit());
        }

        //  Gera minhas tabelas através das minhas classes de domínio
        //  Seu tivessem mais classes no projeto, seriam adicionadas aqui
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        //  No momento da criação do meu modelo, adicione essas configurações
        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Configurations.Add(new ProductMap());
            modelBuilder.Configurations.Add(new CategoryMap());
            base.OnModelCreating(modelBuilder);
        }
    }

    //  Inicializa o banco de dados, apaga e cria a base se houver mudanças
    public class DataContextInit : DropCreateDatabaseIfModelChanges<DataContext> {
        //  Alimenta o banco de dados
        protected override void Seed(DataContext context) {
            context.Categories.Add(new Category { Id = 1, Title = "Games"});
            context.Categories.Add(new Category { Id = 2, Title = "Informática" });
            context.Categories.Add(new Category { Id = 3, Title = "Música" });
            context.Products.Add(new Product { Id = 1, CategoryId = 1, Title = "Final Fantasy XV", Price = 199.90m, IsActive = true});
            context.Products.Add(new Product { Id = 2, CategoryId = 1, Title = "Sniper Ghost Warrior 3", Price = 159.90m, IsActive = true });
            context.Products.Add(new Product { Id = 3, CategoryId = 1, Title = "Counter Striker: GO", Price = 29.90m, IsActive = true });
            //  Persiste no banco de dados
            context.SaveChanges();
        }
    }
}
