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
        //ìÆÇ©Ç∑ÇΩÇﬂÇÃÉvÉçÉOÉâÉÄ
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");
        var mouseHorizontal = Input.GetAxisRaw("Mouse X");
        if (mouseHorizontal != 0)
        {
            transform.Rotate(0, mouseHorizontal, 0);
        }
        if (vertical != 0)
        {
            moveDirection += transform.forward * vertical;
        }
        if (horizontal != 0)
        {
            moveDirection += transform.right * horizontal;
        }
    }
    private void FixedUpdate()
    {
        moveDirection.Normalize();
        if (GameManager._canMove)
        {
            _rig.velocity = moveDirection * moveSpeed;
        }
        else
        {
            _rig.velocity = Vector3.zero;
        }
        moveDirection = Vector3.zero;
        //èdóÕÇÇ©ÇØÇÈ
        _rig.AddForce(_gravity * 9.81f * transform.TransformDirection(new Vector3(0, -1, 0)), ForceMode.Acceleration);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tile"))
        {
            transform.SetParent(other.transform);
        }
    }
    void GameStart()
    {
        transform.position = new Vector3(0, 27.5f, 0);
    }
}
