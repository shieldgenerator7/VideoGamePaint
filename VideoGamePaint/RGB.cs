using System;

public struct RGB
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

    public static RGB nullRGB = new RGB(-1, -1, -1);
    public static RGB white = new RGB(255, 255, 255);
    public static RGB black = new RGB(0, 0, 0);

    public override string ToString()
    {
        return "RGB: (" + red + ", " + green + ", " + blue + ")";
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

    public static implicit operator bool(RGB a)
    {
        return a != nullRGB;
    }
}
