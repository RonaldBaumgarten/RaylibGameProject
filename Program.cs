using Raylib_CsLo;

namespace HelloWorld;

class Program
{
    public static void Main()
    {
        Raylib.InitWindow(1600, 960, "Hello World");

        Game game = new Game();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.SetTargetFPS(60);
            Raylib.DrawFPS(12, 12);
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Raylib.WHITE);
            game.update();
            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}