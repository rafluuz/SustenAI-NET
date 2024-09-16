using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace SustenAI.Models.Mappings
{
    public class ArquivoMapping : IEntityTypeConfiguration<Arquivo>
    {
        public void Configure(EntityTypeBuilder<Arquivo> builder)
        {
            builder.ToTable("sustenAI_arquivos");

            builder.HasKey(a => a.IdArq);

            builder.Property(a => a.IdArq)
                .HasColumnName("id_arq")
                .IsRequired();

            builder.Property(a => a.NomeArq)
                .HasColumnName("nome_arq")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(a => a.TipoArq)
                .HasColumnName("tipo_arq")
                .HasMaxLength(50);

            builder.Property(a => a.DataUpload)
                .HasColumnName("data_upload")
                .IsRequired();

        }
    }
}