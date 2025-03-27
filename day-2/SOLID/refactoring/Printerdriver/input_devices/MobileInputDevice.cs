public class MobileInputDevice : IInputDevice
{
    private readonly Mobile _mobile;

    public MobileInputDevice(Mobile mobile)
    {
        _mobile = mobile;
    }

    public bool IsEndOfData() => _mobile.HasMoreData();

    public string ReadData() => _mobile.ReadMessage();
}