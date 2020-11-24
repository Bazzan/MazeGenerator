using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze : MonoBehaviour
{

    public MazePassage passagePrefab;
    public MazeWall wallPrefab;
    public Vector2Int size;
    public MazeCell cellPrefab;
    public float seconds;
    private MazeCell[,] cells;


    public MazeCell GetCell(Vector2Int position)
    {
        return cells[position.x, position.y];
    }

    public IEnumerator Generate()
    {
        cells = new MazeCell[size.x, size.y];
        WaitForSeconds waitForSeconds = new WaitForSeconds(seconds);

        List<MazeCell> activeCells = new List<MazeCell>();
        FirstGenerationStep(activeCells);
        Vector2Int coordinates = RandomCoordinates;
        while (activeCells.Count > 0)
        {
            yield return waitForSeconds;
            DoNextGenerationStep(activeCells);
        }
    }

    private void FirstGenerationStep(List<MazeCell> activeCells)
    {
        activeCells.Add(CreateCell(RandomCoordinates));
    }
    private void DoNextGenerationStep(List<MazeCell> activeCells)
    {
        int currentIndex = activeCells.Count - 1;
        MazeCell currentCell = activeCells[currentIndex];

        if (currentCell.IsFullyInitialized)
        {
            activeCells.RemoveAt(currentIndex);
            return;
        }

        MazeDirection direction = currentCell.RandomUninitializedDirection;
        Vector2Int coordinates = currentCell.position + direction.ToVector2Int();

        if (ContainsCoordinates(coordinates))
        {
            MazeCell neighbor = GetCell(coordinates);
            if (neighbor == null)
            {
                neighbor = CreateCell(coordinates);
                CreatePassage(currentCell, neighbor, direction);
                activeCells.Add(neighbor);
            }
            else
            {
                CreateWall(currentCell, neighbor, direction);
            }
        }
        else
        {
            CreateWall(currentCell, null, direction);
        }
    }

    public Vector2Int RandomCoordinates
    {
        get
        {
            return new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));
        }
    }

    public bool ContainsCoordinates(Vector2Int coordinate)
    {
        return coordinate.x >= 0 && coordinate.x < size.x && coordinate.y >= 0 && coordinate.y < size.y;
    }


    private void CreateWall(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazeWall wall = Instantiate(wallPrefab) as MazeWall;
        wall.Initialize(cell, otherCell, direction);
        if (otherCell != null)
        {
            wall = Instantiate(wallPrefab) as MazeWall;
            wall.Initialize(otherCell, cell, direction.GetOpposite());
        }
    }
    private void CreatePassage(MazeCell cell, MazeCell otherCell, MazeDirection direction)
    {
        MazePassage passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(cell, otherCell, direction);
        passage = Instantiate(passagePrefab) as MazePassage;
        passage.Initialize(otherCell, cell, direction.GetOpposite());
    }
    private MazeCell CreateCell(Vector2Int position)
    {
        MazeCell newCell = Instantiate(cellPrefab) as MazeCell;
        cells[position.x, position.y] = newCell;
        newCell.position = position;
        newCell.name = $"Maze cell {position.x},{position.y} ";
        newCell.transform.parent = transform;
        newCell.transform.localPosition = new Vector3(position.x - size.x * 0.5f + 0.5f,
            0f,
            position.y - size.y * 0.5f + 0.5f);
        return newCell;
    }

}

