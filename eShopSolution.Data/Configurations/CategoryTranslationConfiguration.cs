using eShopSolution.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace eShopSolution.Data.Configurations
{
    public class CategoryTranslationConfiguration : IEntityTypeConfiguration<CategoryTranslation>
    {
        public void Configure(EntityTypeBuilder<CategoryTranslation> builder)
        {
            builder.ToTable("CategoryTranslations");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id).UseIdentityColumn();
            builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
            builder.Property(c => c.SeoAlias).IsRequired().HasMaxLength(200);
            builder.Property(c => c.SeoDescription).IsRequired().HasMaxLength(200);
            builder.Property(c => c.SeoTitle).IsRequired().HasMaxLength(200);
            builder.Property(c => c.LanguageId).IsUnicode(false).HasMaxLength(5);
            builder.HasOne(l => l.Language).WithMany(ct => ct.CategoryTranslations).HasForeignKey(ct => ct.LanguageId);
            builder.HasOne(c => c.Category).WithMany(ct => ct.CategoryTranslations).HasForeignKey(ct => ct.CategoryId);
        }
    }
}
