using Raylib_CsLo;
using System.Data;
using System.Numerics;

class Attack : MovableObject
{
    float lifetime;
    Player player;

    //public Attack() : base(Raylib.YELLOW)
    //{
    //    lifetime = 0.5f;
    //    timer = 0f;
    //}

    public Attack(Vector2 pos, Color c, Vector2 o, Player p) : base(pos, c, o) 
    {
        lifetime = 0.2f;
        player = p;
        acceleration = p.acceleration;
        friction = p.friction;
    }

    internal void Update()
    {
        /**** Should Move with Player: ***/

        this.velocity = player.velocity;
        orientationV = player.orientationV;    // Vector = ( -1/1 | -1/1 )
        Move(Input.GetNormalizedVector());

        if (!isActive) return;

        timer += Raylib.GetFrameTime();
        if(timer >= lifetime)
        {
            timer = 0f;
            isActive = false;
        }
    }
}
