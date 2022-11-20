using UnityEngine;

public class Board
{
    private int posX;
    private int posY;
    private int[,] boardArray;

    public Board(int posX, int posY)
    {
        this.posX = posX;
        this.posY = posY;

        boardArray = new int[posX, posY];
    }

    public int GetPosX
    {
        get { return posX; }
    }

    public int GetPosY
    {
        get { return posY; }
    }
}
