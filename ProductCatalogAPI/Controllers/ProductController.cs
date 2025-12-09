using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// Store each of the endpoints for the API

[Route("api/products")]
[ApiController]

public class ProductController : ControllerBase
{
    private static List<Product> products = new List<Product>();
    
    // GET: retrieve all products
    [HttpGet]
    public ActionResult<List<Product>> GetAll() => products;

    // GET by ID: Retrieve a single product by ID
    [HttpGet("{id}")] // when pass an ID, it will retrieve a specific product
    public ActionResult<Product> GetById(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        return product != null ? Ok(product) : NotFound();
    }
    
    // POST: add a new product
    [HttpPost]
    public ActionResult<Product> Create(Product newProduct)
    {
        newProduct.Id = products.Count + 1;
        products.Add(newProduct);
        return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
    }
    
    // PUT: update an existing product
    [HttpPut("{id}")]
    public ActionResult Update(int id, Product updatedProduct)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        product.Name = updatedProduct.Name;
        product.Description = updatedProduct.Description;
        product.Price = updatedProduct.Price;
        return Ok(product);
    }
    
    // Delete: remove a product
    [HttpDelete("{id}")]
    public ActionResult Delete(int id)
    {
        var product = products.FirstOrDefault(p => p.Id == id);
        if (product == null) return NotFound();

        products.Remove(product);
        return NoContent();
    }
}