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
        base.acceleration = 2.0f;
        base.friction = 0.3f;
    }

    internal void Update()
    {
        Draw();
        MoveByVectorAndKeyboard();
        Attack();
        //UpdateOrientation();

        //Raylib.DrawText("pos.X: " + pos.X + " -- xco: " + xco, 30, 90, 50, Raylib.BLACK);

    }


    public void MoveByVectorAndKeyboard()
    {
        // Bewegt das Mob direkt mit input

        /*
        orientationV = Input.GetVector();
        if (orientationV.LengthSquared() > 0f)       // Eine Richtung wird gedrueckt
        {
            orientationV = Vector2.Normalize(orientationV);   // Länge des Vektors wird auf bei schrägen schritten auf 1 gesetzt
        }

        Move(ref orientationV);

        */
        Vector2 dir = Input.GetVector();    // Vector = ( -1/1 | -1/1 )
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

}
