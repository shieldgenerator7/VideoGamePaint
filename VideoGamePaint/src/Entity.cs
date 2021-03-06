﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class Entity
{
    public List<Keys> inputs;//the runtime list of keys currently being pressed
    public List<Rule> rules = new List<Rule>();

    public Dictionary<string, object> variables = new Dictionary<string, object>();

    public Vector pos = new Vector(0, 0);
    private Vector moveDir = new Vector(0, 0);//how much it moves each frame

    private PixelGrid collisionGrid;

    public Vector inputDir = Vector.zero;

    public Entity(PixelGrid pg)
    {
        this.collisionGrid = pg;
    }

    public void setVariableNames(string nameListString)
    {
        string[] names = nameListString.Trim().Split(
            new char[] { '\n', ' ', ',' },
            StringSplitOptions.RemoveEmptyEntries
            );
        setVariableNames(names);
    }

    public void setVariableNames(string[] names)
    {
        variables = new Dictionary<string, object>();
        int initialValueStep = 0;//0,1
        string lastVarName = "";
        foreach (string name in names)
        {
            string nameTrim = name.Trim();
            if (nameTrim == "=")
            {
                initialValueStep = 1;
                continue;
            }
            else if (initialValueStep == 1)
            {
                initialValueStep = 0;
                variables[lastVarName] = ConstantValue.getObjectFromString(nameTrim);
                continue;
            }
            if (variables.ContainsKey(nameTrim))
            {
                throw new ArgumentException(
                    "Variable " + name + " cannot be added twice! " +
                    "(Same as variable " + nameTrim + ") " +
                    "Entity: " + this
                    );
            }
            lastVarName = nameTrim;
            variables.Add(nameTrim, 0);
        }
    }

    public void processRules()
    {
        foreach (Rule rule in rules)
        {
            rule.check();
        }
    }

    /// <summary>
    /// Returns true if this entity can move the whole way in the given direction (incorpoates magnitude)
    /// </summary>
    /// <param name="moveDir"></param>
    /// <returns></returns>
    public bool canMove(Vector moveDir)
    {
        Vector newPos = pos + moveDir;
        if (collisionGrid.validPixel(newPos))
        {
            RGB rgb = collisionGrid.getPixel(newPos);
            if (rgb == RGB.white || !rgb.isValid())
            {
                return true;
            }
        }
        return false;
    }

    public void move(Vector moveDir)
    {
        this.moveDir += moveDir;
    }

    public void updateMovement()
    {
        int gx = pos.x + moveDir.x;
        int gy = pos.y + moveDir.y;
        Vector endPos = pos + moveDir;
        Vector lastValidPoint = pos;
        foreach (Vector v in collisionGrid.getPixelsInLine(pos, endPos))
        {
            if (!collisionGrid.validPixel(v))
            {
                break;
            }
            RGB rgb = collisionGrid.getPixel(v.x, v.y);
            if (rgb == RGB.white || !rgb.isValid())
            {
                lastValidPoint = v;
            }
            else
            {
                break;
            }
        }
        pos = lastValidPoint;
        moveDir = Vector.zero;
        //Continue: Slide player in valid direction
        if (lastValidPoint != endPos)
        {
            int rise = endPos.y - lastValidPoint.y;
            int run = endPos.x - lastValidPoint.x;
            Vector[] tryDirs = new Vector[2];
            if (Math.Abs(run) >= Math.Abs(rise))
            {
                tryDirs[0] = new Vector(run, 0);
                tryDirs[1] = new Vector(0, rise);
            }
            else
            {
                tryDirs[0] = new Vector(0, rise);
                tryDirs[1] = new Vector(run, 0);
            }
            for (int i = 0; i < tryDirs.Length; i++)
            {
                Vector nextPos = lastValidPoint + new Vector(
                    Math.Sign(tryDirs[i].x),
                    Math.Sign(tryDirs[i].y)
                    );
                if (collisionGrid.validPixel(nextPos))
                {
                    RGB rgb = collisionGrid.getPixel(nextPos);
                    if (rgb == RGB.white || !rgb.isValid())
                    {
                        moveDir = tryDirs[i];
                        updateMovement();
                        return;
                    }
                }
            }
        }
    }
}
