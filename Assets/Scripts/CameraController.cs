using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float maxPlayerCameraHeight;
    public Camera Camera { get; private set; }
    private LevelManager LM => LevelManager.Instance;
    private Player Player => LM.Player;

    private void Awake()
    {
        Camera = GetComponent<Camera>();

        LM.UpdateLevelPosition += UpdateCameraPosition;
    }
    

    private void UpdateCameraPosition(object sender, LevelPositionEventArgs e)
    {
        Debug.Log("UpdateCam");
        var newCameraYPos = e.newPosition - maxPlayerCameraHeight + Camera.orthographicSize;
        transform.position = new Vector3(transform.position.x, newCameraYPos, transform.position.z);
    }
}
