using System;

public class Obstacle
{
    Texture2D carSprite = Raylib.LoadTexture("car-1.png.png");//laddar upp car sprite
    
    int carSpeed = 10;   //hastigheten för bilen

    Rectangle rectRight;      //rektangel för carRight
    Rectangle rectLeft;       //rektangel för carLeft

    Random generator = new Random(); //random bestämmer om bilen ska komma från vänster eller höger.
    int o;      //variablen för random bil position.

    public Obstacle (int carY)  //anger int värde för obstacle klassen, alltså var bilen ska vara (X, Y position)
    {

         o = generator.Next(1, 3);   //slumpar ett tal som är 1, eller 2

        if (o == 1) //ifall o (obstacle) är 1 då är det rectRight
        {
        rectRight = new Rectangle(1000, carY, carSprite.width, carSprite.height);  //anger vart rect bilen ska stå och storleken på den
        }

        else if (o == 2)
        {
        rectLeft = new Rectangle(1, carY + 60, carSprite.width, carSprite.height); //kollar vänster. carY +60 behövs för att rotationen skulle vara i mitten av spriten.
        }
        
    }
    public void Update()
    {
        rectRight.x -= carSpeed;
        rectLeft.x += carSpeed;

        if (rectRight.x < -100 )
        {
         rectRight.x = 1000;
        }
        if (rectLeft.x > 1000)
        {
            rectLeft.x = 1;
        }
        
    }
    public void Draw()
    {
        if (o == 1)
        {
        Raylib.DrawTexture(carSprite, (int)rectRight.x, (int)rectRight.y, Color.WHITE);//carRight

        }
        else if (o == 2)
        {
        Raylib.DrawTexturePro(carSprite, new Rectangle(0,0,120,120), rectLeft, new Vector2(50, 50), 180, Color.WHITE);//carLeft

        }
    }
}

