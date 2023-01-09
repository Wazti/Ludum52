using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGenerator : MonoBehaviour {

    public Material mat;
    
    [SerializeField] private float z = 0; //v
    
    float width = 1;
    float height = 1;

    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] private MeshFilter _filter;

    public void Generate(Vector2 lBot, Vector2 lTop, Vector2 rTop, Vector2 rBot, Vector2 lMid, Vector2 rMid)
    {
        var mesh = new Mesh();

        var vertices = new Vector3[4];

        var localLTop = transform.InverseTransformPoint(lTop);
        var localLBot = transform.InverseTransformPoint(lBot);
        var localRTop = transform.InverseTransformPoint(rTop);
        var localRBot = transform.InverseTransformPoint(rBot);

        var localLMidBot = transform.InverseTransformPoint(lMid);
        var localRMidBot = transform.InverseTransformPoint(rMid);
        
        //Left Top
        vertices[0] = new Vector3(localLTop.x, localLTop.y, z);
        
        //Left Bottom
        vertices[1] = new Vector3(localLBot.x, localLBot.y, z);

        /*//mid left
        vertices[2] = new Vector3(localLMidBot.x, localLMidBot.y, z);*/
        
        /*//mid right
        vertices[3] = new Vector3(localRMidBot.x, localRMidBot.y, z);*/
        
        //Right Bottom
        vertices[2] = new Vector3(localRBot.x, localRBot.y, z);
        
        //Right Top
        vertices[3] = new Vector3(localRTop.x, localRTop.y, z);

        mesh.vertices = vertices;
        mesh.triangles = new[] {0, 2, 1, 0, 3, 2};
        mesh.uv = new[]
        {
            new Vector2(0, 0),
            new Vector2(1, 0),
            new Vector2(1, 1),
            new Vector2(0, 1)
        };

        _renderer.material = mat;
        _filter.mesh = mesh;
        
    }
}