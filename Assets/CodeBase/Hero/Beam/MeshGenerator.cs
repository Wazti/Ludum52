using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {

    public Material mat;
    
    private const float z = 0; //v
    
    float width = 1;
    float height = 1;

    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private MeshFilter _filter;

    public void Generate(Vector2 lBot, Vector2 lTop, Vector2 rTop, Vector2 rBot)
    {
        var mesh = new Mesh();

        var lMidBot = new Vector2(lTop.x, lBot.y);
        var rMidBot = new Vector2(rTop.x, rBot.y);
        
        var vertices = new Vector3[6];
        
        /*vertices[0] = new Vector3(-width, -height);
        vertices[1] = new Vector3(-width, height);
        vertices[2] = new Vector3(width, height);
        vertices[3] = new Vector3(width, -height);*/
        
        /*//Left Bottom
        vertices[0] = new Vector3(lBot.x, lBot.y, z);
        
        //Left Top
        vertices[1] = new Vector3(lTop.x, lTop.y, z);
        
        //Right Top
        vertices[2] = new Vector3(rTop.x, rTop.y, z);
        
        //Right Bottom
        vertices[3] = new Vector3(rBot.x, rBot.y, z);*/
        
        
        //Left Top
        vertices[0] = new Vector3(lTop.x, lTop.y, z);
        
        //Left Bottom
        vertices[1] = new Vector3(lBot.x, lBot.y, z);

        //mid left
        vertices[2] = new Vector3(lMidBot.x, lMidBot.y, z);
        
        //mid right
        vertices[3] = new Vector3(rMidBot.x, rMidBot.y, z);
        
        //Right Bottom
        vertices[4] = new Vector3(rBot.x, rBot.y, z);
        
        //Right Top
        vertices[5] = new Vector3(rTop.x, rTop.y, z);

        mesh.vertices = vertices;
        mesh.triangles = new int[] {2, 1, 0, 0, 5, 2, 5, 3, 2, 3, 5, 4};
        
        _renderer.material = mat;
        _filter.mesh = mesh;
    }
}