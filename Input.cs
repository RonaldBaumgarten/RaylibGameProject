using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_CsLo;

// written in course
public class Input
{
    static readonly KeyboardKey Up = KeyboardKey.KEY_E;
    static readonly KeyboardKey Right = KeyboardKey.KEY_F;
    static readonly KeyboardKey Down = KeyboardKey.KEY_D;
    static readonly KeyboardKey Left = KeyboardKey.KEY_S;

    public static int GetAxis(string axis)
    {
        if (axis == "horizontal")
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) || Raylib.IsKeyDown(Right)) return 1;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) || Raylib.IsKeyDown(Left)) return -1;
        }
        if (axis == "vertical")
        {
            if (Raylib.IsKeyDown(KeyboardKey.KEY_UP)|| Raylib.IsKeyDown(Up)) return -1;
            if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN)|| Raylib.IsKeyDown(Down)) return 1;
        }
        return 0;
    }

    public static Vector2 GetVector()
    {
        int h = GetAxis("horizontal");
        int v = GetAxis("vertical");

        return new Vector2(h, v);
        //return Vector2.Normalize(new Vector2(h, v));
    }

    public static Vector2 GetNormalizedVector()
    {
        Vector2 v = GetVector();
        if(v.LengthSquared() > 0f)
        {
            return Vector2.Normalize(v);
        }
        return v;
    }
}
