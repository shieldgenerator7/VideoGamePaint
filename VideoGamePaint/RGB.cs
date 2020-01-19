using System;

public class RGB
{
    public int red;
    public int green;
    public int blue;

    public RGB(int r, int g, int b)
    {
        this.red = r;
        this.green = g;
        this.blue = b;
    }

    public override bool Equals(object obj)
    {
        return this == (RGB)obj;
    }

    public override int GetHashCode()
    {
        return red * 1000000 + green * 1000 + blue * 1;
    }

    public static bool operator ==(RGB a, RGB b)
    {
        return a.red == b.red && a.green == b.green && a.blue == b.blue;
    }
    public static bool operator !=(RGB a, RGB b)
    {
        return a.red != b.red || a.green != b.green || a.blue != b.blue;
    }
}
