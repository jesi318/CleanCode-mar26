public class ScannerInputDevice : IInputDevice
{
    private readonly Scanner _scanner;

    public ScannerInputDevice(Scanner scanner)
    {
        _scanner = scanner;
    }

    public bool IsEndOfData() => _scanner.IsEndOfScan();

    public string ReadData() => _scanner.ScanPage();
}
