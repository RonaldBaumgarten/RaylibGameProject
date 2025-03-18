using Raylib_CsLo;
using System.Numerics;
using System.Collections.Generic;

class Game
{
    readonly bool WANT_ASTEROIDS = true;

    Player player;
    List<MovableObject> enemies = new List<MovableObject>();
    MovableObject enemy;
    Random r = new Random();
    int roundValue = 1;

    private Asteroid asteroid;
    private Asteroid[] asteroids = new Asteroid[11];

    public Game()
    {
        player = new Player();
        enemy = new MovableObject();
        enemy.Spawn();
        enemies.Add(enemy);

        if (WANT_ASTEROIDS)
        {
        // create Asteroids:
        for(int i = 0; i < 11; i++)
        {
            asteroids[i] = new Asteroid();
            asteroids[i].Init();
        }

        }
    }

    public void update()
    {

        player.update();

        if (WANT_ASTEROIDS)
        {
            for (int i = 0; i < asteroids.Length; i++)
            {
                asteroids[i].Draw();
            }
        }

        // update roundValue
        // roundValue = r.Next(0, 499);
        // for testing purposes roundValue lineary
        roundValue++;
        if(roundValue  >= 350)
            roundValue = 0;

        // let's see if we want to spawn another enemy
        if (roundValue == 0)
        {
            enemies.Add(new MovableObject());
            enemies[enemies.Count-1].Spawn();
        }
       
        // draw and move every enemy
        foreach(MovableObject m in enemies)
        {
            //m.Draw(); 
            m.DrawByVector();
            m.Move();
            //m.MoveByVectorOrientation;
            if (player.isCollidingByAxis(m))
            {
                player.bump(m);
                m.bump(player);
            }
        }

    }

}
