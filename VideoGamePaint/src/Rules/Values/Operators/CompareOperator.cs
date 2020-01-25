using System;

public class CompareOperator : Operator
{
    private enum Comparator
    {
        EQUALS,
        NOT_EQUALS,
        GREATER,
        GREATER_EQUALS,
        LESSER,
        LESSER_EQUALS
    }
    Comparator comparator;

    private CompareOperator(Comparator comp) : base()
    {
        this.comparator = comp;
    }

    public override int parameterCount { get => 2; }
    protected override Type[] _getParameterTypeList()
    {
        return new Type[2]
        {
            typeof(int),
            typeof(int)
        };
    }

    public override bool isBool { get => true; }
    public override bool toBool()
    {
        int int1 = arguments[0].toInteger();
        int int2 = arguments[1].toInteger();
        switch (comparator)
        {
            case Comparator.EQUALS:
                return int1 == int2;
            case Comparator.NOT_EQUALS:
                return int1 != int2;
            case Comparator.GREATER:
                return int1 > int2;
            case Comparator.GREATER_EQUALS:
                return int1 >= int2;
            case Comparator.LESSER:
                return int1 < int2;
            case Comparator.LESSER_EQUALS:
                return int1 <= int2;
        }
        return int1 == int2;
    }

    public static Expression claimExpressionString(string exprStr)
    {
        exprStr = exprStr.Trim().ToLower();
        switch (exprStr)
        {
            case "==":
                return new CompareOperator(Comparator.EQUALS);
            case "!=":
                return new CompareOperator(Comparator.NOT_EQUALS);
            case ">":
                return new CompareOperator(Comparator.GREATER);
            case ">=":
                return new CompareOperator(Comparator.GREATER_EQUALS);
            case "<":
                return new CompareOperator(Comparator.LESSER);
            case "<=":
                return new CompareOperator(Comparator.LESSER_EQUALS);
        }
        //Unable to claim expression string, return null
        return null;
    }

    public override string TokenName => "Compare";
}
