﻿namespace RESTfullAPI.ViewModels.Product.Requests;

public class PatchProductRequest
{
    public string? Name { get; set; }

    public decimal? Price { get; set; }

    public int? Quantity { get; set; }
}
