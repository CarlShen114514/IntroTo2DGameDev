using System;
using UnityEngine;
using UnityEngine.UI; // 需要导入这个命名空间以使用UI组件


public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool newOne;
    private Vector3 speed;
    public const float horizontalSpeed = 50f;
    private const float verticalSpeed = 50f;
    public const float jumpSpeed = 150f;
    public const float maxSpeed = 150f;
    private const float g = -350f;
    private float gravity = g;
    private bool isGrounded = false;
    private bool isAttached = false;
    public int health = 3; 
    public float climbForce = 10f; // 爬爬的力度
    private Animator anim = null;    
    public GameObject PanelGameOver; // 游戏结束的面板
    public string gameOverMessage = "菜"; // 通关信息

    private void Start()
    {
        // transform.localPosition = new Vector3(-20, -20, 0);
        anim = GetComponent<Animator>();
        speed = new Vector3(0, 0, 0);
        gameObject.transform.localScale = new Vector3(-70, 70, 70);
        // isGrounded = true;
    }

    private void Update()
    {
        CheckAccelerate();
        CheckAnime();
        Move();
        speed.y += gravity * Time.smoothDeltaTime;
        if (speed.y <= -300f)
        {
            speed.y = -300f;
        }
        Debug.Log(speed.y);

    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.A) ^ Input.GetKey(KeyCode.D)){
            if (Input.GetKey(KeyCode.A)){
                speed.x = -horizontalSpeed;
                gameObject.transform.localScale = new Vector3(-70, 70, 70);
            }
            else{
                speed.x = horizontalSpeed;
                gameObject.transform.localScale = new Vector3(70, 70, 70);
            }       
        }
        else{
            speed.x = 0f;
        }
        if ((isGrounded || isAttached) && Input.GetKey(KeyCode.Space)){
            speed.y = jumpSpeed;
        }
        if (isAttached){
            if (Input.GetKey(KeyCode.W) ^ Input.GetKey(KeyCode.S)){
                if (Input.GetKey(KeyCode.W)){
                    speed.y = verticalSpeed;
                }
                else{
                    speed.y = -verticalSpeed;
                } 
            }
            else{
                speed.y = 0f;
            }
        }
        transform.localPosition = transform.localPosition + speed * Time.smoothDeltaTime;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ladder")){
            newOne=anim.GetBool("isClimbing");
            isAttached = true;
        }
        if (other.gameObject.CompareTag("Enemy")){
            TakeDamage(1); // 每次碰撞减少1点生命值
            Destroy(other.gameObject); // 销毁与玩家相撞的敌人
        }
        if (other.gameObject.CompareTag("Ground")){
            isGrounded = true;
            gravity = 0f; // 碰到地面时重力为0
        }
    }
    
    void OnTriggerExit2D(Collider2D other){
        if (other.CompareTag("Ground")){
            isGrounded = false;
        }
        if (other.CompareTag("ladder")){
            if(!newOne)
                isAttached = false;
            newOne=false;
        }
    }
    public void TakeDamage(int damage){
        health -= damage;
        if (health <= 0){
            Die();
        }
    }
    private void Die(){
        Text victoryText = PanelGameOver.GetComponentInChildren<Text>();
        victoryText.text = gameOverMessage;
        PanelGameOver.SetActive(true);
        Destroy(gameObject); // 销毁玩家对象
        Time.timeScale = 0f;
    }
    void CheckAccelerate(){
        if(isGrounded){
            gravity = 0f;
            speed.y = 0f;
        }
        else if (isAttached){
            gravity = 0f;
        }
        else
        {
            gravity = g;
        }
    }
    void CheckAnime(){
        if(isGrounded){
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
            anim.SetBool("isRunning", false);
        }
        if(isGrounded && speed.x != 0){
            anim.SetBool("isRunning", true);
        }
        if (isAttached){
            anim.SetBool("isClimbing", true);
        }
        if (!isGrounded && speed.y > 0){
            anim.SetBool("isJumping", true);
        }
        if (!isGrounded && speed.y < 0){
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }
    }
}