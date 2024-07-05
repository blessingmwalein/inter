public class ReceiptItem
{
    public string Name { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }

    public double TotalPrice => Price * Quantity;

    public ReceiptItem(string name, double price, int quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }

    public override string ToString()
    {
        return $"{Name} {Price} x {Quantity} = {TotalPrice}";
    }
}
