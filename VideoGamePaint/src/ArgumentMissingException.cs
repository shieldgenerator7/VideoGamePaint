using System;

/// <summary>
/// Use when an expected argument is not there
/// </summary>
public class ArgumentMissingException:Exception
{
    public Expression expression;
    public Type[] argTypes;

    /// <summary>
    /// Makes a new instance
    /// </summary>
    /// <param name="expression">The expression that expected the argument.</param>
    /// <param name="argTypes">The argument type expected.</param>
	public ArgumentMissingException(Expression expression, Type[] argTypes)
        :base(
            "Expression "+expression+
            " expected an argument of type "+argTypes+
            " but it was not found!"
            )
	{
        this.expression = expression;
        this.argTypes = argTypes;
	}
}
