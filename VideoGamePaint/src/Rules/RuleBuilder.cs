using System;
using System.Collections.Generic;

public static class RuleBuilder
{
    /// <summary>
    /// Returns a new Rule with the conditions and actions in the string
    /// FORMAT: Expression Argument Expression Argument; Expression Argument Expression Argument.
    /// </summary>
    /// <param name="ruleString"></param>
    /// <returns></returns>
    public static Rule buildRule(string ruleString)
    {
        string[] split = ruleString.Split(
            new char[] { ';' },
            StringSplitOptions.RemoveEmptyEntries
            );
        //Conditions
        string[] conditionStrings = split[0].Split(
            new char[] { ' ', ',' },
            StringSplitOptions.RemoveEmptyEntries
            );
        List<Expression> conditions = new List<Expression>();
        for (int i = 0; i < conditionStrings.Length; i++)
        {
            Expression expr = getExpression(conditionStrings, i);
            if (expr)
            {
                conditions.Add(expr);
            }
        }
        //Actions
        string[] actionStrings = split[1].Split(
            new char[] { ' ', ',' },
            StringSplitOptions.RemoveEmptyEntries
            );
        List<Expression> actions = new List<Expression>();
        for (int i = 0; i < actionStrings.Length; i++)
        {
            Expression expr = getExpression(actionStrings, i);
            if (expr)
            {
                actions.Add(expr);
            }
        }
        //Make new rule
        return new Rule(conditions.ToArray(), actions.ToArray());
    }

    static Expression getExpression(string[] exprs, int index)
    {
        string expr = exprs[index].Trim().ToLower();
        if (exprs[index].Trim() == "")
        {
            throw new ArgumentException("Expression string at the given index was the empty string!");
        }
        //Switch expression string and make a new expression
        switch (expr)
        {
            //Values
            case "constant":
                return new ConstantValue(getNextParameter(exprs, index, 1));
            case "entity":
                return new EntityValue(getNextParameter(exprs, index, 1));
            case "grounded":
                return new GroundedValue();
            case "key":
                return new KeyHeld(getNextParameter(exprs, index, 1));
            //Operators
            case "not":
                return new NotOperator();
            case "multiply":
                return new MultiplyOperator();
            //Actions
            case "move":
                return new MoveAction();
        }
        //Constant claiming
        Expression claimedExpression = null;
        claimedExpression = EntityValue.claimExpressionString(expr);
        if (!claimedExpression)
        {
            claimedExpression = ConstantValue.claimExpressionString(expr);
        }
        return claimedExpression;
    }

    static string getNextParameter(string[] exprs, int index, int paramNumber)
    {
        string exprNext = exprs[index + 1].Trim();
        int curParamNumber = 0;
        if (exprNext != "")
        {
            curParamNumber++;
        }
        //Make sure next argument is not empty string
        for (
            int i = index + 1;
            i < exprs.Length && (exprNext == "" || curParamNumber != paramNumber);
            i++
            )
        {
            exprNext = exprs[i].Trim();
            if (exprNext != "")
            {
                curParamNumber++;
            }
            else
            {
                throw new ArgumentException("Expression string at the given index was the empty string!");
            }
        }
        return exprNext;
    }
}
