using Raylib_cs;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;

Raylib.InitWindow(800, 600, "screen");
Raylib.SetTargetFPS(60);

// -----------------------------------------------------------------------------
// random och listor
// -----------------------------------------------------------------------------

Random generator = new Random();

// list<string> names = den kommer innehålla flera string variabler. Listor börjar på 0 (Mario = names[0])
List<string> names = new List<string>() {"Mario", "Luigi", "Toad", "Bowser", "Peach"};
// names.Add("Mario");

// int i = generator.Next(5); 
int i = generator.Next(names.Count);

// ------------------------------------------------------------------------------
// karaktärer, väggar mm.
// ------------------------------------------------------------------------------
Texture2D characterImage = Raylib.LoadTexture("bunny.png");

Rectangle characterRect = new Rectangle(400, 300, 64, 64);
Vector2 movement = new Vector2(0, 0);

Rectangle doorRect = new Rectangle(5, 100, 50, 50);
Rectangle wallRect = new Rectangle(50, 70, 400, 20);

float speed = 5;
int scene = 0;
int points = 0;
// int HP = 3;


while (!Raylib.WindowShouldClose())
{
    // --------------------------------------------------------------------------
    // movement
    if (scene == 1 || scene == 2)
    {
        movement = Vector2.Zero;

        if (Raylib.IsKeyDown(KeyboardKey.KEY_D))
        {
            movement.X = 1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_A))
        {
            movement.X = -1;
        }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_W))
        {
            movement.Y = -1;
        }
        else if (Raylib.IsKeyDown(KeyboardKey.KEY_S))
        {
            movement.Y = 1;
        }
        if (movement.Length() > 0)
        {
            movement = Vector2.Normalize(movement) * speed;
        }

        characterRect.x += movement.X;
        characterRect.y += movement.Y;

        // --------------------------------------------------------------------------------
        // gå ej utanför skärmen

        if (characterRect.x > 800 - characterRect.width || characterRect.x < 0)
        {
            characterRect.x -= movement.X;
        }
        if (characterRect.y > 600 - characterRect.height || characterRect.y < 0)
        {
            characterRect.y -= movement.Y;
        }

        // ------------------------------------------------------------------------
        // byta level/kolla kollision
        if (Raylib.CheckCollisionRecs(characterRect, doorRect))
        {
            scene++;
            points++;
            characterRect.x += 300;
            characterRect.y += 100;
        }
        if (Raylib.CheckCollisionRecs(characterRect, wallRect))
        {
            // HP--;
            // Thread.Sleep(1000);
            scene = 101;
        }
    }
    // --------------------------------------------------------------------------
    // start skärm
    else if (scene == 0)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            // HP = 3;
            points = 0;
            scene = 1;
        }
    }
    else if (scene == 101)
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_SPACE))
        {
            // HP = 3;
            points = 0;
            scene = 1;
        }
    }

    // ----------------------------------------------------------------------------
    // DRAWING
    // ----------------------------------------------------------------------------
    Raylib.BeginDrawing();
    if (scene == 1)
    {
        Raylib.ClearBackground(Color.DARKPURPLE);
        // Raylib.DrawRectangleRec(characterRect, Color.WHITE);
        Raylib.DrawTexture(characterImage, (int)characterRect.x, (int)characterRect.y, Color.WHITE);
        Raylib.DrawRectangleRec(doorRect, Color.WHITE);
        Raylib.DrawRectangleRec(wallRect, Color.BROWN);
        DrawPoints(points);
        // Raylib.DrawText($"HP: {HP}", 600, 10, 40, Color.WHITE);

    }
    else if (scene == 2)
    {
        Raylib.ClearBackground(Color.DARKGREEN);
        Raylib.DrawTexture(characterImage, (int)characterRect.x, (int)characterRect.y, Color.WHITE);
        Raylib.DrawRectangleRec(doorRect, Color.GOLD);
        Raylib.DrawRectangleRec(wallRect, Color.BROWN);
        DrawPoints(points);
        // Raylib.DrawText($"HP: {HP}", 600, 10, 40, Color.WHITE);
    }
    else if (scene == 0)
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText("PRESS SPACE TO START", 200, 300, 40, Color.WHITE);
    } 
    else if (scene > 2)
    {
        // 101 för GAME OVER
        scene = 101;
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText("Game Over", 300, 300, 40, Color.WHITE);
        DrawPoints(points);
        Raylib.DrawText("Press space to try again.", 10, 10, 10, Color.WHITE);
    }
    Raylib.EndDrawing();
}


// ------------------------------------------------------------------------------------------
// METODER
// ------------------------------------------------------------------------------------------

static void DrawPoints(int points)
{
    Raylib.DrawText($"Points: {points}", 300, 350, 40, Color.WHITE);
}