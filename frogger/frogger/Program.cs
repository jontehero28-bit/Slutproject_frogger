global using Raylib_cs;
global using System.Numerics;
global using System.Threading;

Raylib.InitWindow(1000, 1000, "Frogger game"); //rutor där 100px X 100px. Då spelet är 10 rutor bredd och 10 lång
Raylib.SetTargetFPS(3);  

Texture2D frog = Raylib.LoadTexture("frog-1.png.png");

Rectangle player = new Rectangle(500, 900, frog.width, frog.height);

string currentScene = "start"; //start, game, end,
float timerMaxValue = 100;   //100 sekunder för att klara av banan
float timerCurrentValue = 100;
int jump = 100; //jump ska vara en ruta stor, så 100px.
//____________________________________________________________


while(Raylib.WindowShouldClose() == false)
{

//logik______________________________________________________

if (currentScene == "game")
{
    if(timerCurrentValue >= 0)
    {
    timerCurrentValue--;
    }
    
    if(Raylib.IsKeyPressed(KeyboardKey.KEY_W)  || Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
    {
        player.y -= jump;
    }
}
else if (currentScene == "start")
{
    if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
    {
        currentScene = "game";
    }
}
else if (currentScene == "end")
{
    if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
    {
        currentScene = "game";
        timerCurrentValue = timerMaxValue;
    }
}

if (timerCurrentValue == 0)
{
    currentScene = "end";
    
}


//grafik____________________________________________________

Raylib.BeginDrawing();
Raylib.ClearBackground(Color.WHITE);

if (currentScene == "game")
{
    Raylib.ClearBackground(Color.GRAY);
    Raylib.DrawRectangle(1, 900, 1000, 100, Color.GREEN); //grässmatta, spawn punkt
    Raylib.DrawRectangle(1, 600, 1000, 300, Color.BLACK); //bilväg
    Raylib.DrawRectangle(1, 500, 1000, 100, Color.GREEN); //safezone
    Raylib.DrawRectangle(1, 300, 1000, 200, Color.BLUE); //water
    Raylib.DrawRectangle(1, 100, 1000, 200, Color.BLACK);//bilväg
    Raylib.DrawRectangle(1, 1, 1000, 100, Color.GREEN);//safezone

    Raylib.DrawText($"Time: {timerCurrentValue}", 1, 930, 40, Color.BLACK);

    Raylib.DrawTexture(frog, (int)player.x, (int)player.y, Color.WHITE);

}
else if (currentScene == "start")
{
    Raylib.DrawText("FROGGER", 430, 350, 36, Color.BLACK);
    Raylib.DrawText("Press ENTER to start", 350, 450, 34, Color.BLACK);
}
else if (currentScene == "end")
{
    Raylib.ClearBackground(Color.RED);
    Raylib.DrawText("GAME OVER", 430, 350, 36, Color.BLACK);
    Raylib.DrawText("Press ENTER to play again", 320, 450, 34, Color.BLACK);
    
}

Raylib.EndDrawing();
}