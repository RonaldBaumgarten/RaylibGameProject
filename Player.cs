using Raylib_CsLo;
using System.Transactions;

class Player : MovableObject
{
    static KeyboardKey Up = KeyboardKey.KEY_E;
    static KeyboardKey Right = KeyboardKey.KEY_F;
    static KeyboardKey Down = KeyboardKey.KEY_D;
    static KeyboardKey Left = KeyboardKey.KEY_S;

    public Player() :
        base(Raylib.RED)
    {
    }

    internal void update()
    {
        movePlayer();
    }

    private void movePlayer()
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_UP) || Raylib.IsKeyDown(Up))
        {
            orientation = 0;
            Move();
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) || Raylib.IsKeyDown(Right))
        {
            orientation = 1;
            Move();
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) || Raylib.IsKeyDown(Down))
        {
            orientation = 2;
            Move();
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) || Raylib.IsKeyDown(Left))
        {
            orientation = 3;
            Move();
        }

    }
}
