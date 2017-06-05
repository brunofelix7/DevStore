using DevStore.Domain;
using System.Data.Entity.ModelConfiguration;

namespace DevStore.Infra.Mappings {

    public class CategoryMap : EntityTypeConfiguration<Category>{

        public CategoryMap() {

            //  Define o nome da tabela
            ToTable("Category");

            //  Diz que esse campo é a chave primária da tabela
            HasKey(x => x.Id);

            //  Configurando propriedades, tamanho máximo 60 e é NOT NULL
            Property(x => x.Title).HasMaxLength(60).IsRequired();
        }
    }
}
