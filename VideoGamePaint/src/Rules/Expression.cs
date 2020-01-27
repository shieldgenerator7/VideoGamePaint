using System;

/// <summary>
/// An expression carries out a function, returns a value, or both.
/// </summary>
public abstract class Expression
{
    public Expression[] arguments = null;
    public Expression[] Arguments
    {
        get => arguments;
        set
        {
            if (arguments == null || arguments.Length == 0)
            {
                arguments = value;
            }
            else
            {
                throw new InvalidOperationException(
                    "Expression " + this + " cannot have its argument list set twice! " +
                    "Check to make sure you're not using the same " + this.GetType() +
                    " in a condition list or action list more than once."
                    );
            }
        }
    }

    //For building
    public int index = 0;
    public int nextIndex = 0;

    public Expression()
    {
        if (!isFunction && !isValue)
        {
            throw new NotImplementedException("Expression subtype " + this.GetType() + " does not override any type properties!");
        }
    }

    //Parameters
    public virtual int parameterCount { get => 0; }
    private Type[][] parameterTypeList;
    protected Type[][] getParameterTypeList()
    {
        if (parameterTypeList == null)
        {
            parameterTypeList = new Type[signatureCount][];
            if (signatureCount == 1)
            {
                Type[] typeList = _getParameterTypeList();
                parameterTypeList[0] = typeList;
                if (typeList.Length != parameterCount)
                {
                    throw new InvalidOperationException(
                        "Expression subtype " + this.GetType()
                        + " gives a list of " + typeList.Length
                        + " parameter types, but says it requires " + parameterCount + "!"
                        );
                }
            }
            else
            {
                for (int i = 0; i < signatureCount; i++)
                {
                    Type[] typeList = _getParameterTypeList(i);
                    parameterTypeList[i] = typeList;
                    if (typeList.Length != parameterCount)
                    {
                        throw new InvalidOperationException(
                            "Expression subtype " + this.GetType()
                            + " gives a list of " + typeList.Length
                            + " parameter types, but says it requires " + parameterCount + "!"
                            );
                    }
                }
            }
        }
        return parameterTypeList;
    }
    protected virtual Type[] _getParameterTypeList()
    {
        //Return empty list for no parameters
        return new Type[0];
    }

    protected virtual int signatureCount { get => 1; }
    /// <summary>
    /// For those expressions that have multiple signatures
    /// </summary>
    /// <param name="signatureIndex"></param>
    /// <returns></returns>
    protected virtual Type[] _getParameterTypeList(int signatureIndex)
    {
        return new Type[0];
    }

    public Type[] getArgumentTypeOptions(int argIndex)
    {
        if (parameterTypeList == null)
        {
            getParameterTypeList();
        }
        Type[] options = new Type[parameterTypeList.Length];
        int i = 0;
        foreach (Type[] typeList in parameterTypeList)
        {
            if (typeList.Length > 0)
            {
                options[i] = typeList[argIndex];
            }
            else
            {
                options[i] = null;
            }
            i++;
        }
        return options;
    }

    /// <summary>
    /// Throws an exception if the argIndex does not match any param list signature
    /// </summary>
    /// <param name="expr"></param>
    /// <param name="argIndex"></param>
    public void checkArgument(Expression expr, int argIndex)
    {
        if (parameterTypeList == null)
        {
            getParameterTypeList();
        }
        bool argValid = false;
        foreach (Type[] typeList in parameterTypeList)
        {
            if (expr.isType(typeList[argIndex]))
            {
                argValid = true;
                break;
            }
        }
        if (!argValid)
        {
            string validTypeString = "";
            foreach (Type[] typeList in parameterTypeList)
            {
                validTypeString += typeList[argIndex] + ", ";
            }
            throw new ArgumentException(
                   "Expression " + this +
                   " cannot accept parameter " + expr +
                   " as its parameter [" + argIndex + "]! " +
                   "Expression " + this +
                   " requires one of " + validTypeString +
                   " and " + expr + " does not return it."
                   );
        }
    }

    //Process as function
    public virtual bool isFunction { get => false; }
    public virtual void runFunction() { throw new NotImplementedException(); }

    public bool isType(Type type)
    {
        if (type == typeof(int))
        {
            return isInteger;
        }
        else if (type == typeof(float))
        {
            return isFloat;
        }
        else if (type == typeof(bool))
        {
            return isBool;
        }
        else if (type == typeof(string))
        {
            return isString;
        }
        else if (type == typeof(Vector))
        {
            return isVector;
        }
        else if (type == typeof(Entity))
        {
            return isEntity;
        }
        else if (type == typeof(object))
        {
            return isValue;
        }
        throw new ArgumentException("Unsupported type: " + type);
    }

    public virtual bool isValue
    {
        get
        {
            return isInteger
                || isFloat
                || isBool
                || isString
                || isVector
                || isEntity;
        }
    }
    public virtual object toValue()
    {
        if (isInteger)
        {
            return toInteger();
        }
        else if (isFloat)
        {
            return toFloat();
        }
        else if (isBool)
        {
            return toBool();
        }
        else if (isString)
        {
            return toString();
        }
        else if (isVector)
        {
            return toVector();
        }
        else if (isEntity)
        {
            return toEntity();
        }
        throw new InvalidOperationException("Expression " + this + " does not return a value!");
    }

    //Interpret as int
    public virtual bool isInteger { get => false; }
    public virtual int toInteger() { throw new NotImplementedException(); }

    //Interpret as float
    public virtual bool isFloat { get => false; }
    public virtual float toFloat() { throw new NotImplementedException(); }

    //Interpret as bool
    public virtual bool isBool { get => false; }
    public virtual bool toBool() { throw new NotImplementedException(); }

    //Interpret as string
    public virtual bool isString { get => false; }
    public virtual string toString() { throw new NotImplementedException(); }

    //Interpret as Vector
    public virtual bool isVector { get => false; }
    public virtual Vector toVector() { throw new NotImplementedException(); }

    //Interpret as Entity
    public virtual bool isEntity { get => false; }
    public virtual Entity toEntity() { throw new NotImplementedException(); }

    #region Meta Expression Properties and Methods
    //
    // The Meta Expression is used to build the rules
    //

    private enum Meta
    {
        NORMAL,
        META
    }
    private Meta meta = Meta.NORMAL;
    public bool isMeta
    {
        get => meta == Meta.META;
        set => this.meta = (value)
            ? Meta.META
            : Meta.NORMAL;
    }

    /// <summary>
    /// The name used in the code to identify this expression
    /// </summary>
    public abstract string TokenName { get; }
    //public abstract string DisplayName { get; set; }

    public virtual int ConstructorParameterCount { get => 0; }

    public virtual Expression claimExpressionString(string exprStr)
    {
        if (!isMeta)
        {
            throw new InvalidOperationException(
                "Cannot call claimExpressionString() on a non meta expression object! " +
                "Expression: " + this + ", " +
                "ExprStr: " + exprStr
                );
        }
        return null;
    }

    #endregion

    #region Operator And Object Method Overrides
    public override string ToString()
    {
        return TokenName;
    }

    //Operator Overloads
    public static implicit operator bool(Expression a) => a != null;
    #endregion
}
