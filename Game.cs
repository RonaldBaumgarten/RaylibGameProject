﻿using Raylib_CsLo;
using System.Numerics;
using System.Collections.Generic;

class Game
{
    // readonly bool WANT_ASTEROIDS = false;

    Player player;
    internal List<MovableObject> enemies = new List<MovableObject>();
    internal List<Attack> attacks = new List<Attack>();
    MovableObject toDelete;
    MovableObject enemy;
    Random r = new Random();
    int roundNumber = 1;

    public String messageA;
    public String messageB;

    Enemy test;
    public Game()
    {
        player = new Player(this);
        enemy = new Enemy(this);
        enemy.Spawn();
        enemies.Add(enemy);
    }

    public void Update()
    {
        Raylib.DrawText(messageA, 30, 30, 50, Raylib.BLACK);
        Raylib.DrawText(messageB, 30, 70, 50, Raylib.BLACK);

        Vector2 vel = new Vector2(0f, 3f);

        player.Update();

        /**** Random round number to use for spawning enemies ****/ // update roundNumber
        // roundValue = r.Next(0, 499);
        /**** For testing purposes roundValue lineary ***/
        roundNumber++;
        if(roundNumber  >= 250)
            roundNumber = 0;

        // let's see if we want to spawn another enemy
        if (roundNumber == 0)
        {
            enemies.Add(new Enemy(this));
            enemies[enemies.Count-1].Spawn();
        }
       
        // draw and move every enemy
        foreach(MovableObject m in enemies)
        {
            if (m.isActive)
            {
                m.Draw();
                m.MoveSteady();
                //if (player.isCollidingByAxis(m))
                if (player.isColliding(m))
                {
                    //messageA = "Collision!";
                    m.bump(player); // funktioniert nicht mehr richtig mit vektorbasierter Bewegung - wahrscheinlich durch velocity?
                    player.bump(m);
                }
            } else
            {
                toDelete = m;
            }
        }

        enemies.Remove(toDelete);
        
        foreach(Attack a in attacks)
        {
            a.Update();
            if (a.isActive)
            {
            //    messageA = "HIER!";
                a.Draw();
                foreach (MovableObject m in this.enemies)
                {
                    if (a.isColliding(m))
                    {
                        player.velocity = new Vector2(0,0);
                        m.hp -= 1;
                        if(m.hp<=0)
                        {
                        a.bump(m);
                        m.isActive = false;
                        } else
                        {
                            a.bump(m);
                        }
                    }
                }
            }

        }

    }

}
