using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool _canMove = true;
    public static float _key = 0;
    public static float _rotate = 0;
    [SerializeField, Range(0, 5)] int _keyNumber = 5;
    Action _gameStart;

    public Action GameStart
    {
        get { return _gameStart; }
        set { _gameStart = value; }
    }
    public Action tileLayer
    {
        get; set;
    }
    private void Start()
    {
        _gameStart();
    }
    public void AddRotate()
    {
        _rotate++;
    }
    public void AddKey()
    {
        _key++;
    }
}
