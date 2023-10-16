using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Category;

namespace TaskPlusPlus.Persistence.Configurations;
public class CategoryConfiguration  : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        var nameConverter = new ValueConverter<CategoryName, string>(
            name => name.Value,
            value => CategoryName.Create(value).Value);
        var colorHexConverter = new ValueConverter<ColorHex, string>(
            colorHex => colorHex.Value,
            value => ColorHex.Create(value).Value);
        var userIdConverter = new ValueConverter<UserId, string>(
            userId => userId.Value,
            value => UserId.Create(value).Value);
        var idConverter = new ValueConverter<CategoryId, ulong>(
            categoryId => categoryId.Value,
            value => new CategoryId(value));


        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasConversion(idConverter)
            .ValueGeneratedOnAdd();

        builder.Property(c => c.Name)
            .HasConversion(nameConverter);

        builder.Property(c => c.ColorHex)
            .HasConversion(colorHexConverter);

        builder.Property(c => c.UserId)
            .HasConversion(userIdConverter);

    }
}
