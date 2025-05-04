﻿namespace RESTfullAPI.ViewModels.Product.Responses;

public class ProductResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public DateTime CreatedOn { get; set; }
}
