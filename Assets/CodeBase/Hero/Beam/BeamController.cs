using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Hero.Beam;
using UnityEngine;

public class BeamController : MonoBehaviour
{
    [SerializeField] private BeamModel _model;
    [SerializeField] private MeshGenerator _meshGenerator;
    [SerializeField] private BeamCast _cast;
    
    private void Update()
    {
        var leftPoint =  _cast.Cast(_model.LeftAngle, _model.LeftStartPos, _model.CastMask, Color.cyan, _model.Range);
        var rightPoint = _cast.Cast(_model.RightAngle, _model.RightStartPos, _model.CastMask, Color.red, _model.Range);
        var midleftPoint = leftPoint + (rightPoint - leftPoint).normalized * Mathf.Abs(_model.LeftStartLocalPos.x);
        var midRightPoint = rightPoint + (leftPoint - rightPoint).normalized * Mathf.Abs(_model.RighStartLocalPos.x);
        _meshGenerator.Generate(leftPoint, _model.LeftStartPos, _model.RightStartPos, rightPoint, midleftPoint, midRightPoint);
    }
}
