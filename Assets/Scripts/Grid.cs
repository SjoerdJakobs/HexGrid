using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{

    public int width = 6;
    public int height = 6;
    public float spacing = 0;

    [SerializeField]
    private bool multipleObjects;

    [SerializeField]
    private Material matt;

    private GameObject[,] hexagonArray;

    public List<Vector3> points;

    void Awake()
    {
        points = new List<Vector3>();
        hexagonArray = new GameObject[width,height];
        createGrid();
    }

    void Start()
    {
        //Invoke("DestroyGrid", 5);
        //Invoke("createGrid", 8);
    }

    public void createGrid()
    {
        DestroyGrid();
        for (int z = 0, i = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                i++;
                if (multipleObjects)
                {
                    AddHexagon(x, z);
                }
                else
                {
                    createPoints(x, z, i);
                }
            }
        }
        for (int i = 0; i < points.Count; i++)
        {
            //print(points[i] + "point" + i);
        }
    }

    public void DestroyGrid()
    {
        for (int z = 0; z < height; z++)
        {
            for (int x = 0; x < width; x++)
            {
                Destroy(hexagonArray[x,z]);
            }
        }
    }

    void createPoints(int x, int z, int o)
    {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * ((Hexagon.innerRadius + spacing) * 2f);
        position.y = 0f;
        position.z = z * ((Hexagon.radius + spacing) * 1.5f);
        Vector3[] tempArray = Hexagon.Corners.singleCorners;
        for(int j = 0; j < tempArray.Length; j++)
        {
            tempArray[j] += position;
            points.Add(tempArray[j]);
            print(points.Count);
        }
    }

    void AddHexagon(int x, int z)
    {
        GameObject hexagon = new GameObject("Hexagon");
        hexagon.AddComponent<MeshRenderer>();
        Renderer rendr = hexagon.GetComponent<Renderer>();
        rendr.material = matt;
        rendr.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        hexagon.AddComponent<Hexagon>();

        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * ((Hexagon.innerRadius+spacing) * 2f);
        position.y = 0f;
        position.z = z * ((Hexagon.radius+spacing) * 1.5f);

        hexagon.transform.position = position;

        hexagonArray[x,z] = hexagon;       
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.black;
        for (int i = 0; i < points.Count; i++)
        {
            Gizmos.DrawSphere(points[i], 0.1f);
        }
    }
}