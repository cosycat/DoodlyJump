using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    private IEnumerable<Platform> _platforms = new List<Platform>();

    public Player Player { get; private set; }

    private float PlayerYPosition => Player.FeetPos.y;

    private float _levelYPosition;
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
        if (LevelYPosition < PlayerYPosition)
        {
            LevelYPosition = PlayerYPosition;
        }
    }


    
    #region Events

    public event EventHandler<LevelPositionEventArgs> UpdateLevelPosition;
    
    protected virtual void OnUpdateLevelPosition(LevelPositionEventArgs e)
    {
        UpdateLevelPosition?.Invoke(this, e);
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