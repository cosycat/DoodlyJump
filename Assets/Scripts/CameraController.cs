using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float maxPlayerCameraHeight;
    public Camera Camera { get; private set; }
    public float DisplayWidth { get; private set; }
    public float DisplayHeight { get; private set; }
    
    private LevelManager LM => LevelManager.Instance;
    private Player Player => LM.Player;

    private void Awake()
    {
        Camera = GetComponent<Camera>();
        DisplayHeight = Camera.orthographicSize * 2;
        DisplayWidth = Camera.aspect * DisplayHeight;

        LM.UpdateLevelPosition += UpdateCameraPosition;
    }


    private void UpdateCameraPosition(object sender, LevelPositionEventArgs e)
    {
        var newCameraYPos = e.newPosition - maxPlayerCameraHeight + Camera.orthographicSize;
        transform.position = new Vector3(transform.position.x, newCameraYPos, transform.position.z);
    }
}
