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

    public void Generate(Vector2 lBot, Vector2 lTop, Vector2 rTop, Vector2 rBot)
    {
        var mesh = new Mesh();

        var lMidBot = new Vector2(lTop.x, lBot.y);
        var rMidBot = new Vector2(rTop.x, rBot.y);
        
        var vertices = new Vector3[6];

        var localLTop = transform.InverseTransformPoint(lTop);
        var localLBot = transform.InverseTransformPoint(lBot);
        var localRTop = transform.InverseTransformPoint(rTop);
        var localRBot = transform.InverseTransformPoint(rBot);

        var localLMidBot = transform.InverseTransformPoint(lMidBot);
        var localRMidBot = transform.InverseTransformPoint(rMidBot);
        
        //Left Top
        vertices[0] = new Vector3(localLTop.x, localLTop.y, z);
        
        //Left Bottom
        vertices[1] = new Vector3(localLBot.x, localLBot.y, z);

        //mid left
        vertices[2] = new Vector3(localLMidBot.x, localLMidBot.y, z);
        
        //mid right
        vertices[3] = new Vector3(localRMidBot.x, localRMidBot.y, z);
        
        //Right Bottom
        vertices[4] = new Vector3(localRBot.x, localRBot.y, z);
        
        //Right Top
        vertices[5] = new Vector3(localRTop.x, localRTop.y, z);

        mesh.vertices = vertices;
        mesh.triangles = new int[] {2, 1, 0, 0, 5, 2, 5, 3, 2, 3, 5, 4};
        
        var uvs = new Vector2[vertices.Length];
        for (var i = 0; i < uvs.Length; i++)
        {
            var uv = uvs[i];
            uv = vertices[i];
        }

        mesh.uv = uvs;

        _renderer.material = mat;
        _filter.mesh = mesh;
        
    }
}