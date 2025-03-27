public class FileInputDevice : IInputDevice
{
    private readonly File _file;

    public FileInputDevice(File file)
    {
        _file = file;
    }

    public bool IsEndOfData() => _file.IsEndOfFile();

    public string ReadData() => _file.ReadPage();
}