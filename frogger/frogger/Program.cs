global using Raylib_cs;
global using System.Numerics;
global using System.Threading;

Raylib.InitWindow(1000, 1000, "Frogger game"); //rutor där 100px X 100px. spelet 10 rutor bredd och lång
Raylib.SetTargetFPS(60);

Texture2D frog = Raylib.LoadTexture("frog-1.png.png"); //laddar upp frog sprite

Rectangle player = new Rectangle(550, 950, frog.width, frog.height);//skapar player


string currentScene = "start"; //start, game, end, win
float timerMaxValue = 60;   //100 sekunder för att klara av banan. Maxvalue är tiden när man startar
float timerCurrentValue = 60;//nuvarande tiden
int jump = 100; //jump ska vara en ruta stor, så 100px.
int degrees = 0;//rotera player

Rectangle frogCollider = player; //ändra min rectangle hitbox till mitten av spriten. Ändrar positionen längre ner


//använder listor för att jag vill lägga till mer i dem efter skapelse
List<Obstacle> cars = spawncars(); //lista för bilar där jag skapar de med bilar i
List<Platform> logs = spawnlogs(); //lista för platoformer där jag skapar de med plattformar i
List<Water> waters = spawnwater(); //lista för vatten rectangel där jag skapar de med vatten i

//Jag använder listor istället för array för att enkelt lägga till fler saker senare i spelet. Plus det emklare att använda listor (dock listor är långsammare)


//________________________________________________________________________________________________

while (true)//medans while är true ska hela koden under köras
{
    
    //logik______________________________________________________


    if (currentScene == "game")//ifall scenen är på game då ska
    {
        frogMove(player, jump, degrees, frogCollider, currentScene, timerCurrentValue); //här använder jag frog metod
        collisions(cars, waters, frogCollider, currentScene); //kollisioner för spelaren och bilar och vatten.

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
            player.x = 550;     //ifall game over jag tar x, y och degrees värde till samma som var från början.
            player.y = 950;
            degrees = 0;
            
        }
    }

    else if (currentScene == "win")
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))//gå till start
        {
            currentScene = "start";
            timerCurrentValue = timerMaxValue;    //återställer timern till maximala tiden (tid från början)
            player.x = 550;     //ifall game over jag tar x, y och degrees värde till samma som var från början.
            player.y = 950;
            degrees = 0;
        }
    }

    if (timerCurrentValue <= 0)//ifall tiden är ute då ska
    {
        currentScene = "end";//game over
    }

    //grafik_________________________________________________________________________


    Raylib.BeginDrawing();//påbörkar ritning
    Raylib.ClearBackground(Color.WHITE);
    
    scences(currentScene, timerCurrentValue, degrees, logs, cars, player, frog);

    Raylib.EndDrawing();//avslutar ritning
}
static List<Obstacle> spawncars()
{
    List<Obstacle> cars = new();
    cars.Add(new(700)); //start position för första bilen Den har bara värde för carY position
    cars.Add(new(800)); //andra bilen
    cars.Add(new(600)); // tredje bilen
    cars.Add(new(100)); //fjärde bilen
    cars.Add(new(200)); //femte bilen
    return cars;
}
static List<Platform> spawnlogs() 
{
    List<Platform> logs = new();
    logs.Add(new(300, 300)); //platformerna
    logs.Add(new(300, 400));
    logs.Add(new(600, 300));
    logs.Add(new(600, 400));
    return logs;
}
static  List<Water> spawnwater() 
{
    List<Water> waters = new();
    waters.Add(new(1, 300, 300)); //lista för vatten
    waters.Add(new(400, 300, 200));
    waters.Add(new(700, 300, 300));
    return waters;
}
static void scences(string currentScene, float timerCurrentValue, int degrees, List<Platform> logs, List<Obstacle> cars, Rectangle player, Texture2D frog) 
{
    if (currentScene == "game")
    {
        level1.DrawLevel1(); //metod för att rita upp level

        Raylib.DrawText($"Time: {(int)timerCurrentValue}", 1, 930, 40, Color.BLACK);

        foreach (Obstacle c in cars)   //execute draw metoden för (c) item
        {
            c.Draw();
        }
        
        foreach (Platform p in logs) //ritar ut platformen
        {
            p.DrawPlatform();
        }

        

        Raylib.DrawTexturePro(frog, new Rectangle(0 + degrees, 0 + degrees, 90, 90), player, new Vector2(45, 45), degrees, Color.WHITE);//(texture, Rectangle source, Rectangle dest, Vector2 origin, rotation, Color)
    }//___________________________________________________________________________________________

    else if (currentScene == "start")
    {
        Raylib.DrawText("FROGGER", 430, 350, 36, Color.BLACK);             // ritar upp text
        Raylib.DrawText("Press ENTER to start", 350, 450, 34, Color.BLACK);
        Raylib.DrawText("Use WASD or arrows to move", 350, 550, 34, Color.BLACK);
        Raylib.DrawText("Avoid cars and water", 350, 650, 34, Color.BLACK);


    }

    else if (currentScene == "end")
    {
        Raylib.ClearBackground(Color.RED); //röd bakgrund 
        Raylib.DrawText("GAME OVER", 430, 350, 36, Color.BLACK);//text
        Raylib.DrawText("Press ENTER to play again", 320, 450, 34, Color.BLACK);

    }
    else if (currentScene == "win")
    {
        Raylib.ClearBackground(Color.GREEN);//grön backgrund
        Raylib.DrawText("YOU WON!", 430, 350, 36, Color.BLACK);//text
        Raylib.DrawText("Press ENTER to go to start screen", 320, 450, 34, Color.BLACK);

    }
}

static void frogMove(Rectangle player, int jump, int degrees, Rectangle frogCollider, string currentScene, float timerCurrentValue)
{
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
            degrees = 90;     //kolla höger
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
        if (player.x >= 1000)
        {
            player.x -= jump;
        }
        if (player.x <= 1)
        {
            player.x += jump;
        }


        frogCollider = player;
        frogCollider.x -= player.width / 2; //flyttar på frogens htibox
        frogCollider.y -= player.height / 2;

        if (frogCollider.y <= 1)
        {
            currentScene = "win";
        }

    }
}
static void collisions(List<Obstacle> cars, List<Water> waters, Rectangle frogCollider, string currentScene)
{
    foreach (Obstacle c in cars) //execute update metoden för (c) item
        {
            c.Update();

            if (Raylib.CheckCollisionRecs(frogCollider, c.carCollider)) //när frog kolliderar med bilen 
            {
                currentScene = "end";    //game over
            }
        }

        foreach (Water w in waters)
        {
            if (Raylib.CheckCollisionRecs(frogCollider, w.rectWater)) //när frog kolliderar med bilen
            {
                currentScene = "end";     //game over
            }
        }
} //jag vet att det funkar inte riktigt som den ska, jag kommer på måndag o fixar det.