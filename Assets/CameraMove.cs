using UnityEngine;
using UnityEngine.UI;

public class CameraMove : MonoBehaviour
{
    [SerializeField] Image Image;
    [SerializeField] Sprite[] sprites;
    [SerializeField] GameManager _gameManager;
    float time = 0;
    
    void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time < 0)
            {
                time = 0;
            }
        }
        var mouse = Input.GetAxisRaw("Mouse Y");
        if (mouse != 0f)
        {
            transform.Rotate(mouse * -1, 0, 0);
        }
        var ray = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit);
        if (ray && hit.transform.gameObject.CompareTag("CenterTile"))
        {
            Image.sprite = sprites[0];
            if (Input.GetButtonDown("Jump") && time == 0f)
            {
                hit.collider.gameObject.GetComponent<CenterTile>().RotateTile();
                _gameManager.AddRotate();
                time = 1.2f;
            }
        }
        else
        {
            Image.sprite = sprites[1];
        }
    }
}
