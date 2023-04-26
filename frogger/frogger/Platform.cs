using System;


public class Platform //class för platformen
{
    Texture2D platformSprite = Raylib.LoadTexture("log-1.png.png");//sprite för platform

    public Rectangle rectPlatform; //rect för plaform

    public Rectangle platformCollider; //paltform collider
    
    public Platform(int platformX, int platformY) //x och y värde för platform
    {
        rectPlatform = new Rectangle(platformX, platformY, platformSprite.width, platformSprite.height); //säger vart platform rectangle ska va

        platformCollider.width = rectPlatform.width;
        platformCollider.height = rectPlatform.height;

    }

    public void DrawPlatform()   //rita platform
    {
        Raylib.DrawTexture(platformSprite, (int)rectPlatform.x, (int)rectPlatform.y, Color.WHITE); //ritar ut platform rectangle
        //Raylib.DrawRectangle((int)rectPlatform.x, (int)rectPlatform.y, (int)rectPlatform.width, (int)rectPlatform.height, Color.BROWN);
        
    }
    
}

public class Water //class för vatten
{
    public Rectangle rectWater; //rect vatten
    public Water(int waterX, int waterY, int waterWidth) //skriver in int för vattens width
    {
        rectWater = new Rectangle(waterX, waterY, waterWidth, 200); // säger vart rektangel för vatten ska vara.
    }
}
