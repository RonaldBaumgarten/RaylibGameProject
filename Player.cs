using Raylib_CsLo;
using System.Transactions;
using System.Numerics;

class Player : MovableObject
{
    float attackCooldown;
    bool canAttack;
    public Player(Game g) : base(Raylib.RED)
    {
        base.acceleration = 2.0f;
        base.friction = 0.3f;
        orientationV = new Vector2(1, 0);
        game = g;
        canAttack = true;
        attackCooldown = 1f;
    }

    internal void Update()
    {
        MovePlayer();
        Draw();
        Attack();

        //game.messageA = "CAN attack";
        if (canAttack) return;

        game.messageA = "can NOT attack";

        timer += Raylib.GetFrameTime();
        if(timer >= attackCooldown)
        {
            game.messageA = "Timer >= Cooldown";
            timer = 0f;
            canAttack = true;
        }
    }


    public void MovePlayer()
    {
        // Bewegt Player direkt mit input
        Vector2 newOrientation = Input.GetNormalizedVector();
        if(newOrientation.LengthSquared() != 0) 
        { 
            orientationV = newOrientation;    // Vector = ( -1/1 | -1/1 )
        }
        Move(Input.GetNormalizedVector());
    }

    public void Attack()
    /*** We need to bump enemy that is some pixels away and then delete it from Game's enemy-list ***/
    {

        if (Raylib.IsKeyDown(KeyboardKey.KEY_K))
        {
            if (!canAttack) return;

            canAttack = false;
            Vector2 dir = orientationV;
            if (dir.LengthSquared() == 0f)
            {
                dir = new Vector2(1, 0);
            }
            Vector2 atPos = pos + (dir * 20);
            Attack attack = new Attack(atPos, Raylib.YELLOW, this.orientationV);
            attack.size = MobSize.L;
            game.attacks.Add(attack);
        }
    }

}
