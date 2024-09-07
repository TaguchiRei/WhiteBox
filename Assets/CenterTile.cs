using DG.Tweening;
using System.Collections.Generic;
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
    [SerializeField] GameObject _key;
    GameManager _gameManager;
    float timer = 0;
    public List<Vector3> angle;
    public List<GameObject> tileList;

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

    /// <summary>
    /// スタートボタンを押した後に動かす鵜プログラムを入れる
    /// 現在壁の生成を行うプログラムが入っている
    /// </summary>
    void GameStart()
    {
        bool colorChange = false;
        bool keyGenerate = this.gameObject.name != "whiteCenter";
        float keyPlace = Random.Range(0, 10);
        for (var i = 0; i < 3; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                var random = Random.Range(0, 2);
                //0.54*50 =27 足元のキューブのサイズが50で壁は4なので逸れの半径動詞を足し合わせて27。それをローカル座標に合わせて0.54になる
                Vector3 pos = new(i - 1, 0.54f, j - 1);
                var A = Instantiate(_walls[random]);
                A.transform.SetParent(transform);
                A.transform.rotation = transform.rotation;
                A.transform.localPosition = pos;
                //位置マスごとに色を変える
                if (colorChange)
                {
                    var renderer = A.transform.GetComponentsInChildren<Renderer>();
                    foreach (var r in renderer)
                    {
                        if (!r.gameObject.CompareTag("Pillar"))
                        {
                            r.material.color = Color.white;
                        }
                    }
                }
                colorChange = !colorChange;
                //鍵を生成する
                if (keyGenerate && i * 3 + j == keyPlace)
                {
                    var key = Instantiate(_key);
                    key.transform.SetParent(transform);
                    key.transform.localPosition = pos;
                    key.transform.rotation = transform.rotation;
                    key.GetComponent<Renderer>().material.color = this.GetComponent<Renderer>().material.color;
                    key.GetComponent<Renderer>().material.SetColor("_EmissionColor",this.GetComponent<Renderer>().material.color);
                }
            }

        }
    }
}
