public class Printer : IOutputDevice
{
    public void Print(string data)
    {
        Console.WriteLine($"Printing: {data}");
    }
}