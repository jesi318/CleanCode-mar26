public class PrinterDriver
{
    private readonly IInputDevice _inputDevice;
    private readonly IOutputDevice _printer;

    public PrinterDriver(IInputDevice inputDevice, IOutputDevice printer)
    {
        _inputDevice = inputDevice;
        _printer = printer;
    }

    public void Print()
    {
        while (!_inputDevice.IsEndOfData())
        {
            string page = _inputDevice.ReadData();
            _printer.Print(page);
        }
    }
}

public class Program
{
    public static void Main()
    {
        IInputDevice fileInput = new FileInputDevice(new File());
        IOutputDevice printer = new Printer();

        PrinterDriver printerDriver = new PrinterDriver(fileInput, printer);
        printerDriver.Print();

        IInputDevice scannerInput = new ScannerInputDevice(new Scanner());
        printerDriver = new PrinterDriver(scannerInput, printer);
        printerDriver.Print();
    }
}
