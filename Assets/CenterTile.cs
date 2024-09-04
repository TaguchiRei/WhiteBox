using DG.Tweening;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
/// <summary>
/// 面を回転させるためのプログラム。全方向に対応済み。
/// キューブの中央のタイルにアタッチして使用する。
/// 回転させたいときにRotateTile()を呼び出して使用する。
/// Rotate Angleには回転方向を1か-1で入力する
/// Rotate Timeには回転時間を入れる
/// </summary>
public class CenterTile : MonoBehaviour
{
    [SerializeField] float _rotateAngle = 1;
    [SerializeField] float _rotateTime = 1;
    [SerializeField] GameObject[] _walls;
    GameManager _gameManager;
    float timer = 0;
    public List<Vector3> angle; //= new List<Vector3> ();
    public List<GameObject> tileList; //new List<GameObject>();

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                var wall = transform.Find("CenterWall");
                transform.DetachChildren();
                wall.transform.SetParent(transform);
                GameManager._canMove = true;
            }
        }
    }
    /// <summary>
    /// 一面を回転させるためのプログラム。
    /// </summary>
    public void RotateTile()
    {
        GameManager._canMove = false;
        angle.AddRange(new Vector3[] { transform.forward, transform.right + transform.forward, transform.right, transform.right * -1 + transform.forward });
        angle.ForEach(i =>
        {
            var ray = Physics.Raycast(transform.position, i, out RaycastHit Hit);
            var ray2 = Physics.Raycast(transform.position, i * -1, out RaycastHit Hit2);
            tileList.AddRange(new GameObject[] { Hit.collider.gameObject, Hit2.collider.gameObject });
        });
        tileList.ForEach(i => i.transform.SetParent(transform));

        tileList.Clear();
        transform.DORotate(new Vector3(0, 90 * _rotateAngle, 0), _rotateTime, RotateMode.LocalAxisAdd);
        timer = _rotateTime;
    }
    private void Awake()
    {
        _gameManager = GameObject.FindObjectOfType<GameManager>();
        _gameManager.GameStart += GameStart;
    }
    private void Start()
    {
        //GameStart();
    }

    void GameStart()
    {
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                var random = Random.Range(0, 2);
                Vector3 pos = new (i - 1, 0.54f, j - 1);
                var A = Instantiate(_walls[random]);
                A.transform.SetParent(transform);
                A.transform.rotation = transform.rotation;
                A.transform.localPosition = pos;
            }

        }
    }
}
