using Raylib_CsLo;
using System.Numerics;
class MovableObject
{
    MobSize size;
    Color color;
    public int speedInPixels;
    protected int orientation;
    public int xco;
    public int yco;
    internal float acceleration = 2f;
    internal float friction = 0.1f;
    internal Vector2 pos;
    internal Vector2 velocity;

    public MovableObject()
    {
        color = Raylib.BLACK;
        size = MobSize.M;
        speedInPixels = 5;
        int orientation = 0;
        xco = Raylib.GetScreenWidth() / 2;
        yco = Raylib.GetScreenHeight() / 2;
        pos = new Vector2(xco * 1.0f, yco * 1.0f);
        velocity = new Vector2();
    }
    public MovableObject(Color c)
    {
        color = c;
        size = MobSize.M;
        speedInPixels = 5;
        int orientation = 0;
        xco = Raylib.GetScreenWidth() / 2;
        yco = Raylib.GetScreenHeight() / 2;
    }
    public void Draw()
    {
        switch (size)
        {
            case (MobSize):
                Raylib.DrawRectangle(xco, yco, 20, 30, color);
                break;

        }
    }
    public void DrawByVector()
    {
        Vector2 medium = new Vector2(20.0f, 30.0f);
        switch (this.size)
        {
            case (MobSize):
                Raylib.DrawRectangleV(pos, medium, color);
                break;
        }
    }

    public void Draw2()
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
        Move(m.orientation, 20);       
        Move(m.orientation, 10);       
        Move(m.orientation, 5);       
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
                Draw();
                break;
            case (1):
                orientation = 3;
                xco = Raylib.GetScreenWidth();
                pos.X = Raylib.GetScreenWidth();
                Draw();
                break;
            case (2):
                orientation = 0;
                yco = Raylib.GetScreenHeight();
                pos.Y = Raylib.GetScreenHeight();
                Draw();
                break;
            case (3):
                orientation = 1;
                xco = 0;
                pos.X = 0;
                Draw();
                break;

        }

    }

    public void UpdateCoordinates()
    {
        xco = (int)pos.X;
        yco = (int)pos.Y;
    }

    public void MoveByVectorOrientation()
    {
        MoveByVectorOrientation(orientation);
    }

    public void MoveByVectorOrientation(int direction)
    {
        MoveByVectorOrientation(direction, speedInPixels);
    }

    public void MoveByVectorOrientation(int direction, int speed)
    {
        if(orientation == 0)
        {
            pos.Y -= speedInPixels;
        }
        if(orientation == 1)
        {
            pos.X += speedInPixels;
        }
        if(orientation == 2)
        {
            pos.Y += speedInPixels;
        }
        if(orientation == 3)
        {
            pos.X -= speedInPixels;
        }
        UpdateCoordinates();
        StayOnScreen();
    }

    public void Move()
    {
        Move(orientation);
    }

    public void Move(int direction)
    {
        Move(direction, speedInPixels);
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
        StayOnScreen();
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
