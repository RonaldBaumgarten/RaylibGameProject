using Raylib_CsLo;
using System.Numerics;
 abstract class MovableObject
{
    internal Game game;
    MobSize size;
    Color color;
    public int speedInPixels;
    protected int orientation;
    public int xco;
    public int yco;
    internal float acceleration;
    internal float friction;
    internal Vector2 pos;
    internal Vector2 velocity;
    protected Vector2 orientationV;

    public MovableObject(Color c)
    {
        color = c;
        size = MobSize.M;
        speedInPixels = 5;
        int orientation = 0;
        // TO-DO: add orientation/direction vector
        xco = Raylib.GetScreenWidth() / 2;
        yco = Raylib.GetScreenHeight() / 2;
        pos = new Vector2(Raylib.GetScreenWidth() / 2,  Raylib.GetScreenHeight() / 2);
        velocity = new Vector2();
    }

    public void Draw()
    {
        Vector2 medium = new Vector2(20.0f, 30.0f);
        switch (this.size)
        {
            case (MobSize):
                Raylib.DrawRectangleV(pos, medium, color);
                break;
        }
    }

    public void DrawAsCircle()
    {
        Raylib.DrawCircleV(pos, 20f, Raylib.YELLOW);
    }

    public bool isColliding(MovableObject m)
    {
        int width = 20;
        int heigth = 30;

        if (this.xco <= m.xco + width && this.xco + width >= m.xco)
        {
            if (this.yco <= (m.yco + heigth) && this.yco + heigth >= m.yco)
            {
                return true;
            }
        }
        return false;
    }

    public bool isCollidingByAxis(MovableObject m)
    {
        float width = 20.0f;
        float heigth = 30.0f;

        if(this.pos.X <= m.pos.X+width && this.pos.X+width >= m.pos.X)
        {
            if(this.pos.Y <= (m.pos.Y+heigth) && this.pos.Y+heigth >= m.pos.Y)
            {
                return true;
            }
        }

        return false;
    }

    public void bump(MovableObject m)
    {

        // the angle between the two vectors is less than 90 degrees if Dot-Product > 0!
        float p = Vector2.Dot(this.orientationV, m.orientationV);
        String prod = "Angle between MoBs: " + p;   // Debug-Message
        game.messageB = prod;                       // Debug-Message

        /*** When MoBs are not facing the same direction: ***/
        // the angle between the two vectors is less than 90 degrees if Dot-Product > 0!
        if(p <= 0)
        {
            game.messageA = "NOT same orientation";
            m.velocity = new Vector2(0,0);
            m.Move(this.orientationV, 20);       
            m.Move(this.orientationV, 10);       
            m.Move(this.orientationV, 5);       
        }
        /*** When MoBs ARE facing the same direction: ***/
        /*
        else
        {
            game.messageB = "SAME orientation";
            //Raylib.DrawText("Collision! Different orientation", 30, 30, 50, Raylib.BLACK);

            if(orientation == 1)
            {
                bool isBehind = (this.pos.X < m.pos.X);
                if(isBehind)
                {
                    m.Move(m.orientation, 20);       
                    m.Move(m.orientation, 10);       
                    m.Move(m.orientation, 5);       
                }
            }
            if(orientation == 3)
            {
                bool isBehind = (this.pos.X > m.pos.X);
                if(isBehind)
                {
                    m.Move(m.orientation, 20);       
                    m.Move(m.orientation, 10);       
                    m.Move(m.orientation, 5);       
                }
            }
            if(orientation == 0)
            {
                bool isBehind = (this.pos.Y > m.pos.Y);
                if(isBehind)
                {
                    m.Move(m.orientation, 20);       
                    m.Move(m.orientation, 10);       
                    m.Move(m.orientation, 5);       
                }
            }
            if(orientation == 2)
            {
                bool isBehind = (this.pos.Y < m.pos.Y);
                if(isBehind)
                {
                    m.Move(m.orientation, 20);       
                    m.Move(m.orientation, 10);       
                    m.Move(m.orientation, 5);       
                }
            }
        }
        
        */
        //MoveByVectorOrientation();       
    }

    public void Spawn()
    {
        Random rnd = new Random();
        int side = rnd.Next(0, 4);

        switch (side)
        {
            case (0):
                orientation = 2;
                yco = 0;
                pos.Y = 0;
                xco = rnd.Next(10, Raylib.GetScreenWidth() - 10);
                pos.X = xco;
                //Draw();
                Draw();
                break;
            case (1):
                orientation = 3;
                xco = Raylib.GetScreenWidth();
                pos.X = Raylib.GetScreenWidth();
                yco = rnd.Next(10, Raylib.GetScreenHeight() - 10);
                pos.Y = yco;
                //Draw();
                Draw();
                break;
            case (2):
                orientation = 0;
                yco = Raylib.GetScreenHeight();
                pos.Y = Raylib.GetScreenHeight();
                xco = rnd.Next(10, Raylib.GetScreenWidth() - 10);
                pos.X = xco;
                //Draw();
                Draw();
                break;
            case (3):
                orientation = 1;
                xco = 0;
                pos.X = 0;
                yco = rnd.Next(10, Raylib.GetScreenHeight() - 10);
                pos.Y = yco;
                //Draw();
                Draw();
                break;

        }

    }

    public void Attack()
    {
        if (Raylib.IsKeyDown(KeyboardKey.KEY_K))
        {
            Vector2 dir = Input.GetNormalizedVector();
            if(dir.LengthSquared() ==  0f)
            {
                dir = new Vector2(1, 0);
            }
            Vector2 atPos = pos + (dir * 20);
            Raylib.DrawRectangleV(atPos, new Vector2(20,30), Raylib.YELLOW);
        }
    }

    public void MoveSteady()
    {
        MoveSteady(this.orientationV * speedInPixels);
    }

    public void MoveSteady(Vector2 velocity)
    {
        // ==>  veolocity wird hier also nie von alleine geringer!
        pos += velocity;
        // pos wird hier in jedem Durchlauf der Method um velocity erhoeht!
        // Wenn man direkt stoppen möchte, wenn man die Richtungstaste loslaesst, muss man velocity jetzt auf 0 setzen:
        // velocity = new Vector2();
        StayOnScreen();

        UpdateCoordinates();
        UpdateOrientation();
    }

    public void Move()
    {
        velocity += orientationV * acceleration;
        pos += velocity;
        StayOnScreen();
        velocity -= velocity * friction;  // je hoeher friction, dest mehr wird velocity verringert
        
        UpdateCoordinates();
        UpdateOrientation();
    }
    public void Move(Vector2 dir)
    {
        Move(dir, this.acceleration);
    }

    public void Move(Vector2 dir, float accel)
    {
        velocity += dir * accel;     // Velocity wird auf Richtung(1) mal acceleration gesetzt
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

    public void StayOnScreenCo()
    {
        if(xco < 0)
            xco = Raylib.GetScreenWidth(); 
        if(xco > Raylib.GetScreenWidth())
            xco = 0; 
        if(yco < 0)
            yco = Raylib.GetScreenHeight(); 
        if(yco > Raylib.GetScreenHeight())
            yco = 0;
        UpdateVector();
    }

    public void StayOnScreen()
    {
        if(pos.X < 0)
            pos.X = Raylib.GetScreenWidth(); 
        if(pos.X > Raylib.GetScreenWidth())
            pos.X = 0; 
        if(pos.Y < 0)
            pos.Y = Raylib.GetScreenHeight(); 
        if(pos.Y > Raylib.GetScreenHeight())
            pos.Y = 0;
        UpdateCoordinates();
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
    public void UpdateCoordinates()
    {
        xco = (int)pos.X;
        yco = (int)pos.Y;
    }

    public void UpdateVector()
    {
        pos.X = xco;
        pos.Y = yco;
    }


}


public enum MobSize
{
    S,
    M,
    L
}

public enum Direction
{
    N,
    E,
    S,
    W /*,
    NE,
    SE,
    SW,
    NW
        */
}
