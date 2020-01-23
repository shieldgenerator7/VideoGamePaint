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
                    "Check to make sure you're not using the same "+this.GetType()+
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
        if (!isFunction
            && !isInteger
            && !isFloat
            && !isBool
            && !isVector
            && !isEntity
            )
        {
            throw new NotImplementedException("Expression subtype " + this.GetType() + " does not override any type properties!");
        }
    }

    //Parameters
    public virtual int parameterCount { get => 0; }
    public Type[] getParameterTypeList()
    {
        Type[] types = _getParameterTypeList();
        if (types.Length != parameterCount)
        {
            throw new InvalidOperationException(
                "Expression subtype " + this.GetType()
                + " gives a list of " + types.Length
                + " parameter types, but says it requires " + parameterCount + "!"
                );
        }
        return types;
    }
    protected virtual Type[] _getParameterTypeList()
    {
        //Return empty list for no parameters
        return new Type[0];
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
        else if (type == typeof(Vector))
        {
            return isVector;
        }
        else if (type == typeof(Entity))
        {
            return isEntity;
        }
        throw new ArgumentException("Unsupported type: "+type);
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

    //Interpret as Vector
    public virtual bool isVector { get => false; }
    public virtual Vector toVector() { throw new NotImplementedException(); }

    //Interpret as Entity
    public virtual bool isEntity { get => false; }
    public virtual Entity toEntity() { throw new NotImplementedException(); }

    //Operator Overloads
    public static implicit operator bool(Expression a) => a != null;
}
