using Raylib_CsLo;

 class Game
{
    Player player;
    MovableObject enemy;
    public Game()
    {
        player = new Player();
        enemy = new MovableObject();
        enemy.Spawn();
    }

    public void update()
    {
        player.Draw();
        enemy.Draw();
        enemy.Move();

    }

}
