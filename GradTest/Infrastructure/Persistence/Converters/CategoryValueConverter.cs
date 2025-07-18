using GradTest.Domain.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GradTest.Infrastructure.Persistence.Converters;

public class CategoryValueConverter : ValueConverter<Category, string>
{
        public CategoryValueConverter() : base(
                category => category.Name,
                value => Category.FromName(value, true) 
            ) { }
}