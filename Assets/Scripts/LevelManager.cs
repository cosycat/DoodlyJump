using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private IEnumerable<Platform> _platforms = new List<Platform>();

    public Player Player { get; private set; }

    public bool IsGameOver { get; private set; } = false;
    
    private float PlayerFeetPos => Player.FeetPos.y;
    public float DisplayWidth => cameraController.DisplayWidth;
    public float DisplayHeight => cameraController.DisplayHeight;

    public float DeathLine => cameraController.transform.position.y - DisplayHeight / 2;
    
    private float _levelYPosition;
    
    [SerializeField] private int levelBlockSize = 1000;
    [SerializeField] private float jumpHeight = 10;
    [SerializeField] private CameraController cameraController;


    public int LevelBlockSize => levelBlockSize;
    public float JumpHeight => jumpHeight;

    /// <summary>
    /// The Position the Player's feet were the highest yet in the current run.
    /// </summary>
    public float LevelYPosition
    {
        get => _levelYPosition;
        set
        {
            OnUpdateLevelPosition(new LevelPositionEventArgs(previousPosition: _levelYPosition, newPosition: value));
            _levelYPosition = value;
        }
    }


    private void Awake()
    {
        if (Instance is null)
            Instance = this;
        else
            Debug.LogError("Multiple Level Manager Instances!");
        
        if (Player == null)
        {
            Player = FindObjectOfType<Player>();
        }
    }

    private void Update()
    {
        if (LevelYPosition < PlayerFeetPos)
        {
            LevelYPosition = PlayerFeetPos;
        }

        if (PlayerFeetPos < DeathLine)
        {
            IsGameOver = true;
        }
    }


    
    #region Events

    /// <summary>
    /// Called every frame, the Player jumped higher than ever before.
    /// The previous highest position and the new highest position are in the LevelPositionEventArgs.
    /// </summary>
    public event EventHandler<LevelPositionEventArgs> UpdateLevelPosition;
    public event EventHandler<LevelPositionEventArgs> UpdateLevelBlockPosition;

    private void OnUpdateLevelPosition(LevelPositionEventArgs e)
    {
        UpdateLevelPosition?.Invoke(this, e);
        if ((int)(e.previousPosition / LevelBlockSize) != (int)(e.newPosition / LevelBlockSize))
        {
            OnUpdateLevelBlockPosition(e);
        }
    }

    private void OnUpdateLevelBlockPosition(LevelPositionEventArgs e)
    {
        UpdateLevelBlockPosition?.Invoke(this, e);
    }

    #endregion
}




public class LevelPositionEventArgs : EventArgs
{
    public LevelPositionEventArgs(float previousPosition, float newPosition)
    {
        this.previousPosition = previousPosition;
        this.newPosition = newPosition;
    }
    public float previousPosition { get; }
    public float newPosition { get; }
}
