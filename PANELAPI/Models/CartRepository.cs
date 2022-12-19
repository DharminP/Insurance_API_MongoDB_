using PANELAPI.Models;

public class CartRepository : ICartRepository
{
    private List<CartItem> _CartItems;
    public CartRepository()
    {
        _CartItems = new List<CartItem>();
    }
    public List<CartItem> CreateCartItem(CartItem cartItem)
    {

        if (_CartItems.Count(p => p.cpid == cartItem.cpid) == 1)
        {
            var emptycart = new List<CartItem>();
            return emptycart;
        }

        int NoOfRecords = _CartItems.Count();
        if (NoOfRecords > 0)
        {
            cartItem.cid = _CartItems.Max(p => p.cid) + 1;
        }
        else
        {
            cartItem.cid = 1;
        }
        _CartItems.Add(cartItem);
        return _CartItems;
    }

    public List<CartItem> DeleteCartItem(int cid)
    {
        var result = _CartItems.Where(p => p.cid == cid).FirstOrDefault();
        if (result != null)
            _CartItems.Remove(result);
        return _CartItems;
    }

    public CartItem GetCartItemById(int cid)
    {
        CartItem cartItem = new CartItem();
        var result = _CartItems.Where(p => p.cid == cid).FirstOrDefault();
        if (result != null)
            return result;
        else
            return cartItem;
    }

    public List<CartItem> GetCartItemList()
    {
        return _CartItems;
    }

    public List<CartItem> UpdateCartItem(CartItem cartItem)
    {
        int i = _CartItems.FindIndex(p => p.cid == cartItem.cid);
        if (i >= 0)
        {
            _CartItems[i].cpid = cartItem.cpid;
            _CartItems[i].cpname = cartItem.cpname;
        }
        return _CartItems;
    }
}