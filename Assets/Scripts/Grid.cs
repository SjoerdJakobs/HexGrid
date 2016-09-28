using UnityEngine;
using UnityEngine.UI;

public class Grid : MonoBehaviour
{

    public int width = 6;
    public int height = 6;

    void Awake()
    {
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                AddHexagon(x, z, i++);
            }
        }
    }

    void AddHexagon(int x, int z, int i)
    {
        GameObject hexagon = new GameObject("Hexagon");
        hexagon.AddComponent<Hexagon>();
        Hexagon hexagonInfo = hexagon.GetComponent<Hexagon>();
        
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        hexagon.transform.position = position;
    }
}