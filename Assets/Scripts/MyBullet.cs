using UnityEngine;

public class MyBullet : MonoBehaviour
{
    public float speed = 10f;
    public float damage = 1f;

    private Vector2 direction;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    private void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime); // 子弹按照方向移动
    }

   // private void OnTriggerEnter2D(Collider2D collision)
    // {
    //     Enemy enemy = collision.GetComponent<Enemy>();
    //     if (enemy != null)
    //     {
    //         enemy.TakeDamage(damage); // 对敌人造成伤害
    //         Destroy(gameObject); // 销毁子弹
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Enemy enemy = collision.GetComponent<Enemy>();
        if (collision.gameObject.CompareTag("Enemy") )
        {
            Debug.Log("子弹击杀敌人了");
            // enemy.TakeDamage(damage); // 对敌人造成伤害
            Destroy(collision.gameObject); // 销毁子弹
            Destroy(gameObject);
        }
    }
}
