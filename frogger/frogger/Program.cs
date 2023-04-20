global using Raylib_cs;
global using System.Numerics;
global using System.Threading;

Raylib.InitWindow(1000, 1000, "Frogger game"); //rutor där 100px X 100px. spelet 10 rutor bredd och lång
Raylib.SetTargetFPS(60);

Texture2D frog = Raylib.LoadTexture("frog-1.png.png"); //laddar upp frog sprite

Rectangle player = new Rectangle(550, 950, frog.width, frog.height);//skapar player


string currentScene = "start"; //start, game, end,
float timerMaxValue = 100;   //100 sekunder för att klara av banan. Maxvalue är tiden när man startar
float timerCurrentValue = 100;//nuvarande tiden
int jump = 100; //jump ska vara en ruta stor, så 100px.
int degrees = 0;//rotera player

List<Obstacle> cars = new List<Obstacle>(); //lista för bilar
//____________________________________________________________

cars.Add(new(700)); //start position för första bilen Den har bara värde för carY position
cars.Add(new(800)); //andra bilen
cars.Add(new(600)); // tredje bilen
cars.Add(new(100)); //fjärde bilen
cars.Add(new(200)); //femte bilen

while (Raylib.WindowShouldClose() == false)//medans spelfönster är öppet ska hela koden under köras
{

    //logik______________________________________________________

    if (currentScene == "game")//ifall scenen är på game då ska
    {
        if (timerCurrentValue > 0)//timern går ner per frame tid (varje sekund)
        {
            timerCurrentValue -= Raylib.GetFrameTime();//nuvarande tid kvar - framestime
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

        if (player.y >= 1000)      //så player skulle inte gå utanför scenen i varje håll förutom upp
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


        foreach (Obstacle c in cars)
        {
        c.Update();
        }

    }
//____________________________________________________________________________

    
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
            timerCurrentValue = timerMaxValue;    //återställer timern till maximala tiden (tid från början)
        }
    }

    if (timerCurrentValue <= 0)//ifall tiden är ute då ska
    {
        currentScene = "end";//game over

    }


    //grafik_________________________________________________________________________

    Raylib.BeginDrawing();//påbörkar ritning
    Raylib.ClearBackground(Color.WHITE);

    if (currentScene == "game")
    {
        level1.DrawLevel1(); //metod för att rita upp level

        Raylib.DrawText($"Time: {(int)timerCurrentValue}", 1, 930, 40, Color.BLACK);

        Raylib.DrawTexturePro(frog, new Rectangle(0,0,100,100), player, new Vector2(50,50), degrees, Color.WHITE);//(texture, Rectangle source, Rectangle dest, Vector2 origin, rotation, Color)

        foreach (Obstacle c in cars)
        {
        c.Draw();
            
        }

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