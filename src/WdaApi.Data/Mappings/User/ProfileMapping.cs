using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WdaApi.Business.Models;

namespace WdaApi.Data.Mappings
{
    public class ProfileMapping : IEntityTypeConfiguration<ProfileUser>
    {
        public void Configure(EntityTypeBuilder<ProfileUser> builder)
        {
            builder.HasKey(c => c.Id);           

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(255)");


            builder.ToTable("profiles");
        }
    }
}
