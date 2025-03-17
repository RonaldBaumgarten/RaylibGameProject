using Raylib_CsLo;

class Game
{

    Player player;
    List<MovableObject> enemies = new List<MovableObject>();
    MovableObject enemy;
    Random r = new Random();
    int roundValue = 1;

    public Game()
    {
        player = new Player();
        enemy = new MovableObject();
        enemy.Spawn();
        enemies.Add(enemy);
    }

    public void update()
    {

        player.Draw();
        player.update();

        // update roundValue
        // roundValue = r.Next(0, 499);
        // for testing purposes roundValue lineary
        roundValue++;
        if(roundValue  >= 350)
            roundValue = 0;

        // let's see if we want to spawn anoter enemy
        if (roundValue == 0)
        {
            enemies.Add(new MovableObject());
            enemies[enemies.Count-1].Spawn();
        }
       
        // draw and move every enemy
        foreach(MovableObject m in enemies)
        {
            m.Draw();
            m.Move();
        }

    }

}
