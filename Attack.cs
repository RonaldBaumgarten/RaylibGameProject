using Raylib_CsLo;
using System.Data;
using System.Numerics;

class Attack : MovableObject
{
    float lifetime;

    public Attack() : base(Raylib.YELLOW)
    {
        lifetime = 0.5f;
        timer = 0f;
    }

    public Attack(Vector2 pos, Color c, Vector2 o) : base(pos, c, o) 
    {
        lifetime = 1.0f;
    }

    internal void Update()
    {
        if (!isActive) return;

        timer += Raylib.GetFrameTime();
        if(timer >= lifetime)
        {
            timer = 0f;
            isActive = false;
        }
    }
}
