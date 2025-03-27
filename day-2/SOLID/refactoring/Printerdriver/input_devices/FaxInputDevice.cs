public class FaxInputDevice : IInputDevice
{
    private readonly Fax _fax;

    public FaxInputDevice(Fax fax)
    {
        _fax = fax;
    }

    public bool IsEndOfData() => _fax.IsEndOfFax();

    public string ReadData() => _fax.ReceivePage();
}