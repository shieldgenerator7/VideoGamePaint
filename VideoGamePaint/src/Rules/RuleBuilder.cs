﻿using System;
using System.Collections.Generic;

public static class RuleBuilder
{
    static Dictionary<string, Expression> exprMetas;

    public static string[] ExpressionDisplayNames
    {
        get
        {
            string[] names = new string[exprMetas.Keys.Count];
            int i = 0;
            foreach (Expression expr in exprMetas.Values)
            {
                names[i] = expr.TokenName;
                i++;
            }
            return names;
        }
    }

    public static void buildMetas()
    {
        List<Type> metaTypes = new List<Type>();
        //Operators
        metaTypes.Add(typeof(NotOperator));
        metaTypes.Add(typeof(MultiplyOperator));
        metaTypes.Add(typeof(CompareOperator));
        //Values
        metaTypes.Add(typeof(EntityValue));
        metaTypes.Add(typeof(GroundedValue));
        metaTypes.Add(typeof(KeyHeld));
        metaTypes.Add(typeof(VariableGetValue));
        metaTypes.Add(typeof(VectorValue));
        metaTypes.Add(typeof(ConstantValue));
        //Actions
        metaTypes.Add(typeof(MoveAction));
        metaTypes.Add(typeof(TeleportAction));
        metaTypes.Add(typeof(VariableSetAction));

        //Add all the types to the dictionary
        exprMetas = new Dictionary<string, Expression>();
        foreach (Type metaType in metaTypes)
        {
            Expression meta = (Expression)metaType
                .GetConstructor(new Type[0])
                .Invoke(new object[0]);
            meta.isMeta = true;
            exprMetas.Add(meta.TokenName.Trim().ToLower(), meta);
        }
    }

    public static List<Rule> buildRuleSet(string ruleSetString)
    {
        List<Rule> ruleSet = new List<Rule>();
        string[] ruleStrings = ruleSetString.Split(
            new char[] { '.', ';' },
            StringSplitOptions.RemoveEmptyEntries
            );
        foreach (string ruleString in ruleStrings)
        {
            string ruleStringTrim = ruleString.Trim();
            if (ruleStringTrim != "" && !ruleStringTrim.StartsWith("//"))
            {
                ruleSet.Add(buildRule(ruleStringTrim));
            }
        }
        return ruleSet;
    }

    /// <summary>
    /// Returns a new Rule with the conditions and actions in the string
    /// FORMAT: Expression Argument Expression Argument; Expression Argument Expression Argument.
    /// </summary>
    /// <param name="ruleString"></param>
    /// <returns></returns>
    public static Rule buildRule(string ruleString)
    {
        string[] split = ruleString.Split(
            new char[] { ':' },
            StringSplitOptions.RemoveEmptyEntries
            );
        //Conditions
        string[] conditionStrings = split[0].Split(
            new char[] { ' ', ',' },
            StringSplitOptions.RemoveEmptyEntries
            );
        List<Expression> conditions = new List<Expression>();
        for (int i = 0; i < conditionStrings.Length;)
        {
            int outIndex;
            Expression expr = getExpression(conditionStrings, i, out outIndex);
            i = outIndex;
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
        for (int i = 0; i < actionStrings.Length;)
        {
            int outIndex;
            Expression expr = getExpression(actionStrings, i, out outIndex);
            i = outIndex;
            if (expr)
            {
                actions.Add(expr);
            }
        }
        //Make new rule
        return new Rule(conditions.ToArray(), actions.ToArray());
    }

    static Expression getExpression(string[] exprs, int index, out int nextIndex)
    {
        nextIndex = index + 1;
        string expr = exprs[index].Trim().ToLower();
        if (exprs[index].Trim() == "")
        {
            throw new ArgumentException("Expression string at the given index was the empty string!");
        }
        //Get expression string and make a new expression
        if (exprMetas.ContainsKey(expr))
        {
            int count = exprMetas[expr].ConstructorParameterCount;
            Type[] strTypes = new Type[count];
            object[] objs = new object[count];
            for (int i = 0; i < count; i++)
            {
                strTypes[i] = typeof(string);
                objs[i] = getNextParameter(exprs, index, out nextIndex, i + 1);
            }
            return (Expression)exprMetas[expr].GetType()
                .GetConstructor(strTypes)
                .Invoke(objs);
        }
        //Constant claiming
        foreach (Expression meta in exprMetas.Values)
        {
            Expression claimedExpression = meta.claimExpressionString(expr);
            if (claimedExpression)
            {
                return claimedExpression;
            }
        }
        return null;
    }

    static string getNextParameter(string[] exprs, int index, out int nextIndex, int paramNumber)
    {
        nextIndex = index + 2;
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
            nextIndex++;
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
