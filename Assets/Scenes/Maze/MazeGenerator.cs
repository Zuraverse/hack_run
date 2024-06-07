using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    public int width, height;
    public Material brick;
    private int[,] Maze;
    private Stack<Vector2> _tiletoTry = new Stack<Vector2>();
    private List<Vector2> offsets = new List<Vector2>
    {
        new Vector2(0, 1),
        new Vector2(0, -1),
        new Vector2(1, 0),
        new Vector2(-1, 0)
    };
    private System.Random rnd = new System.Random();
    private int _width, _height;
    private Vector2 _currentTile;
    public static MazeGenerator Instance;
    public GameObject Wall;
    public float WallLength = 1.0f;
    public Transform WallHolder;

    void Start()
    {
        Instance = this;
        Maze = new int[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Maze[x, y] = 1;
            }
        }
        _currentTile = Vector2.zero;
        _tiletoTry.Push(_currentTile);
        CreateMaze();
        MakeBlocks();
    }

    void CreateMaze()
    {
        while (_tiletoTry.Count > 0)
        {
            _currentTile = _tiletoTry.Pop();
            var validDirections = ValidDirections();
            if (validDirections.Count > 0)
            {
                _tiletoTry.Push(_currentTile);
                var nextDirection = validDirections[rnd.Next(validDirections.Count)];
                var newTile = new Vector2(_currentTile.x + nextDirection.x, _currentTile.y + nextDirection.y);
                if (Maze[(int)newTile.x, (int)newTile.y] == 1)
                {
                    Maze[(int)newTile.x, (int)newTile.y] = 0;
                    Maze[(int)(_currentTile.x + nextDirection.x / 2), (int)(_currentTile.y + nextDirection.y / 2)] = 0;
                }
                _tiletoTry.Push(newTile);
            }
        }
    }

    List<Vector2> ValidDirections()
    {
        List<Vector2> validDirections = new List<Vector2>();
        foreach (var offset in offsets)
        {
            var newX = (int)_currentTile.x + (int)offset.x * 2;
            var newY = (int)_currentTile.y + (int)offset.y * 2;
            if (newX >= 0 && newY >= 0 && newX < width && newY < height)
            {
                if (Maze[newX, newY] == 1)
                {
                    validDirections.Add(offset);
                }
            }
        }
        return validDirections;
    }

    void MakeBlocks()
    {
        WallHolder = new GameObject().transform;
        WallHolder.name = "Maze";
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (Maze[i, j] == 1)
                {
                    var cube = Instantiate(Wall, new Vector3(i * WallLength - width / 2f + WallLength / 2f, WallLength / 2f, j * WallLength - height / 2f + WallLength / 2f), Quaternion.identity) as GameObject;
                    cube.transform.parent = WallHolder.transform;
                }
            }
        }
    }
}
