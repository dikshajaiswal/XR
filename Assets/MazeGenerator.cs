using UnityEngine;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json; // Import Newtonsoft.Json

public class MazeGenerator : MonoBehaviour
{
    public GameObject wallPrefab;  // Prefab for walls
    public float cellSize = 1.0f;  // Size of each grid cell

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        string jsonPath = Path.Combine(Application.dataPath, "Maze.json");
        if (!File.Exists(jsonPath))
        {
            Debug.LogError("Maze JSON file not found!");
            return;
        }

        string jsonData = File.ReadAllText(jsonPath);
        Debug.Log("Raw JSON Data: " + jsonData);

        try
        {
            // Use Newtonsoft.Json for parsing
            MazeData mazeData = JsonConvert.DeserializeObject<MazeData>(jsonData);

            if (mazeData == null || mazeData.grid == null || mazeData.grid.Count == 0)
            {
                Debug.LogError("Failed to parse maze data or grid is empty!");
                return;
            }

            // Generate maze walls
            for (int row = 0; row < mazeData.grid.Count; row++)
            {
                for (int col = 0; col < mazeData.grid[row].Count; col++)
                {
                    if (mazeData.grid[row][col] == 1) // Create a wall
                    {
                        Vector3 position = new Vector3(col * cellSize, 0, row * cellSize);
                        Instantiate(wallPrefab, position, Quaternion.identity, transform);
                    }
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("JSON Parsing Error: " + e.Message);
        }
    }
}

[System.Serializable]
public class MazeData
{
    public List<List<int>> grid;
}
