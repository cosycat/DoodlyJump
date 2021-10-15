using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelGenerator : MonoBehaviour
{
    public LevelManager LM => LevelManager.Instance;
    
    private float JumpHeight => LM.JumpHeight;
    private int LevelBlockSize => LM.LevelBlockSize;

    private int _currentBlockNumber = 0;


    [SerializeField] private float minYPlatformDistance = 1;
    [SerializeField] private Platform basicPlatform;
    

    private void Awake()
    {
        LM.UpdateLevelBlockPosition += OnNewBlockReached;
    }

    private void Start()
    {
        CreateNewLevelBlock(0, isFirstBlock: true);
        CreateNewLevelBlock(1);
    }

    private void OnNewBlockReached(object sender, LevelPositionEventArgs e)
    {
        CreateNewLevelBlock(_currentBlockNumber + 2);
        _currentBlockNumber++;
    }

    private void CreateNewLevelBlock(int blockNumber, bool isFirstBlock = false)
    {
        float newHeight = LevelBlockSize * blockNumber;
        while (newHeight < LevelBlockSize * (blockNumber + 1))
        {
            var newPlatform = Instantiate(basicPlatform, transform, true);
            // Set the Position of the new Platform
            var newPlatformTransform = newPlatform.transform;
            var platformTransformPosition = newPlatformTransform.position;
            platformTransformPosition.y = newHeight;
            var maxDistanceFromCenter = LM.DisplayWidth / 2 - newPlatform.Width / 2;
            Debug.Log("maxDistanceFromCenter = " + maxDistanceFromCenter);
            platformTransformPosition.x = Random.Range(-maxDistanceFromCenter, maxDistanceFromCenter);
            newPlatformTransform.position = platformTransformPosition;
            
            // Lastly update the new height for the next platform
            newHeight += Random.Range(minYPlatformDistance, newPlatform.MaxYDistance);
        }
        // Old loop
        // for (var i = 0; i < LevelBlockSize; i+= (int)JumpHeight)
        // {
        //     var newPlatform = Instantiate(basicPlatform, transform, true);
        //     
        // }

        Debug.Log("New Level Block Created");
    }
    
}
