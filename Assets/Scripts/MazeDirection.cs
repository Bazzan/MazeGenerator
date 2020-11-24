using UnityEngine;


public enum MazeDirection
{
    North,
    East,
    South,
    West
}
public static class MazeDirections
{
    public const int Count = 4;

    private static Vector2Int[] vectors = {
        new Vector2Int(0, 1),
        new Vector2Int(1, 0),
        new Vector2Int(0, -1),
        new Vector2Int(-1, 0)
    };

    private static MazeDirection[] opposites = {
        MazeDirection.South,
        MazeDirection.West,
        MazeDirection.North,
        MazeDirection.East
    };

    private static Quaternion[] rotations = {
        Quaternion.identity,
        Quaternion.Euler(0f, 90f, 0f),
        Quaternion.Euler(0f, 180f, 0f),
        Quaternion.Euler(0f, 270f, 0f)
    };

    

    public static MazeDirection RandomValue
    {
        get
        {
            return (MazeDirection)Random.Range(0, Count);
        }
    }
    public static Vector2Int ToVector2Int(this MazeDirection direction)
    {
        return vectors[(int)direction];
    }
    public static MazeDirection GetOpposite(this MazeDirection direction)
    {
        return opposites[(int)direction];
    }
    public static Quaternion ToRotation(this MazeDirection direction)
    {
        return rotations[(int)direction];
    }
}
