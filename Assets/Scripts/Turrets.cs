using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Turrets : MonoBehaviour
{
    public float Radius;
    public Transform muzzlePoint;
    public LayerMask playerLayer;
    public float timeToStartAttack;
    public GameObject bulletPrefab;
    public float bulletSpeed;
    protected bool isTargetDetevted;
    protected bool isAllowToAttack;
    private Vector3 direction;
    private Collider[] targets;
    public Collider[] Targets
    {
        get => targets; set
        {
            if (isTargetDetevted)
            {
                OnAction += Watching;
            }
            else
            {
                OnAction -= Watching;
            }
            targets = value;
        }
    }
    public Action OnAction;

    public void Detect()
    {
        isTargetDetevted = Physics.CheckSphere(transform.position, Radius, playerLayer);
        Targets = Physics.OverlapSphere(transform.position, Radius, playerLayer);
    }

    private void Watching()
    {
        direction = (Targets[0].transform.position - transform.position).normalized;
        direction.y = 0f;
        transform.rotation = Quaternion.LookRotation(direction);
        Physics.Raycast(muzzlePoint.position, direction, out RaycastHit raycastHit);
        if (raycastHit.collider.tag == "Player")
        {
            isAllowToAttack = true;
        }
        else
        {
            isAllowToAttack = false;
        }
    }

    public virtual void Attack()
    {
        GameObject currentBullet = Instantiate(bulletPrefab, muzzlePoint.position, Quaternion.Euler(direction));
        currentBullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, Radius);
        Gizmos.DrawRay(muzzlePoint.position, Vector3.forward);
    }
}
