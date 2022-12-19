using Microsoft.AspNetCore.Mvc;
using PANELAPI.Models;
using System.Text.Json;

namespace PANELAPI.Controllers;

[ApiController]
[Route("[controller]")]

public class DashboardController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _Configuration;
    private ICartRepository _cartRepository;
    public DashboardController(IHttpClientFactory httpClientFactory, IConfiguration Configuration, ICartRepository cartRepository)
    {
        _httpClientFactory = httpClientFactory;
        _Configuration = Configuration;
        _cartRepository = cartRepository;
    }
    [HttpGet]
    public async Task<ActionResult<string>> GetAllActivePolicies()
    {
        Policy? policy = new Policy();
        string? Json = "";
        var client = _httpClientFactory.CreateClient("CMSApi");
        client.BaseAddress = new Uri(_Configuration.GetValue<string>("GlobalVariable:CMSUrl"));
        var response = await client.GetAsync("/Policy/GetAllPoliciesJson");
        string result = await response.Content.ReadAsStringAsync();
        Json = JsonSerializer.Serialize(result);
        return result;
    }
    [HttpGet("CartItems")]
    public List<CartItem> GetAllCartItem()
    {
        return _cartRepository.GetCartItemList();
    }
    [HttpGet("{cid:int}")]
    public CartItem GetCartItemById(int cid)
    {
        return _cartRepository.GetCartItemById(cid);
    }

    [HttpPost]
    public List<CartItem> InsertIntoCart(CartItem cartItem)
    {
        return _cartRepository.CreateCartItem(cartItem);
    }
    [HttpPut]
    public List<CartItem> UpdateCartItem(CartItem cartItem)
    {
        return _cartRepository.UpdateCartItem(cartItem);
    }
    [HttpDelete("{cid:int}")]
    public List<CartItem> DeleteFromCart(int cid)
    {
        return _cartRepository.DeleteCartItem(cid);
    }
    //
    [HttpGet("GetRelevantPolicies")]
    public async Task<ActionResult<string>> GetRelevantPolicies(string ptype, string Gender, string AgeGroup, string Members, int pgrade)
    {
        Policy? policy = new Policy();
        string? Json = "";
        var client = _httpClientFactory.CreateClient("CMSApi");
        client.BaseAddress = new Uri(_Configuration.GetValue<string>("GlobalVariable:CMSUrl"));        
        string Url = "/Policy/GetRelevantPolicyJson/" + ptype + "/" + Gender + "/" + AgeGroup + "/" + Members + "/" + pgrade;
        var response = await client.GetAsync(Url);
        string result = await response.Content.ReadAsStringAsync();
        Json = JsonSerializer.Serialize(result);
        return result;
    }
}