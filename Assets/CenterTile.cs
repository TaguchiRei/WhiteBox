using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �ʂ���]�����邽�߂̃v���O�����B�S�����ɑΉ��ς݁B
/// �L���[�u�̒����̃^�C���ɃA�^�b�`���Ďg�p����B
/// ��]���������Ƃ���RotateTile()���Ăяo���Ďg�p����B
/// Rotate Angle�ɂ͉�]������1��-1�œ��͂���
/// Rotate Time�ɂ͉�]���Ԃ�����
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
    /// ��ʂ���]�����邽�߂̃v���O�����B
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
    /// �X�^�[�g�{�^������������ɓ������L�v���O����������
    /// ���ݕǂ̐������s���v���O�����������Ă���
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
                //0.54*50 =27 �����̃L���[�u�̃T�C�Y��50�ŕǂ�4�Ȃ̂ň��̔��a�����𑫂����킹��27�B��������[�J�����W�ɍ��킹��0.54�ɂȂ�
                Vector3 pos = new(i - 1, 0.54f, j - 1);
                var A = Instantiate(_walls[random]);
                A.transform.SetParent(transform);
                A.transform.rotation = transform.rotation;
                A.transform.localPosition = pos;
                //�ʒu�}�X���ƂɐF��ς���
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
                //���𐶐�����
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
