using UnityEditor;
using UnityEngine;
using UnityEngine.UI; // 需要导入这个命名空间以使用UI组件
using UnityEngine.SceneManagement;
using TMPro;
using Unity.Burst.Intrinsics;
using MsgBoxBase = System.Windows.Forms.MessageBox;
using WinForms = System.Windows.Forms;
public class PlayerController : MonoBehaviour
{
    private bool DKeyValid = true;
    private Rigidbody2D rb;
    private bool newOne=false;
    private Vector3 speed;
    public const float horizontalSpeed = 50f;
    private const float verticalSpeed = 50f;
    public const float jumpSpeed = 150f;
    private const float g = -350f;
    private float gravity = g;
    private bool isGrounded = false;
    private bool isAttached = false;
    public int health = 3; 
    public float climbForce = 10f; // 爬爬的力度
     private bool isOnTopOfLadder = false; // 标记是否站在梯子顶部
    private Animator anim = null;    
    public GameObject PanelGameOver; // 游戏结束的面板
    public string gameOverMessage = "菜"; // 通关信息
    private bool twoJump=false;
    private bool canDoubleJump = false;
    public TextMeshProUGUI note=null;
    public Canvas canvas;
    private TextMeshProUGUI Note;
    private bool isClimbing=false;
    public GameObject footCollider; // 脚底碰撞器
    private void Start()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        if(SceneManager.GetActiveScene().name == "FolderScene"){
            DKeyValid = false;
        }
        // transform.localPosition = new Vector3(-20, -20, 0);
        anim = GetComponent<Animator>();
        speed = new Vector3(0, 0, 0);
        gameObject.transform.localScale = new Vector3(-70, 70, 70);
        // isGrounded = true;
    }

    private void Update()
    {
     
        // Debug.Log("isClimbing:"+anim.GetBool("isClimbing"));
        // Debug.Log("isAttach:"+isAttached);
        CheckAccelerate();
        CheckAnime();
        Move();
        speed.y += gravity * Time.smoothDeltaTime;
        if (speed.y <= -300f)
        {
            speed.y = -300f;
        }
    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.A) ^ (Input.GetKey(KeyCode.D) && DKeyValid)){
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
        if ((isGrounded || isClimbing) && Input.GetKey(KeyCode.Space)){
            if(isClimbing){
                isClimbing=false;
            }
            speed.y = jumpSpeed;
             canDoubleJump = true; // 允许二段跳
        }
        else if (canDoubleJump && twoJump && Input.GetKeyDown(KeyCode.Space)&&!isGrounded)
        {
            speed.y = jumpSpeed;
            canDoubleJump = false; // 已经使用二段跳
        }
        if (isClimbing && isAttached){
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
        if (other.CompareTag("LadderTop") && footCollider.GetComponent<Collider2D>().IsTouching(other))
        {
            Debug.Log("脚底接触到梯子顶端，离开梯子");
            isAttached = false;
            isOnTopOfLadder = true;
            isGrounded = true; // 在梯子顶端时将角色视为在地面上
        }
        if (other.CompareTag("Ladder"))
    {
        if (isOnTopOfLadder)
        {
            // 如果角色在梯子顶部，不进入爬梯子状态
            return;
        }
        Debug.Log("进入梯子");
        isAttached = true;
    }
    if (other.CompareTag("Ground"))
    {
        isGrounded = false;
    }

        if (other.CompareTag("ladder")){
        //     if(!isClimbing){
        //      Vector2 screenPoint = Camera.main.WorldToScreenPoint(new Vector2(transform.localPosition.x,transform.localPosition.y));
        //     RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, screenPoint, canvas.GetComponent<Canvas>().worldCamera, out Vector2 localPoint);
        // // 在Canvas下生成文件名
        //     Note = Instantiate(note, canvas.transform);
        //     Note.GetComponent<RectTransform>().anchoredPosition = localPoint;
        //     Note.text="press 'E' to climb";
        //     }
            newOne=anim.GetBool("isClimbing");
            isAttached = true;                  //具备吸附条件
        }
        if (other.gameObject.CompareTag("Enemy")){
            anim.SetBool("isHurting", true);
            TakeDamage(1); // 每次碰撞减少1点生命值
            Destroy(other.gameObject); // 销毁与玩家相撞的敌人
        }
        if (other.gameObject.CompareTag("Ground")){
            isGrounded = true;
            gravity = 0f; // 碰到地面时重力为0
            
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
           // anim.SetBool("isRunning", false);
            anim.SetBool("isClimbing",false);
        }
    }
    public void TwoJumpValid(){
        MsgBoxBase.Show("获得了二段跳技能", "提示", WinForms.MessageBoxButtons.OK, WinForms.MessageBoxIcon.Asterisk);
        Debug.Log("现在可以二段跳了！");
        twoJump=true;
    }
    public void DKeyFix(){
         if(SceneManager.GetActiveScene().buildIndex == 0)
        MsgBoxBase.Show("这是一个键盘驱动修复程序！","提示", WinForms.MessageBoxButtons.OK, WinForms.MessageBoxIcon.Asterisk);
        if(SceneManager.GetActiveScene().buildIndex == 1)
        MsgBoxBase.Show("您的D键已经被修复！", "提示", WinForms.MessageBoxButtons.OK, WinForms.MessageBoxIcon.Asterisk);
        DKeyValid=true;
    }
    public void OnHurtAnimationEnd()
{
    anim.SetBool("isHurting", false);
}
    void OnTriggerExit2D(Collider2D other){
       if (other.CompareTag("LadderTop"))
    {
        Debug.Log("离开梯子顶");
        Debug.Log("此时重力" + gravity);
        isOnTopOfLadder = false;
        isGrounded = false; // 离开梯子顶端时将角色视为不在地面上
        isClimbing=false;
    }
        if (other.CompareTag("Ground")){
            isGrounded = false;
        }
        if (other.CompareTag("ladder")){
            //Destroy(Note);
            if(!newOne)
            {
                isClimbing=false;
                //isAttached = false;
            }
            newOne=false;
        }
        if (other.CompareTag("Ladder"))
    {
        if (speed.y < 0)
        {
            // 如果角色从梯子向下移动，设置为在梯子顶部
            isOnTopOfLadder = true;
        }
        else
        {
            // 角色离开梯子
            isAttached = false;
            isOnTopOfLadder = false;
            Debug.Log("离开梯子");
        }
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
        else if (isClimbing){
            gravity = 0f;
        }
         else if (isOnTopOfLadder)
    {
        // 如果角色在梯子顶部，重力为0
        gravity = 0f;
        speed.y = 0f;
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
        }
        if(isGrounded && speed.x != 0){
            anim.SetBool("isRunning", true);
        }
        else if(speed.x==0){
            anim.SetBool("isRunning",false);
        }
        if (isAttached && Input.GetKeyDown(KeyCode.E))
    {
        isClimbing = !isClimbing;
        Debug.Log("isClimbing"+isClimbing);
        anim.SetBool("isClimbing", isClimbing);
        anim.SetBool("isJumping",false);
        Debug.Log("isJumping"+anim.GetBool("isJumping"));
    }
        // if((Input.GetKeyDown(KeyCode.E)&&isClimbing)||!isAttached){
        //     anim.SetBool("isClimbing",false);
        //     isClimbing = false;
        // }
        if (!isGrounded && speed.y > 0 && !isClimbing){
            anim.SetBool("isJumping", true);
            anim.SetBool("isClimbing",false);
        }
        if (!isGrounded && speed.y < 0 && !isClimbing){
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", true);
        }
        if (isOnTopOfLadder)
    {
        anim.SetBool("isJumping", false);
        anim.SetBool("isFalling", false);
        anim.SetBool("isRunning", false);
        anim.SetBool("isClimbing", false);
        // 如果在梯子顶部，设置与地面相同的动画状态
        if (speed.x != 0)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
    }
}