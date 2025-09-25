namespace Basket.Responses
{
    public record ShoppingCartResponse
    {
        public string UserName { get; init; }
        public List<ShoppingCartItemResponse> Items { get; init; }

        public ShoppingCartResponse()
        {
            UserName = string.Empty;
            Items = new List<ShoppingCartItemResponse>();
        }

        public ShoppingCartResponse(string userName)
            :this(userName, new List<ShoppingCartItemResponse>())
        {
        }

        public ShoppingCartResponse(string userName, List<ShoppingCartItemResponse> items)
        {
            UserName = userName;
            Items = new List<ShoppingCartItemResponse>();
        }
        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    }
}
