using UnityEngine;


[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Hexagon : MonoBehaviour
{
    /// <summary>
    /// here we generate a hexagon together with its variables or even only its variables to give to another script.
    /// </summary>
    private Mesh mesh;
    private Vector3[] vertices;
    private Vector3 transf;
    
    public static float radius = 10f;

    public static float innerRadius = radius * 0.866025404f;


    void Awake()
    {
        transf = transform.position;
        GenerateSingleHex();
    }
        
    public void GenerateSingleHex()
    {
        //here i hard code a hexagon with wals under it because it was faster and easier
        //to do for me that some sort of loop. 

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "hexagon";
        Vector3[] positionCorners = Corners.multipleCorners;
        for(int i = 0; i < positionCorners.Length; i++)
        {
            positionCorners[i] += transf;
        }

        mesh.vertices = positionCorners;


        int[] triangles = new int[30];
        //right top middle triangele
        triangles[0] = 1;
        triangles[1] = 2;
        triangles[2] = 5;

        //left top middle triangle
        triangles[3] = 2;
        triangles[4] = 4;
        triangles[5] = 5;

        //right top triangle
        triangles[6] = 0;
        triangles[7] = 1;
        triangles[8] = 5;

        //left top triangle
        triangles[9] = 2;
        triangles[10] = 3;
        triangles[11] = 4;


        //wals
        triangles[12] = 6;
        triangles[13] = 0;
        triangles[14] = 5;

        triangles[15] = 11;
        triangles[16] = 6;
        triangles[17] = 5;

        triangles[18] = 6;
        triangles[19] = 7;
        triangles[20] = 0;

        triangles[21] = 7;
        triangles[22] = 1;
        triangles[23] = 0;

        triangles[24] = 8;
        triangles[25] = 2;
        triangles[26] = 1;

        triangles[27] = 7;
        triangles[28] = 8;
        triangles[29] = 1;

        mesh.triangles = triangles;
    }

    public struct Corners
    {
        /// <summary>
        /// here i set the position of the vertices of 2 kinds of hexagons. 
        /// the single object hexagon and the multiple objects hexagon.
        /// while i know that this gives it more responsebility than it should have it wil do for now.
        /// with the multipleCorners i give the vertices for a hex grid where every hex is its own object
        /// while the singleCorners is to just give a hex form with vertices to a script that could
        /// create its own hexgrid with just one mesh.
        /// </summary>
        public static Vector3[] multipleCorners = {
            //top level
        new Vector3(0f, 10f, radius),//corner 0, far top corner
        new Vector3(innerRadius, 10f, 0.5f * radius),//corner 1,top right corner 
        new Vector3(innerRadius, 10f, -0.5f * radius),//corner 2, down right corner
        new Vector3(0f, 10f, -radius),//corner 3, fardown corner
        new Vector3(-innerRadius, 10f, -0.5f * radius),//corner 4, down left corner
        new Vector3(-innerRadius, 10f, 0.5f * radius),//corner 5,top left corner 
            
            //lower level
        new Vector3(0f, 0f, radius),//corner 6, far top corner
        new Vector3(innerRadius, 0f, 0.5f * radius),//corner 7,top right corner
        new Vector3(innerRadius, 0f, -0.5f * radius),//corner 8, down right corner
        new Vector3(0f, 0f, -radius),//corner 9, fardown corner
        new Vector3(-innerRadius, 0f, -0.5f * radius),//corner 10, down left corner
        new Vector3(-innerRadius, 0f, 0.5f * radius),//corner 11,top left corner 
        };

        public static Vector3[] singleCorners = {
        new Vector3(0f, 10f, radius),//corner 0, far top corner
        new Vector3(innerRadius, 10f, 0.5f * radius),//corner 1,top right corner 
        new Vector3(innerRadius, 10f, -0.5f * radius),//corner 2, down right corner
        new Vector3(0f, 10f, -radius),//corner 3, fardown corner
        new Vector3(-innerRadius, 10f, -0.5f * radius),//corner 4, down left corner
        new Vector3(-innerRadius, 10f, 0.5f * radius),//corner 5,top left corner 
        };
    }

    private void OnDrawGizmos()
    {

        Gizmos.color = Color.black;
        for (int i = 0; i < Corners.multipleCorners.Length; i++)
        {
            Gizmos.DrawSphere(Corners.multipleCorners[i], 0.1f);
        }
    }
}