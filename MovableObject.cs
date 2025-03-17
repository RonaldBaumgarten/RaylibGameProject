using Raylib_CsLo;
using System;
class MovableObject
{
    MobSize size;
    Color color;
    int speedInPixels;
    protected int orientation;
    int xco;
    int yco;

    public MovableObject()
    {
        color = Raylib.BLACK;
        size = MobSize.M;
        speedInPixels = 5;
        int orientation = 0;
        xco = Raylib.GetScreenWidth() / 2;
        yco = Raylib.GetScreenHeight() / 2;
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
        Raylib.DrawText("X = " + xco, 30, 30, 50, Raylib.BLACK);
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
                {
                    xco = Raylib.GetScreenWidth();
                }
                    
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
