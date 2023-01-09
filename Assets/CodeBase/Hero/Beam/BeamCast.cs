using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamCast : MonoBehaviour
{
    public Vector2 Cast(float angle, Vector2 startPos, LayerMask mask, Color color, float range)
    {
        var direction = GetDirectionVector2D(angle);

        var ray = Physics2D.Raycast(startPos, direction, range, mask);

        var result = Vector2.zero;

        if (ray.collider == null)
        {
            result = startPos + direction * range;
        }
        else
        {
            result = ray.point;
            ;
        }

        Debug.DrawLine(startPos, result, color);

        return result;
    }

    public Vector2 GetDirectionVector2D(float angle)
    {
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }
}