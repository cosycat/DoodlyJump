using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{

    public float Height => transform.localScale.y;
    public float TopHeight => transform.position.y + Height / 2;
    
}
