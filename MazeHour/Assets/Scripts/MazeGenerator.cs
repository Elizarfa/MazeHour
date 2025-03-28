using UnityEngine;
using Random = UnityEngine.Random;

public class MazeGenerator : MonoBehaviour
{
    public int width = 15; // Размер лабиринта по ширине
    public int height = 10; // Размер лабиринта по высоте
    public GameObject wallPrefab; // Префаб стены
    public float cellSize = 1f; // Размер одной ячейки лабиринта

    void Start()
    {
        GenerateMaze();
    }

    void GenerateMaze()
    {
        // Создаем пустой контейнер для стен
        GameObject wallsContainer = new GameObject("Walls");
        wallsContainer.transform.parent = transform;

        // Создаем сплошную сетку стен
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                Vector3 position = new Vector3(x * cellSize, y * cellSize, 0f);
                Instantiate(wallPrefab, position, Quaternion.identity, wallsContainer.transform);
            }
        }

        // Случайным образом удаляем некоторые стены (очень простой подход)
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                if (Random.value < 0.3f && (x > 0 || y > 0 || x < width - 1 || y < height - 1)) // Увеличиваем вероятность прохода, исключая крайние стены
                {
                    Collider2D[] colliders = Physics2D.OverlapCircleAll(new Vector2(x * cellSize, y * cellSize), 0.1f);
                    foreach (var collider in colliders)
                    {
                        Destroy(collider.gameObject);
                    }
                }
            }
        }
    }
}