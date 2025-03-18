using Raylib_CsLo;
using System.Transactions;
using System.Numerics;

class Player : MovableObject
{
    static readonly KeyboardKey Up = KeyboardKey.KEY_E;
    static readonly KeyboardKey Right = KeyboardKey.KEY_F;
    static readonly KeyboardKey Down = KeyboardKey.KEY_D;
    static readonly KeyboardKey Left = KeyboardKey.KEY_S;

    public Player() : base(Raylib.RED)
    {
       
    }

    internal void Update()
    {
        DrawByVector();
        MoveByVectorAndKeyboard();
        //UpdateOrientation();

        //Raylib.DrawText("pos.X: " + pos.X + " -- xco: " + xco, 30, 90, 50, Raylib.BLACK);

    }

    public void UpdateOrientation()
    {
        switch (Input.GetAxis("vertical"))
            {
            case (-1):
                orientation = 0;
                break;
            case (1):
                orientation = 2;
                break;
        }
        switch (Input.GetAxis("horizontal"))
        {
            case (-1):
                orientation = 3;
                break;
            case (1):
                orientation = 1;
                break;
        }
    }

    public void MoveByVectorAndKeyboard()
    {
        // Bewegt das Mob direkt mit input
        Vector2 dir = new Vector2(Input.GetAxis("horizontal"), Input.GetAxis("vertical"));  // Vector = ( -1/1 | -1/1 )
        if (dir.LengthSquared() > 0f)       // Eine Richtung wird gedrueckt
        {
            dir = Vector2.Normalize(dir);   // Länge des Vektors wird auf bei schrägen schritten auf 1 gesetzt
        }
        velocity += dir * acceleration;     // Velocity wird auf Richtung(1) mal acceleration gesetzt
        // ==>  veolocity wird hier also nie von alleine geringer!
        pos += velocity;
        // pos wird hier in jedem Durchlauf der Method um velocity erhoeht!
        // Wenn man direkt stoppen möchte, wenn man die Richtungstaste loslaesst, muss man velocity jetzt auf 0 setzen:
        // velocity = new Vector2();
        StayOnScreen();

        velocity -= velocity * friction;  // je hoeher friction, dest mehr wird velocity verringert

        UpdateCoordinates();
        UpdateOrientation();
    }

    private void MovePlayer()
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
