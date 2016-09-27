using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Hexagon : MonoBehaviour
{
    /// <summary>
    /// here we generate a hexagon together with its variables.
    /// </summary>
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3 transf;

    public const float radius = 10f;

    public const float innerRadius = radius * 0.866025404f;


    void Awake()
    {
        transf = transform.position;
        Generate();
    }
        

    
    public void Generate()
    {
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "hexagon";
        Vector3[] positionCorners = Corners.corners;
        for(int i = 0; i < positionCorners.Length; i++)
        {
            positionCorners[i] += transf;
        }

        mesh.vertices = positionCorners;

        int[] triangles = new int[12];
        triangles[0] = 1;
        triangles[1] = 2;
        triangles[2] = 5;

        triangles[3] = 2;
        triangles[4] = 4;
        triangles[5] = 5;

        triangles[6] = 0;
        triangles[7] = 1;
        triangles[8] = 5;

        triangles[9] = 2;
        triangles[10] = 3;
        triangles[11] = 4;

        mesh.triangles = triangles;
    }
    private void OnDrawGizmos()
    {
        if (vertices == null)
        {
            return;
        }
        Gizmos.color = Color.black;
        for (int i = 0; i < Corners.corners.Length; i++)
        {
            Gizmos.DrawSphere(Corners.corners[i], 0.1f);
        }
    }
    public static class Corners
    {
        public static Vector3[] corners = {
        new Vector3(0f, 0f, radius),
        new Vector3(innerRadius, 0f, 0.5f * radius),
        new Vector3(innerRadius, 0f, -0.5f * radius),
        new Vector3(0f, 0f, -radius),
        new Vector3(-innerRadius, 0f, -0.5f * radius),
        new Vector3(-innerRadius, 0f, 0.5f * radius),
        };
    }
}