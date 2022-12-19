using PANELAPI.Models;

public interface ICartRepository
{
    List<CartItem> GetCartItemList();
    CartItem GetCartItemById(int cid);
    List<CartItem> CreateCartItem(CartItem cartItem);
    List<CartItem> UpdateCartItem(CartItem cartItem);
    List<CartItem> DeleteCartItem(int cid);

}
