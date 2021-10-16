using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private LevelManager LM => LevelManager.Instance;
    private Vector2 _lastFeetPosition;
    private Rigidbody2D _rigidbody2D;
    public float HalvedHeight => transform.localScale.y;
    public Vector2 FeetPos => new Vector2(transform.position.x, transform.position.y - HalvedHeight);

    [SerializeField] private float jumpForce = 1;
    [SerializeField] private float hMovementSpeed = 1;
    private float _hMovement;
    


    #region Initialisation

    private void Awake()
    {
        _rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        _lastFeetPosition = FeetPos;
        RegisterForEvents();
    }
    
    private void RegisterForEvents()
    {
        LandedOnPlatform += Jump;
    }

    #endregion

    
    #region UnityEvents

    private void FixedUpdate()
    {
        // transform.position += Vector3.right * _hMovement * hMovementSpeed * Time.deltaTime;
        _rigidbody2D.AddForce(Vector2.right * (_hMovement * hMovementSpeed), ForceMode2D.Force);
        _lastFeetPosition = FeetPos;

        var transformPosition = transform.position;
        if (transformPosition.x > LM.DisplayWidth / 2)
        {
            transformPosition.x -= LM.DisplayWidth;
        }
        if (transformPosition.x < -LM.DisplayWidth / 2)
        {
            transformPosition.x += LM.DisplayWidth;
        }

        transform.position = transformPosition;
    }
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (LM.IsGameOver)
        {
            return;
        }
        
        var platform = other.gameObject.GetComponent<Platform>();
        if (platform != null)
        {
            if (platform.TopHeight <= _lastFeetPosition.y)
            {
                OnLandedOnPlatform(platform);
            }
        }
    }
    
    public void OnMove(InputAction.CallbackContext c)
    {
        _hMovement = c.ReadValue<Vector2>().x;
    }

    #endregion
    
    
    private void Jump(object sender, LandedOnPlatformEventArgs eventArgs)
    {
        // Debug.Log("JUMP");
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
        _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    
    

    #region Events

    public event EventHandler<LandedOnPlatformEventArgs> LandedOnPlatform;
    protected virtual void OnLandedOnPlatform(Platform platform)
    {
        LandedOnPlatform?.Invoke(this, new LandedOnPlatformEventArgs(platform, this));
    }

    #endregion
    
}

public class LandedOnPlatformEventArgs: EventArgs
{
    public LandedOnPlatformEventArgs(Platform platform, Player player)
    {
        Platform = platform;
        Player = player;
    }

    public Platform Platform { get; }
    public Player Player { get; }
}
