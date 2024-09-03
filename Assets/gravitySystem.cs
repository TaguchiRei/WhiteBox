using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySystem : MonoBehaviour
{
    [SerializeField, Range(1,10)] float _gravity = 1;
    [SerializeField] Rigidbody _rig;
    void Update()
    {
        //�d�͗p���C�L���X�g
        var hitG = Physics.Raycast(transform.position, transform.up*-1,out RaycastHit hit);
        if (hitG)
        {
            transform.SetParent(hit.transform);
        }
        
    }
    private void FixedUpdate()
    {
        //�d�͂�������
        Vector3 gravityDirection = new(0,-1*_gravity*9.81f,0);
        Vector3 worldG = transform.TransformPoint(gravityDirection);
        _rig.AddForce(worldG,ForceMode.Acceleration);
    }
}
