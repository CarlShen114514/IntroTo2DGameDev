using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ShootBehaviour : MonoBehaviour
{
    private Animator anim = null; 
    public GameObject bulletPrefab; // 子弹的预制件
    public Transform firePoint; // 发射子弹的位置
    private Vector2 lastMoveDirection = Vector2.right; // 默认向右


    public void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    public void Update(){
        Fire();
    }
    // 设置子弹方向
    public void SetDirection(Vector2 direction)
    {
        lastMoveDirection = direction.normalized;
    }

    // 发射子弹
    public void Fire()
    {
    if (Input.GetKeyDown(KeyCode.Q))
        {
            anim.SetBool("isShooting", true);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity); // 生成子弹
            bullet.GetComponent<MyBullet>().SetDirection(lastMoveDirection); // 设置子弹的方向
           
        }
        else
        {
            anim.SetBool("isShooting", false);
        }
    }
}
