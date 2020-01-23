using System;

public class Rule
{
    Expression[] actionsRaw;

    Expression[] actions = new Expression[10];

    public Rule(Expression[] actions)
    {
        this.actionsRaw = actions;
        build();
    }

    /// <summary>
    /// Sets all the parameters of all the conditions and actions
    /// </summary>
    private void build()
    {
        Expression action;
        int curIndex = 0;
        for (int nextIndex = 0; nextIndex < actionsRaw.Length; nextIndex = action.nextIndex)
        {
            action = buildAt(nextIndex);
            actions[curIndex] = action;
            curIndex++;
        }
    }
    /// <summary>
    /// Builds the expression at the given index
    /// </summary>
    /// <param name="index"></param>
    /// <returns>The next index after it</returns>
    private Expression buildAt(int index)
    {
        Expression action = actionsRaw[index];
        action.index = index;
        int paramCount = action.parameterCount;
        Expression[] args = new Expression[paramCount];
        int nextIndex = index + 1;
        for (int c = 0; c < paramCount; c++)
        {
            Expression expr = buildAt(nextIndex);
            args[c] = expr;
            nextIndex = expr.nextIndex;
        }
        action.arguments = args;
        action.nextIndex = nextIndex;
        return action;
    }

    public void check()
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
