using Raylib_CsLo;
using System.Transactions;
using System.Numerics;

class Player : MovableObject
{
    static readonly KeyboardKey Up = KeyboardKey.KEY_E;
    static readonly KeyboardKey Right = KeyboardKey.KEY_F;
    static readonly KeyboardKey Down = KeyboardKey.KEY_D;
    static readonly KeyboardKey Left = KeyboardKey.KEY_S;

    public Player() :
        base(Raylib.RED)
    {
       
    }

    internal void update()
    {
        DrawByVector();
        MoveByVectorAndKeyboard();
        UpdateOrientation();
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
        switch (Input.GetAxis("horizontal")){
            case (-1):
                orientation = 3;
                break;
            case (1):
                orientation = 1;
                break;
        }
    }

    public void MoveByAxis()
    {
        xco += Input.GetAxis("horizontal") * speedInPixels;
        yco += Input.GetAxis("vertical") * speedInPixels;
        UpdateOrientation();
       
    }
    public void MoveByVectorAndKeyboard()
    {
        // Bewegt das Mob direkt mit input
        Vector2 dir = new Vector2(Input.GetAxis("horizontal"), Input.GetAxis("vertical"));
        if (dir.LengthSquared() > 0f)
        {
            dir = Vector2.Normalize(dir);
        }
        velocity += dir * acceleration;
        pos += velocity;
        StayOnScreen();

        velocity -= velocity * friction;


        UpdateCoordinates();

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
