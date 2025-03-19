﻿using Raylib_CsLo;
using System.Numerics;
 abstract class MovableObject
{
    MobSize size;
    Color color;
    public int speedInPixels;
    protected int orientation;
    protected Vector2 oVec;
    public int xco;
    public int yco;
    internal float acceleration;
    internal float friction;
    internal Vector2 pos;
    internal Vector2 velocity;
    // ist orientation als Vektor notwendig? Wenn muss zur Darstellung auf jeden Fall wissen, wohin die Figur schaut.... oder?
  

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
        // Von welcher Seite werden wir weggestoßen?
        if(!(this.orientation == m.orientation))
        {
            Move(m.orientation, 20);       
            Move(m.orientation, 10);       
            Move(m.orientation, 5);       

        }
        else
        {
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

    public void MoveSteady()
    {
        MoveSteady(this.oVec * speedInPixels);
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

    public void Move(int direction, int speed)
    {
        switch (direction)
        {
            case 0:
                yco -= speed;
                break;
            case 1:
                xco += speed;
                break;
            case 2:
                yco += speed;
                break;
            case 3:
                xco -= speed;
                break;
        }
        StayOnScreenCo();
        UpdateVector();
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
