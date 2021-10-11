using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float maxPlayerCameraHeight;
    private Camera _camera;
    private LevelManager LM => LevelManager.Instance;
    private Player Player => LM.Player;

    private void Awake()
    {
        _camera = GetComponent<Camera>();

        LM.UpdateLevelPosition += UpdateCameraPosition;
    }
    

    private void UpdateCameraPosition(object sender, LevelPositionEventArgs e)
    {
        var newCameraYPos = e.newPosition - maxPlayerCameraHeight + _camera.orthographicSize;
        transform.position = new Vector3(transform.position.x, newCameraYPos, transform.position.z);
    }
}
