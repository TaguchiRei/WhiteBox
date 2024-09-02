using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool _canMove = true;
    [SerializeField ,Range(0,5)] int _keyNumber = 5;
    Action _gameStart;
    private void Start()
    {
        
    }
}
