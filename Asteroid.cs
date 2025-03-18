using System.Numerics;
using Raylib_CsLo;
using System;
class Asteroid
{
    private Vector2 pos;
    private Vector2 dir;
    private float radius = 30f;
    private bool isActive;
    private Random random = new Random();

    public void Init()
    {
        int randPosX = Raylib.GetRandomValue(0, Raylib.GetScreenWidth());
        int randPosY = Raylib.GetRandomValue(0, Raylib.GetScreenWidth());
        pos = new Vector2(randPosX, randPosY);
        // create random direction to fly in
        while (dir.LengthSquared() == 0f)
        {
            float randXdir = random.NextSingle() * 2f - 1;
            float randYdir = random.NextSingle() * 2f - 1;
            dir = new Vector2(randXdir, randYdir);
        }
        dir = Vector2.Normalize(dir);
    }

    public void Update()
    {
        pos +=  dir;
    }

    public void Draw()
    {
        if (!isActive)
        {
            Raylib.DrawCircleV(pos, radius, Raylib.BLUE);
        }
    }

}
