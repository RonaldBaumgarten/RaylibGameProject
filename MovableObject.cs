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
    private float acceleration = 2f;
    private float friction = 0.1f;
    private Vector2 pos;
    private Vector2 velocity;

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
        pos = new Vector2(xco, yco);
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

        if(this.xco <= m.xco+width && this.xco+width >= m.xco)
        {
            if(this.yco <= (m.yco+heigth) && this.yco+heigth >= m.yco)
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
                Draw();
                break;
            case (1):
                orientation = 3;
                xco = Raylib.GetScreenWidth();
                Draw();
                break;
            case (2):
                orientation = 0;
                yco = Raylib.GetScreenHeight();
                Draw();
                break;
            case (3):
                orientation = 1;
                xco = 0;
                Draw();
                break;

        }

    }

    public void UpdateCoordinates()
    {
        xco = (int)pos.X;
        yco = (int)pos.Y;
    }

    public void MoveByVector()
    {
        // Bewegt das Mob direkt mit input
        Vector2 dir = new Vector2(Input.GetAxis("horizontal"), Input.GetAxis("vertical"));
        if (dir.LengthSquared() > 0f)
        {
            dir = Vector2.Normalize(dir);
        }
        velocity += dir * acceleration;
        pos += velocity;

        velocity -= velocity * friction;


        UpdateCoordinates();
        //Raylib.DrawText("Orientation: " + orientation, 30, 30, 50, Raylib.BLACK);
        Raylib.DrawText("Velocity : " + velocity, 30, 30, 50, Raylib.BLACK);

    }

    public void MoveByVectorOrientation()
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
    }

    public void Move()
    {
        switch (orientation)
        {
            case 0:
                yco -= speedInPixels;
                if (yco < 0)
                    yco = Raylib.GetScreenWidth();
                break;
            case 1:
                xco += speedInPixels;
                if (xco > Raylib.GetScreenWidth())
                    xco = 0;
                break;
            case 2:
                yco += speedInPixels;
                if (yco > Raylib.GetScreenWidth())
                    yco = 0;
                break;
            case 3:
                xco -= speedInPixels;
                if (xco < 0)
                    xco = Raylib.GetScreenWidth();
                break;
        }
        //Raylib.DrawText("X = " + xco, 30, 30, 50, Raylib.BLACK);
    }


    public void Move(int direction)
    {
        switch (direction)
        {
            case 0:
                yco -= speedInPixels;
                if(yco < 0)
                    yco = Raylib.GetScreenWidth(); 
                break;
            case 1:
                xco += speedInPixels;
                if(xco > Raylib.GetScreenWidth())
                    xco = 0; 
                break;
            case 2:
                yco += speedInPixels;
                if(yco > Raylib.GetScreenWidth())
                    yco = 0; 
                break;
            case 3:
                xco -= speedInPixels;
                if(xco < 0)
                    xco = Raylib.GetScreenWidth();
                    
                break;
        }
    }

    public void Move(int direction, int speed)
    {
        switch (direction)
        {
            case 0:
                yco -= speed;
                if(yco < 0)
                    yco = Raylib.GetScreenWidth(); 
                break;
            case 1:
                xco += speed;
                if(xco > Raylib.GetScreenWidth())
                    xco = 0; 
                break;
            case 2:
                yco += speed;
                if(yco > Raylib.GetScreenWidth())
                    yco = 0; 
                break;
            case 3:
                xco -= speed;
                if(xco < 0)
                    xco = Raylib.GetScreenWidth();
                    
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
