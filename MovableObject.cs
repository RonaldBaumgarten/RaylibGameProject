using Raylib_CsLo;
using System.Numerics;
  class MovableObject
{
    internal Game game;
    internal MobSize size;
    Color color;
    public int speedInPixels;   // TO-DO: wird nurnoch fuer MoveSteady() benutzt
    protected int orientation;
    internal float acceleration;
    internal float friction;
    internal Vector2 pos;
    internal Vector2 velocity;
    internal Vector2 orientationV;
    internal bool isActive;
    internal float timer;
    internal int hp;

    public MovableObject(Color c) : this(new Vector2(Raylib.GetScreenWidth() / 2, Raylib.GetScreenHeight() / 2), c) { }

    public MovableObject(Vector2 pos, Color c) :  this( pos,  c, new Vector2(Raylib.GetScreenWidth()) ) {}

    public MovableObject(Vector2 pos, Color c, Vector2 o)
    {
        color = c;
        size = MobSize.M;
        speedInPixels = 5;
        int orientation = 0;
        // TO-DO: add orientation/direction vector
        this.pos = pos;
        velocity = new Vector2(0,0);
        orientationV = o;
        isActive = true;
        timer = 0f;
        hp = 2;
    }


    public void Draw()
    {
        float width;
        float height;
        switch (this.size)
        {
            case (MobSize.S):
                width = 10;
                height = 10;
                break;
            case (MobSize.M):
                width = 20;
                height = 30;
                break;
            default:
                width = 30;
                height = 40;
                break;
        }
        Vector2 size = new Vector2(width, height);
        /*
        switch (this.size)
        {
            case (MobSize):
        */
                Raylib.DrawRectangleV(pos, size, color);
        /*
                break;
        }
        */
    }


    public void DrawAsCircle()
    {
        Raylib.DrawCircleV(pos, 20f, Raylib.YELLOW);
    }

    public bool isColliding(MovableObject m)    // Benutzt gerade noch keine size des Obejektes!!!
    {
        int width;
        int height;
        switch (this.size)
        {
            case (MobSize.S):
                width = 10;
                height = 10;
                break;
            case (MobSize.M):
                width = 20;
                height = 30;
                break;
            default:
                width = 30;
                height = 40;
                break;
        }

        if (this.pos.X <= m.pos.X + width && this.pos.X + width >= m.pos.X)
        {
            if (this.pos.Y <= (m.pos.Y + height) && this.pos.Y + height >= m.pos.Y)
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
        String prod = "Angle between MoBs: " + p;       // Debug-Message
        //game.messageB = prod;                         // Debug-Message - gibt gerade Fehler!!!

        /*** When MoBs are not facing the same direction: ***/
        // the angle between the two vectors is less than 90 degrees if Dot-Product > 0!
        if(p <= 0)
        {
            //game.messageA = "NOT same orientation";
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
                pos.Y = 0;
                pos.X = rnd.Next(10, Raylib.GetScreenWidth() - 10);
                Draw();
                break;
            case (1):
                orientation = 3;
                pos.X = Raylib.GetScreenWidth();
                pos.Y = rnd.Next(10, Raylib.GetScreenHeight() - 10);
                Draw();
                break;
            case (2):
                orientation = 0;
                pos.Y = Raylib.GetScreenHeight();
                pos.X = rnd.Next(10, Raylib.GetScreenWidth() - 10);
                Draw();
                break;
            case (3):
                orientation = 1;
                pos.X = 0;
                pos.Y = rnd.Next(10, Raylib.GetScreenHeight() - 10);
                Draw();
                break;

        }

    }

   

    public void MoveSteady()
    {
        MoveSteady(this.orientationV * speedInPixels);
    }

    public void MoveSteady(Vector2 velocity)
    {
        pos += velocity;
        // pos wird hier in jedem Durchlauf der Method um velocity erhoeht!
        // Wenn man direkt stoppen möchte, wenn man die Richtungstaste loslaesst, muss man velocity jetzt auf 0 setzen:
        // velocity = new Vector2();
        StayOnScreen();
        UpdateOrientation();
    }

    public void Move()
    {
        Move(orientationV);
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

        UpdateOrientation();
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
