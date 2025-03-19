using Raylib_CsLo;

namespace RaylibGameProject;

class Program
{
    readonly static int WINDOWW = 1600;
    readonly static int WINDOWH = 960;
    public static void Main()
    {
        Raylib.InitWindow(WINDOWW, WINDOWH, "Hello World");

        Game game = new Game();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.SetTargetFPS(60);
            Raylib.DrawFPS(12, 12);
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.BEIGE);
            game.Update();
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}