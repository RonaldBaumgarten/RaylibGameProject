using Raylib_CsLo;
using System.Numerics;
using System.Collections.Generic;

class Game
{
    readonly bool WANT_ASTEROIDS = false;

    Player player;
    List<MovableObject> enemies = new List<MovableObject>();
    MovableObject enemy;
    Random r = new Random();
    int roundNumber = 1;

    private Asteroid asteroid;
    private Asteroid[] asteroids = new Asteroid[11];

    Enemy test;
    public Game()
    {
        player = new Player();
        enemy = new Enemy();
        enemy.Spawn();
        enemies.Add(enemy);
        test = new Enemy(Raylib.BLUE);
        test.pos = new Vector2(300, 400);
        //test.Spawn();
        //test.MoveByVectorWithoutFriction(new Vector2(1f, 0f));

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

    public void Update()
    {
        test.Draw();
        Vector2 vel = new Vector2(0f, 3f);
        test.MoveSteady(vel);

        player.Update();

        if (WANT_ASTEROIDS)
        {
            for (int i = 0; i < asteroids.Length; i++)
            {
                asteroids[i].Draw();
                asteroids[i].Update();
            }
        }

        // update roundNumber
        // roundValue = r.Next(0, 499);
        // for testing purposes roundValue lineary:
        roundNumber++;
        if(roundNumber  >= 350)
            roundNumber = 0;

        // let's see if we want to spawn another enemy
        if (roundNumber == 0)
        {
            enemies.Add(new Enemy());
            enemies[enemies.Count-1].Spawn();
        }
       
        // draw and move every enemy
        foreach(MovableObject m in enemies)
        {
            m.Draw();
            m.MoveSteady();
            //if (player.isCollidingByAxis(m))
            if (player.isColliding(m))
            {
                //Raylib.DrawText("Collision! ", 30, 30, 50, Raylib.BLACK);
                player.bump(m);
                m.bump(player); // funktioniert nicht mehr richtig mit vektorbasierter Bewegung - wahrscheinlich durch velocity?
            }
        }

    }

}
