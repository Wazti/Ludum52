using System;
using System.Collections;
using System.Collections.Generic;
using CodeBase.Hero.Beam;
using UnityEngine;

public class BeamController : MonoBehaviour
{
    [SerializeField] private BeamModel _model;
    [SerializeField] private BeamView _view;
    [SerializeField] private MeshGenerator _meshGenerator;
    [SerializeField] private BeamCast _cast;
    
    private void Update()
    {
        var leftPoint =  _cast.Cast(angle: _model.LeftAngle, startPos:_model.LeftStartPos, mask:_model.CastMask, Color.cyan);
        var rightPoint = _cast.Cast(angle: _model.RightAngle, startPos:_model.RightStartPos, mask:_model.CastMask, Color.red);
        _meshGenerator.Generate(leftPoint, _model.LeftStartPos, _model.RightStartPos, rightPoint);
    }
}
