using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create new ReceiptItem objects
            // ReceiptItem item1 = new ReceiptItem("Apple Mirinda", 0.99, 5);
            // ReceiptItem item2 = new ReceiptItem("Banana Super", 0.59, 3);
            // ReceiptItem item3 = new ReceiptItem("Orange Mazowe", 0.79, 2);

            // // Create a new Receipt object
            // Receipt receipt = new Receipt("58023", "Takudzwa Mudari", 8.3, 0.00);

            // // Add the items to the receipt
            // receipt.AddReceiptItem(item1);
            // receipt.AddReceiptItem(item2);
            // receipt.AddReceiptItem(item3);

            // Print the receipt
            // Console.WriteLine(receipt);
            // receipt.PrintReceiptItems();
            // receipt.PrintReceiptToFile();

            Receipt.FromTextFileSmart("receipt-58023.txt").PrintReceiptItems();
        }
    }

}