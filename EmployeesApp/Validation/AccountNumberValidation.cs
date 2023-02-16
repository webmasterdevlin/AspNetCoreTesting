namespace EmployeesApp.Validation;

public class AccountNumberValidation
{
    private const int StartingPartLength = 3;
    private const int MiddlePartLength = 10;
    private const int LastPartLength = 2;

    public bool IsValid(string accountNumber)
    {
        var firstDelimiter = accountNumber.IndexOf('-');
        var secondDelimiter = accountNumber.LastIndexOf('-');

        if (firstDelimiter == -1 || (firstDelimiter == secondDelimiter))
            throw new ArgumentException();

        var firstPart = accountNumber[..firstDelimiter];
        if (firstPart.Length != StartingPartLength)
            return false;

        var tempPart = accountNumber.Remove(0, StartingPartLength + 1);
        var middlePart = tempPart[..tempPart.IndexOf('-')];
        if (middlePart.Length != MiddlePartLength)
            return false;

        var lastPart = accountNumber[(secondDelimiter + 1)..];
        if (lastPart.Length != LastPartLength)
            return false;

        return true;
    }
}