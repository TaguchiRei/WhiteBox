using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField, Range(1, 10)] float _gravity = 1;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] Rigidbody _rig;
    [SerializeField] GameManager _gameManager;
    private void Awake()
    {
        _gameManager.GameStart += GameStart;
    }
    Vector3 moveDirection = Vector3.zero;
    void Update()
    {
        //重力用レイキャスト
        //var hitG = Physics.Raycast(transform.position, transform.up * -1, out RaycastHit hit);
        //if (hitG)
        //{
        //    transform.SetParent(hit.transform);
        //}
        //動かすためのプログラム
        var holizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var mouseHolizontal = Input.GetAxisRaw("Mouse X");
        if (mouseHolizontal != 0)
        {
            transform.Rotate(0, mouseHolizontal, 0);
        }
        if (vertical != 0)
        {
            moveDirection += transform.forward * vertical;
        }
        if (holizontal != 0)
        {
            moveDirection += transform.right * holizontal;
        }
    }
    private void FixedUpdate()
    {
        moveDirection.Normalize();
        if (GameManager._canMove)
        {
            _rig.velocity = moveDirection * moveSpeed;
        }
        moveDirection = Vector3.zero;
        //重力をかける
        _rig.AddForce(_gravity * 9.81f * transform.TransformDirection(new Vector3(0, -1, 0)), ForceMode.Acceleration);
    }
    private void OnTriggerEnter(Collider other)
    {
        transform.SetParent(other.transform);
    }
    void GameStart()
    {
        transform.position = new Vector3(0, 27.5f, 0);
    }
}
