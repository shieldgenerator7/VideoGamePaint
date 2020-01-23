using System;

public class Rule
{
    Expression[] conditionsRaw;
    Expression[] actionsRaw;

    Expression[] conditions = new Expression[10];
    Expression[] actions = new Expression[10];

    public Rule(Expression[] conditions, Expression[] actions)
    {
        this.conditionsRaw = conditions;
        this.actionsRaw = actions;
        build();
    }

    /// <summary>
    /// Sets all the parameters of all the conditions and actions
    /// </summary>
    private void build()
    {
        //Conditions
        {
            Expression condition;
            int curIndex = 0;
            for (int nextIndex = 0; nextIndex < conditionsRaw.Length; nextIndex = condition.nextIndex)
            {
                condition = buildAt(conditionsRaw, nextIndex);
                conditions[curIndex] = condition;
                curIndex++;
            }
            //Check to make sure conditions are good
            for (int i = 0; i < conditions.Length; i++)
            {
                condition = conditions[i];
                if (condition && !condition.isBool)
                {
                    throw new ArgumentException(
                        "Rule " + this + ": "
                        + "Expression " + condition
                        + " does not return a bool! It should override isBool or should not be in the condition list."
                        );

                }
            }
        }
        //Actions
        {
            Expression action;
            int curIndex = 0;
            for (int nextIndex = 0; nextIndex < actionsRaw.Length; nextIndex = action.nextIndex)
            {
                action = buildAt(actionsRaw, nextIndex);
                actions[curIndex] = action;
                curIndex++;
            }
            //Check to make sure actions are good
            for (int i = 0; i < actions.Length; i++)
            {
                action = actions[i];
                if (action && !action.isFunction)
                {
                    throw new ArgumentException(
                        "Rule " + this + ": "
                        + "Expression " + action
                        + " does not have a function! It should override isFunction or should not be in the action list."
                        );

                }
            }
        }
    }
    /// <summary>
    /// Builds the expression at the given index
    /// </summary>
    /// <param name="index"></param>
    /// <returns>The next index after it</returns>
    private Expression buildAt(Expression[] exprListRaw, int index)
    {
        Expression expression = exprListRaw[index];
        expression.index = index;
        int paramCount = expression.parameterCount;
        Type[] paramTypes = expression.getParameterTypeList();
        Expression[] args = new Expression[paramCount];
        int nextIndex = index + 1;
        for (int c = 0; c < paramCount; c++)
        {
            Expression expr = buildAt(exprListRaw, nextIndex);
            if (!expr.isType(paramTypes[c]))
            {
                throw new ArgumentException(
                       "Rule " + this + ": " +
                       "Expression " + expression +
                       " cannot accept parameter " + expr +
                       " as its parameter [" + c + "]! " +
                       "Expression " + expression +
                       " requires a " + paramTypes[c] +
                       " and " + expr + " does not return it."
                       );
            }
            args[c] = expr;
            nextIndex = expr.nextIndex;
        }
        expression.Arguments = args;
        expression.nextIndex = nextIndex;
        return expression;
    }

    public void check()
    {
        bool ruleTrue = true;
        foreach (Expression condition in conditions)
        {
            if (condition)
            {
                if (!condition.toBool())
                {
                    ruleTrue = false;
                    break;
                }
            }
        }
        if (ruleTrue)
        {
            foreach (Expression action in actions)
            {
                if (action)
                {
                    action.runFunction();
                }
            }
        }
    }
}
