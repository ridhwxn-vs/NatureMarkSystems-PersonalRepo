using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using ItemAPI.Models; // ✅ Add this
using System.Collections.Generic;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Welcome to the Simple Web API!");

var items = new List<Item>();

app.MapGet("/items", () => items);

app.MapGet("/items/{id}", (int id) =>
{
    var item = items.FirstOrDefault(i => i.Id == id);
    return item is not null ? Results.Ok(item) : Results.NotFound();
});

app.MapPost("/items", (Item newItem) =>
{
    newItem.Id = items.Count + 1;
    items.Add(newItem);
    return Results.Created($"/items/{newItem.Id}", newItem);
});

app.MapPut("/items/{id}", (int id, Item newItem) =>
{
    var existingItem = items.FirstOrDefault(i => i.Id == id);
    if (existingItem == null)
        return Results.NotFound();

    existingItem.Name = newItem.Name;

    return Results.Ok(existingItem);
});


app.Run();
