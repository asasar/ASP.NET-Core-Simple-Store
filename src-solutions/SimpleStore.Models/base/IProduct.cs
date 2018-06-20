using System;

namespace SimpleStore.Models
{
    public interface IProduct 
    {
        DateTime Created { get; set; }
        string Description { get; set; }
        bool EnabledUser { get; set; }
        int Id { get; set; }
        string ImageUrl { get; set; }
        string Name { get; set; }
        double Price { get; set; }
    }
}