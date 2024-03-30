using System;
using System.Collections.Generic;

namespace OblakotekaServer.DataAccess.Models;

public partial class Product
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }
}
