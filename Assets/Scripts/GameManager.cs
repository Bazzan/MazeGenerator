using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public Maze MazePrefab;

    private Maze mazeInstance;


    void Start()
    {
        InitializeGame();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            RestartGame();
        }
    }

    private void InitializeGame()
    {
        mazeInstance = Instantiate(MazePrefab) as Maze;
        StartCoroutine(mazeInstance.Generate());
    }

    private void RestartGame()
    {
        StopAllCoroutines();
        Destroy(mazeInstance.gameObject);
        InitializeGame();
    }
}
