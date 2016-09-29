using UnityEngine;

public class Grid : MonoBehaviour
{

    public int width = 6;
    public int height = 6;

    [SerializeField]
    private Material matt;

    private GameObject[] hexagonArray;

    void Awake()
    {
        hexagonArray = new GameObject[width*height];
        createGrid();
    }

    /*void Start()
    {
        Invoke("DestroyGrid", 5);
        Invoke("createGrid", 8);
    }*/

    void createGrid()
    {
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                AddHexagon(x, z, i++);
            }
        }
    }

    void DestroyGrid()
    {
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Destroy(hexagonArray[i]);
                i++;
            }
        }
    }

    void AddHexagon(int x, int z, int i)
    {
        GameObject hexagon = new GameObject("Hexagon");
        hexagon.AddComponent<MeshRenderer>();
        Renderer rendr = hexagon.GetComponent<Renderer>();
        rendr.material = matt;
        rendr.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        hexagon.AddComponent<Hexagon>();
        Hexagon hexagonInfo = hexagon.GetComponent<Hexagon>();

        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        hexagon.transform.position = position;

        hexagonArray[i] = hexagon;       
    }
}