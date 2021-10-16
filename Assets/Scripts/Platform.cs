using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : LevelObject
{

    public float Height => transform.localScale.y;
    public float TopHeight => transform.position.y + Height / 2;
    
    /// <summary>
    /// The Max Distance the next Platform needs to be, to reach it from this one.
    /// </summary>
    public float MaxYDistance => LevelManager.Instance.JumpHeight;
    public float MinYDistance => transform.localScale.y;


    public float Width => transform.localScale.x;
}