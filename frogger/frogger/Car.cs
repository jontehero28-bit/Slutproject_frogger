using System;

public class Obstacle
{
    Texture2D carSprite = Raylib.LoadTexture("car-1.png.png");//laddar upp car sprite
    
    int carSpeed = 10;   //hastigheten för bilen

    Rectangle rectCar;      //rektangel för car

    Random generator = new Random(); //random bestämmer om bilen ska komma från vänster eller höger.
    int o;      //variablen för random bil position.

    public Obstacle (int carY)  //anger int värde för obstacle klassen, alltså var bilen ska vara (X, Y position)
    {

         o = generator.Next(1, 3);   //slumpar ett tal som är 1, eller 2

        if (o == 1) //ifall o (obstacle) är 1 då är det rectRight
        {
        rectCar = new Rectangle(1000, carY, carSprite.width, carSprite.height);  //anger vart rect bilen ska stå och storleken på den
        }

        else if (o == 2)   //ifall obstacle är 2 då är det rectLeft
        {
        rectCar = new Rectangle(1, carY + 60, carSprite.width, carSprite.height); //kollar vänster. carY +60 behövs för att rotationen skulle vara i mitten av spriten.
        }
        
    }
    public void Update()
    {
       
       
        
        if (o == 1) //ifall o = 1 då bilen kommer från högra sidan
        {
        if (rectCar.x < -100 )//när den når gränsen av skärmen den teleporteras tillbaka.
        {
         rectCar.x = 1000;
        }
         rectCar.x -= carSpeed;//rörelse för rectRight

        }

        else if (o == 2)
        {
        if (rectCar.x > 1100)
        {
            rectCar.x = 1;
            
        }
         rectCar.x += carSpeed;//rörelse för rectLeft
        }
        
    }
    public void Draw()//rita ut bilar.
    {
        if (o == 1)   //ifall o = 1 då rita ut carRight
        {
        Raylib.DrawTexture(carSprite, (int)rectCar.x, (int)rectCar.y, Color.WHITE);//carRight

        }
        else if (o == 2)    // ifall o = 2 då rita ut carLeft
        {
        Raylib.DrawTexturePro(carSprite, new Rectangle(0,0,120,120), rectCar, new Vector2(50, 50), 180, Color.WHITE);//carLeft

        }
    }
}

