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
    float timer = 0;
    List<GameObject> tileList = new List<GameObject>();

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                transform.DetachChildren();
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
        List<Vector3> angle = new List<Vector3> { transform.forward, transform.right + transform.forward, transform.right, transform.right * -1 + transform.forward };
        RaycastHit Hit;
        RaycastHit Hit2;
        angle.ForEach(i =>
        {
            var ray = Physics.Raycast(transform.position, i, out Hit);
            var ray2 = Physics.Raycast(transform.position, i * -1, out Hit2);
            tileList.AddRange(new GameObject[] { Hit.collider.gameObject, Hit2.collider.gameObject });
        });
        tileList.ForEach(i => i.transform.SetParent(transform)) ;

        tileList.Clear();
        transform.DORotate(new Vector3(0, 90 * _rotateAngle, 0), _rotateTime, RotateMode.LocalAxisAdd);
        timer = _rotateTime;
    }
}
