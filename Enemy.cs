﻿using Raylib_CsLo;
using System.Numerics;

 class Enemy : MovableObject
{
    //public Enemy() : this(Raylib.BLACK) { }
    public Enemy(Game g) : this(Raylib.BLACK) 
    {
        game = g;
    }

    public Enemy(Color c) : base(c) 
    {
        acceleration = 1.2f;
        friction = 0.2f;
        orientationV = new Vector2();

        Init();
    }

    private void Init()
    {
        Random random = new Random();

        int randPosX = Raylib.GetRandomValue(0, Raylib.GetScreenWidth());
        int randPosY = Raylib.GetRandomValue(0, Raylib.GetScreenWidth());
        pos = new Vector2(randPosX, randPosY);
        // create random direction to fly in
        while (orientationV.LengthSquared() == 0f)
        {
            float randXdir = random.NextSingle() * 2f - 1;
            float randYdir = random.NextSingle() * 2f - 1;
            orientationV = new Vector2(randXdir, randYdir);
        }
        orientationV = Vector2.Normalize(orientationV);

    }
}

