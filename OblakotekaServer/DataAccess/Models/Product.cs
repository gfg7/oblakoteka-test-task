using System;
using System.Collections.Generic;
using OblakotekaServer.Domain.Models;

namespace OblakotekaServer.DataAccess.Models;

public partial class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}

public static class ProductDbExstension
{
    public static ProductDomain ToDomain(this Product model)
    {
        return new(model.Id, model.Name, model.Description);
    }

    public static Product BuildDbModel(this ProductCreateParams @params)
    {
        return new()
        {
            Name = @params.Name,
            Description = @params.Description
        };
    }

    public static Product ApplyChanges(this Product model, ProductEditParams @params)
    {
        model.Name = @params.Name;
        model.Description = @params.Description;

        return model;
    }
}
