using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool _canMove = true;
    public static float _key = 0;
    public static float _rotate = 0;
    [SerializeField ,Range(0,5)] int _keyNumber = 5;
    Action _gameStart;

    public Action GameStart
    {
        get { return _gameStart; }
        set { _gameStart = value; }
    }
    private void Start()
    {
        _gameStart();
    }
    public  void AddRotate()
    {
        _rotate++;
    }
    public  void Awake()
    {
        _key++;
    }
}
