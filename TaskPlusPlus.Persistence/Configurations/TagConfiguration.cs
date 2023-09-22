using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaskPlusPlus.Domain.Entities;
using TaskPlusPlus.Domain.ValueObjects.Category;
using TaskPlusPlus.Domain.ValueObjects;
using TaskPlusPlus.Domain.ValueObjects.Tag;

namespace TaskPlusPlus.Persistence.Configurations;
public class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {

        var nameConverter = new ValueConverter<TagName, string>(
            name => name.Value,
            value => TagName.Create(value).Value);
        var colorHexConverter = new ValueConverter<ColorHex, string>(
            colorHex => colorHex.Value,
            value => ColorHex.Create(value).Value);
        var userIdConverter = new ValueConverter<UserId, string>(
            userId => userId.Value,
            value => UserId.Create(value).Value);
        var idConverter = new ValueConverter<TagId, ulong>(
            tagId => tagId.Value,
            value => new TagId(value));

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasConversion(idConverter);

        builder.Property(p => p.Name)
            .HasConversion(nameConverter);

        builder.Property(p => p.UserId)
            .HasConversion(userIdConverter);

        builder.Property(c => c.ColorHex)
            .HasConversion(colorHexConverter);
    }
}
