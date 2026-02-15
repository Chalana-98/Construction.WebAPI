namespace ConstructionTracker.Application.Common.Models;

/// <summary>
/// Represents a result from an operation that can either succeed or fail.
/// </summary>
public class Result
{
    protected Result(bool succeeded, IEnumerable<string> errors)
    {
        Succeeded = succeeded;
        Errors = errors.ToArray();
    }
    
    public bool Succeeded { get; }
    public string[] Errors { get; }
    
    public static Result Success() => new(true, Array.Empty<string>());
    public static Result Failure(IEnumerable<string> errors) => new(false, errors);
    public static Result Failure(string error) => new(false, new[] { error });
}

/// <summary>
/// Represents a result with a value from an operation that can either succeed or fail.
/// </summary>
/// <typeparam name="T">The type of the value.</typeparam>
public class Result<T> : Result
{
    private readonly T? _value;
    
    protected Result(bool succeeded, T? value, IEnumerable<string> errors)
        : base(succeeded, errors)
    {
        _value = value;
    }
    
    public T Value => Succeeded 
        ? _value! 
        : throw new InvalidOperationException("Cannot access value of a failed result.");
    
    public static Result<T> Success(T value) => new(true, value, Array.Empty<string>());
    public new static Result<T> Failure(IEnumerable<string> errors) => new(false, default, errors);
    public new static Result<T> Failure(string error) => new(false, default, new[] { error });
}
