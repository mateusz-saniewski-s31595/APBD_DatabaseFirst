namespace APBD_DatabaseFirst.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}