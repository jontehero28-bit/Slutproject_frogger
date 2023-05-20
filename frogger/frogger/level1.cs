global using System;

public static class level1
{

    public static void DrawLevel1() //gjorde denna klassen för att spara lite på rader kod och flytta kod upprepelse hittåt
    {
        Raylib.ClearBackground(Color.GRAY);
        Raylib.DrawRectangle(1, 900, 1000, 100, Color.GREEN); //grässmatta, spawn punkt
        Raylib.DrawRectangle(1, 600, 1000, 300, Color.BLACK); //bilväg
        Raylib.DrawRectangle(1, 500, 1000, 100, Color.GREEN); //safezone
        Raylib.DrawRectangle(1, 300, 1000, 200, Color.BLUE); //water
        Raylib.DrawRectangle(1, 100, 1000, 200, Color.BLACK);//bilväg
        Raylib.DrawRectangle(1, 1, 1000, 100, Color.GREEN);//safezone

        

       
    }
    
}
