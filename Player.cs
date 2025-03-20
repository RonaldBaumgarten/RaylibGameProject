using Raylib_CsLo;
using System.Transactions;
using System.Numerics;

class Player : MovableObject
{
    static readonly KeyboardKey Up = KeyboardKey.KEY_E;
    static readonly KeyboardKey Right = KeyboardKey.KEY_F;
    static readonly KeyboardKey Down = KeyboardKey.KEY_D;
    static readonly KeyboardKey Left = KeyboardKey.KEY_S;

    public Player(Game g) : base(Raylib.RED)
    {
        base.acceleration = 2.0f;
        base.friction = 0.3f;
        orientationV = new Vector2(1, 0);
        game = g;
    }

    internal void Update()
    {
        MovePlayer();
        Draw();
        Attack();
        //UpdateOrientation();

        //Raylib.DrawText("pos.X: " + pos.X + " -- xco: " + xco, 30, 90, 50, Raylib.BLACK);
    }


    public void MovePlayer()
    {
        // Bewegt Player direkt mit input
        orientationV = Input.GetNormalizedVector();    // Vector = ( -1/1 | -1/1 )
        Move();
    }

}
