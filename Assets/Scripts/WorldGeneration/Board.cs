public class Board
{
    private int _width;
    private int _height;
    private Tile[,] _tiles;

    public Board(int width, int height)
    {
        this._width = width;
        this._height = height;

        _tiles = new Tile[width, height];
    }

    public void SetTile(int posX, int row, Tile tile)
    {
        _tiles[posX, row] = tile;
    }

    public Tile GetTile(int column, int row)
    {
        return _tiles[column, row];
    }

    public int GetWidth
    {
        get { return _width; }
    }

    public int GetHeight
    {
        get { return _height; }
    }
}
