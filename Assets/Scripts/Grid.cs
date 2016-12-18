using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
    /// <summary>
    /// was not sure if i would need some of the value's so i kept them public. but when im sure i just change them to private.
    /// </summary>

    public GameObject cameraObj;

    public int width = 6;
    public int height = 6;
    public float scale = 15;
    public float spacing = 0;

    public Slider sizeSlider;
    public Slider scaleSlider;
    public Slider spaceSlider;

    public Text sizeText;
    public Text scaleText;
    public Text spacingText;

    private int oldWidth;
    private int oldHeight;

    [SerializeField]
    private bool multipleObjects;

    [SerializeField]
    private Material matt;

    private GameObject[,] hexagonArray;

    public List<Vector3> points;

    
    void Awake()
    {
        width = (int)sizeSlider.value;
        height = (int)sizeSlider.value;
        scale = scaleSlider.value;
        spacing = spaceSlider.value;
        points = new List<Vector3>();
        hexagonArray = new GameObject[200,200];
        CreateGrid();
    }
    /// <summary>
    /// all the ui stuff should have a class of its own
    /// </summary>
    void Update()
    {
        width = (int)sizeSlider.value;
        height = (int)sizeSlider.value;
        scale = scaleSlider.value;
        spacing = spaceSlider.value;

        sizeText.text = ""+width;
        scaleText.text = "" + scale;
        spacingText.text = "" + spacing;
    }

    /// <summary>
    /// this function calls the destroy grid funtion to make sure there is only one grid.
    /// then the grid gets created. Addhexagon is for the grid you will see in the build
    /// and CreatePoints is for a single mesh grid option in the future.(wip)
    /// </summary>
    public void CreateGrid()
    {
        DestroyGrid();
        oldHeight = height;
        oldWidth = width;
        for (int z = 0, i = 0; z < oldHeight; z++)
        {
            for (int x = 0; x < oldWidth; x++)
            {
                i++;
                if (multipleObjects)
                {
                    AddHexagon(x, z);
                }
                else
                {
                    CreatePoints(x, z, i);
                }
            }
        }
        for (int i = 0; i < points.Count; i++)
        {
            //print(points[i] + "point" + i);
        }
        cameraObj.gameObject.transform.position = new Vector3(hexagonArray[oldWidth/2,oldHeight/2].transform.position.x, oldWidth * 1.6f, (hexagonArray[oldWidth/2,oldHeight/2].transform.position.z));
    }

    /// <summary>
    /// this function destroys the grid.
    /// all the objects in the grid are put in a list. here we just take everything int the list and estroy it.
    /// </summary>
    public void DestroyGrid()
    {
        for (int z = 0; z < oldHeight; z++)
        {
            for (int x = 0; x < oldWidth; x++)
            {
                Destroy(hexagonArray[x,z]);
            }
        }
    }

    /// <summary>
    /// here i have a get the points of one hexagon and make a whole grid of hexagon vertices.
    /// 
    /// wip
    /// </summary>
    void CreatePoints(int x, int z, int o)
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

    /// <summary>
    /// here i spawn the hexagons in the right place and the right ammount.
    /// also i change the color and height here. something that should need its own classen but i havent made those yet.
    /// </summary>
    void AddHexagon(int x, int z)
    {
        GameObject hexagon = new GameObject("Hexagon");
        hexagon.AddComponent<MeshRenderer>();
        Renderer rendr = hexagon.GetComponent<Renderer>();
        rendr.material = matt;

        float sampleX = (x) / scale;
        float sampleZ = (z) / scale;

        //you should make classes for the color and height changes.
        float noise = Mathf.PerlinNoise(sampleX, sampleZ) * 2 - 1;

        if (noise < -0.3f)
        {
            rendr.material.color = Color.blue;
        }
        else if (noise < 0f)
        {
            rendr.material.color = new Color(1, 0.965f, 0.667f);
        }
        else
        {
            rendr.material.color = Color.green;
        }
        

        if (noise < 0)
        {
            noise = 0;
        }
        //print(noise);

        hexagon.transform.localScale += new Vector3(0, (noise * 5 * (scale/ 10))/10, 0);

        //rendr.material.color = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        
        hexagon.AddComponent<Hexagon>();

        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * ((Hexagon.innerRadius+spacing) * 2f);
        position.y = 0f;
        position.z = z * ((Hexagon.radius+spacing) * 1.5f);

        hexagon.transform.position = position;

        hexagonArray[x,z] = hexagon;

        hexagon.isStatic = true;
    }

    /*private void OnDrawGizmos()
    {

        Gizmos.color = Color.black;
        for (int i = 0; i < points.Count; i++)
        {
            Gizmos.DrawSphere(points[i], 0.1f);
        }
    }*/
}