using FluentValidation;
using GradTest.Application.Orders.Commands.CreateOrderCommand;
using GradTest.Application.Products.Commands.CreateProductCommand;
using GradTest.Application.Products.Commands.DeleteProductCommand;
using GradTest.Application.Products.Commands.UpdateProductCommand;

namespace GradTest.API.Configuration.Builder;

public static class ValidatorConfiguration
{
    public static void SetupValidatiors(this WebApplicationBuilder builder)
    {
        builder
            .SetupOrderValidators()
            .SetupProductValidators();

    }

    private static WebApplicationBuilder SetupOrderValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderCommandValidator>();
        
        return builder;
    }
    
    private static WebApplicationBuilder SetupProductValidators(this WebApplicationBuilder builder)
    {
        builder.Services.AddValidatorsFromAssemblyContaining<CreateProductCommandValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<UpdateProductCommandValidator>();
        builder.Services.AddValidatorsFromAssemblyContaining<DeleteProductCommandValidator>();       
        
        return builder;
    }
}