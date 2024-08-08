namespace SolarVoyage.Core.Models;

public class Result
{
    public bool Succeeded { get;}
    public IEnumerable<string> Errors { get; }

    protected Result(bool success, IEnumerable<string> errors)
    {
        var enumerable = errors as string[] ?? errors.ToArray();
        
        if (success && enumerable.Any())
        {
            throw new InvalidOperationException();
        }
        if (!success && !enumerable.Any())
        {
            throw new InvalidOperationException();
        }
        
        Succeeded = success;
        Errors = enumerable;
    }

    public static Result Fail(string error)
    {
        return new Result(false, new List<string>{error});
    }
    
    public static Result Fail(IEnumerable<string> error)
    {
        return new Result(false, error);
    }
    
    public static Result<T> Fail<T>(string error)
    {
        return new Result<T>(default(T), false, error);
    }
    
    public static Result<T> Fail<T>(IEnumerable<string> error)
    {
        return new Result<T>(default(T), false, error);
    }

    public static Result Success()
    {
        return new Result(true, new List<string>());
    }
    
    public static Result<T> Success<T>(T value)
    {
        return new Result<T>(value,true, string.Empty);
    }
}

public class Result<T> : Result
{
    public T Value { get; }

    protected internal Result(T value, bool success, string error) : base(success, !String.IsNullOrEmpty(error) ? new List<string>(){error} : Array.Empty<string>())
    {
        Value = value;
    }
    protected internal Result(T value, bool success, IEnumerable<string> error) : base(success, error)
    {
        Value = value;
    }
}