global using Raylib_cs;
global using System.Numerics;
global using System.Threading;

Raylib.InitWindow(1000, 1000, "Frogger game"); //rutor där 100px X 100px. Då spelet är 10 rutor bredd och 10 lång
Raylib.SetTargetFPS(60);

Texture2D frog = Raylib.LoadTexture("frog-1.png.png"); //laddar upp frog sprite

Rectangle player = new Rectangle(550, 950, frog.width, frog.height);//skapar player

string currentScene = "start"; //start, game, end,
float timerMaxValue = 1000;   //100 sekunder för att klara av banan
float timerCurrentValue = 1000;
int jump = 100; //jump ska vara en ruta stor, så 100px.
int degrees = 0;//rotera player
//____________________________________________________________



while (Raylib.WindowShouldClose() == false)//medans spelet är öppet ska hela koden under köras
{

    //logik______________________________________________________

    if (currentScene == "game")//ifall scenen är på game då ska
    {
        if (timerCurrentValue >= 0)//timern går ner per frame tid (varje sekund)
        {
            timerCurrentValue -= Raylib.GetFrameTime();
        }

        if (Raylib.IsKeyPressed(KeyboardKey.KEY_W) || Raylib.IsKeyPressed(KeyboardKey.KEY_UP))//up
        {
            player.y -= jump;
            degrees = 0;       //kolla up
        }

        else if (Raylib.IsKeyPressed(KeyboardKey.KEY_S) || Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))//ner
        {
            player.y += jump;
            degrees = 180;      //kollar ner
        }

        else if (Raylib.IsKeyPressed(KeyboardKey.KEY_D) || Raylib.IsKeyPressed(KeyboardKey.KEY_RIGHT))//höger
        {
            player.x += jump;
            degrees =  90;     //kolla höger
        }

        else if (Raylib.IsKeyPressed(KeyboardKey.KEY_A) || Raylib.IsKeyPressed(KeyboardKey.KEY_LEFT))//vänster
        {
            player.x -= jump;
            degrees = 270;      //kolla vänster
        }

        if (player.y >= 1000)      //så player skulle inte gå utanför scenen
        {
            player.y -= jump;
        }
        if (player.x >= 1000 )
        {
            player.x -= jump;
        }
        if (player.x <= 1)
        {
            player.x += jump;
        }

//____________________________________________________________________________

    }
    else if (currentScene == "start")//ifall scenen är på start då ska
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))//går till scen game
        {
            currentScene = "game";
        }
    }
    else if (currentScene == "end")//ifall scenen är på end då ska
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))//gå till scen game
        {
            currentScene = "game";  
            timerCurrentValue = timerMaxValue;    //återställer timern
        }
    }

    if (timerCurrentValue == 0)//ifall tiden är ute då ska
    {
        currentScene = "end";//game over

    }


    //grafik_________________________________________________________________________

    Raylib.BeginDrawing();//påbörkar ritning
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

        Raylib.DrawText($"Time: {(int)timerCurrentValue}", 1, 930, 40, Color.BLACK);

        Raylib.DrawTexturePro(frog, new Rectangle(0,0,100,100), player, new Vector2(50,50), degrees, Color.WHITE);//(texture, Rectangle source, Rectangle dest, Vector2 origin, rotation, Color)

    }

    else if (currentScene == "start")
    {
        Raylib.DrawText("FROGGER", 430, 350, 36, Color.BLACK);             // ritar upp text
        Raylib.DrawText("Press ENTER to start", 350, 450, 34, Color.BLACK);
    }

    else if (currentScene == "end")
    {
        Raylib.ClearBackground(Color.RED); //röd bakgrund 
        Raylib.DrawText("GAME OVER", 430, 350, 36, Color.BLACK);//text
        Raylib.DrawText("Press ENTER to play again", 320, 450, 34, Color.BLACK);

    }

    Raylib.EndDrawing();//avslutar ritning
}