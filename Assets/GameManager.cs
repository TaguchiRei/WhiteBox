using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool _canMove = true;
    float _key = 0;
    float _rightRotate = 0;
    float _leftRotate = 0;
    [SerializeField, Range(0, 5)] int _keyNumber = 5;
    [SerializeField] List<GameObject> _titleButtonList = new();
    Action _gameStart;
    Mode _mode;

    /// <summary>
    /// �^�C�g���\�����s���Ƃ��Ɏg���v���O����������
    /// </summary>
    public Action GameStart
    {
        get { return _gameStart; }
        set { _gameStart = value; }
    }
    public Action Confirm
    {
        get;set;
    }

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _gameStart();
        _mode = Mode.title;
        //�Q�[�����X�^�[�g(�X�^�[�g�{�^�����������Ƃ��̏����ɂ��Ƃł���)
        _titleButtonList.ForEach(t => t.SetActive(false));
    }

    /// <summary>
    /// ��]�������񐔂�ۑ�����B��]�����ɂ����0��1����͂ɓ����
    /// </summary>
    public void AddRotate(int rotateDirection)
    {
        if(rotateDirection == 0)
        {
            _rightRotate++;
        }
        else
        {
            _leftRotate++;
        }
    }

    /// <summary>
    /// �����擾�����Ƃ��Ɏg��
    /// </summary>
    public void AddKey()
    {
        _key++;
    }

    enum Mode
    {
        title,
        play,
        result,
        pause,
    }
}
