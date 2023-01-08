using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCast : MonoBehaviour
{
    public Vector2 Cast(float angle, Vector2 startPos, LayerMask mask, Color color)
    {
        var result = Vector2.zero;
        var direction = GetDirectionVector2D(angle);
        var range = Mathf.Infinity;
        
        var ray = Physics2D.Raycast(startPos, direction, range, mask);
        Color col = new Color(255, 0, 0, 255);
        
        Debug.DrawLine(startPos, ray.point, color);
        Debug.Log(ray.point);
        
        return ray.point;
    }
    
    public Vector2 GetDirectionVector2D(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }
}
