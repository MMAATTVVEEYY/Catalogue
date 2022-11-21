// See https://aka.ms/new-console-template for more information
using CatalogueClient;
using System.Net.Http.Headers;
using System.Net.Http.Json;

HttpClient Client = new HttpClient();
Client.BaseAddress = new Uri("http://localhost:12763");
Client.DefaultRequestHeaders.Accept.Clear();
Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

HttpResponseMessage responce = await Client.GetAsync("api/Item");
responce.EnsureSuccessStatusCode();

if (responce.IsSuccessStatusCode)
{
    var contents = await responce.Content.ReadFromJsonAsync<IEnumerable<ItemDto>>();
    foreach (var content in contents)
    {
        Console.WriteLine(content.Name);
    } 
}
else
{
    Console.WriteLine("No content");
}


Console.ReadLine();


