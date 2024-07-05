using System.IO;


public class Receipt
{
    public string Number { get; set; }
    public string CashierName { get; set; }
    public double Total { get; set; }
    public double Change { get; set; }

    public List<ReceiptItem> receiptItems;

    // Constructor
    public Receipt(string number, string cashierName, double total, double change)
    {
        Number = number;
        CashierName = cashierName;
        Total = total;
        Change = change;
        receiptItems = new List<ReceiptItem>();
    }

    // Add receipt item
    public void AddReceiptItem(ReceiptItem receiptItem)
    {
        receiptItems.Add(receiptItem);
    }

    // Print receipt
    public void PrintReceipt()
    {
        Console.WriteLine("Receipt number: " + Number);
        Console.WriteLine("Cashier name: " + CashierName);
        Console.WriteLine("Total: " + Total);
        Console.WriteLine("Change: " + Change);
    }

    // ToString override
    public override string ToString()
    {
        return $"Receipt number: {Number}\nCashier name: {CashierName}\nTotal: {Total}\nChange: {Change}";
    }

    // Print receipt items
    public void PrintReceiptItems()
    {
        foreach (var item in receiptItems)
        {
            Console.WriteLine(item);
        }
    }

    //print receipt to file
    public void PrintReceiptToFile()
    {
        //path current directory
        string path = $"receipt-{Number}.txt";

        using (StreamWriter sw = File.AppendText(path))
        {
            sw.WriteLine("Receipt number: " + Number);
            sw.WriteLine("Cashier name: " + CashierName);
            sw.WriteLine("Total: " + Total);
            sw.WriteLine("Change: " + Change);
            sw.WriteLine("Items: ");
            foreach (var item in receiptItems)
            {
                sw.WriteLine(item);
            }
        }
    }

    public static Receipt FromTextFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        

        string number = lines[0].Split(':')[1].Trim();
        string cashierName = lines[1].Split(':')[1].Trim();
        double total = double.Parse(lines[2].Split(':')[1].Trim());
        double change = double.Parse(lines[3].Split(':')[1].Trim());

        Receipt receipt = new Receipt(number, cashierName, total, change);

        for (int i = 5; i < lines.Length; i++)
        {
            string[] itemParts = lines[i].Split(' ');
            string itemName = itemParts[0] + " " + itemParts[1];
            double itemPrice = double.Parse(itemParts[2]);
            int itemQuantity = int.Parse(itemParts[4]);
            double itemTotalPrice = double.Parse(itemParts[6]);

            ReceiptItem item = new ReceiptItem(itemName, itemPrice, itemQuantity);
            receipt.AddReceiptItem(item);
        }

        return receipt;
    }

     public static Receipt FromTextFileSmart(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        string number = string.Empty;
        string cashierName = string.Empty;
        double total = 0;
        double change = 0;

        List<ReceiptItem> items = new List<ReceiptItem>();
        bool readingItems = false;

        foreach (var line in lines)
        {
            if (line.StartsWith("Receipt number:"))
            {
                number = line.Split(':')[1].Trim();
            }
            else if (line.StartsWith("Cashier name:"))
            {
                cashierName = line.Split(':')[1].Trim();
            }
            else if (line.StartsWith("Total:"))
            {
                total = double.Parse(line.Split(':')[1].Trim());
            }
            else if (line.StartsWith("Change:"))
            {
                change = double.Parse(line.Split(':')[1].Trim());
            }
            else if (line.StartsWith("Items:"))
            {
                readingItems = true;
                continue;
            }
            else if (readingItems)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }

                string[] itemParts = line.Split(new[] { ' ', 'x', '=' }, StringSplitOptions.RemoveEmptyEntries);
                string itemName = itemParts[0] + " " + itemParts[1];
                double itemPrice = double.Parse(itemParts[2]);
                int itemQuantity = int.Parse(itemParts[3]);
                double itemTotalPrice = double.Parse(itemParts[4]);

                ReceiptItem item = new ReceiptItem(itemName, itemPrice, itemQuantity);
                items.Add(item);
            }
        }

        Receipt receipt = new Receipt(number, cashierName, total, change);
        foreach (var item in items)
        {
            receipt.AddReceiptItem(item);
        }

        return receipt;
    }
    

}
