using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Collider2D coll,disColl;
    private Animator anim;

    public float speed, jumpForce;
    public Transform cellingCheck, groundCheck;
    public LayerMask ground,enemy;
    public Text cherryNum, gemNum;

    Transform stampPiont;

    [SerializeField]
    private bool isGround, isJump,isHurt;
    bool jumpPressed;
    int jumpCount;
    private int Cherry,Gem;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CircleCollider2D>();
        disColl = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        stampPiont = transform.Find("stampPoint");
    }

    
    void Update()
    {
        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpPressed = true;
        }
        cherryNum.text = Cherry.ToString();
        gemNum.text = Gem.ToString();
    }

    private void FixedUpdate()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, 0.1f, ground);
        if (!isHurt)
        {
            GroundMovement();
        }
        Jump();
        Crouch();
        SwitchAnim();
    }

    //移动
    void GroundMovement()
    {
        float horizontalMove = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(horizontalMove * speed, rb.velocity.y);
        if(horizontalMove != 0)
        {
            transform.localScale = new Vector3(horizontalMove, 1, 1);
        }
    }

    //跳跃
    void Jump()
    {
        if (isGround)
        {
            jumpCount = 2;
            isJump = false;
        }
        if(jumpPressed && isGround)
        {
            isJump = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            SoundManager.instance.JumpAudio();
            jumpCount--;
            jumpPressed = false;
        }else if (jumpPressed && jumpCount > 0 && isJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            SoundManager.instance.JumpAudio();
            jumpCount--;
            jumpPressed = false;
        }
    }

    //蹲下
    void Crouch()
    {
        if (!Physics2D.OverlapCircle(cellingCheck.position, 0.2f, ground))
        {
            if (Input.GetButton("Crouch"))
            {
                anim.SetBool("crouching", true);
                disColl.enabled = false;
            }
            else
            {
                anim.SetBool("crouching", false);
                disColl.enabled = true;
            }
        }
    }

    //切换动画
    void SwitchAnim()
    {
        anim.SetFloat("running", Mathf.Abs(rb.velocity.x));

        if (isGround)
        {
            anim.SetBool("falling", false);
        }else if (!isGround && rb.velocity.y > 0)
        {
            anim.SetBool("jumping", true);
        }else if(rb.velocity.y < 0)
        {
            anim.SetBool("jumping", false);
            anim.SetBool("falling", true);
        }

        if (isHurt)
        {
            anim.SetBool("hurt", true);
            anim.SetFloat("running", 0);
            if (Mathf.Abs(rb.velocity.x) < 0.1f)
            {
                isHurt = false;
                anim.SetBool("hurt", false);
            }
        }
    }

    //消灭敌人

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")//如果碰撞物tag为enemy
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (anim.GetBool("falling"))
            {
                enemy.JumpOn();
                rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
                anim.SetBool("jumping", true);
            }//受伤
            else if(transform.position.x < collision.gameObject.transform.position.x)//判断如果X值小于碰撞物（敌人）X值
            {
                rb.velocity = new Vector2(-5f, rb.velocity.y);
                SoundManager.instance.HurtAudio();
                isHurt = true;
            }
            else if (transform.position.x > collision.gameObject.transform.position.x)
            {
                rb.velocity = new Vector2(5f, rb.velocity.y);
                SoundManager.instance.HurtAudio();
                isHurt = true;
            }
        }
    }

    //物品收集
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //收集物品
        if (collision.tag == "Collection")//樱桃
        {
            SoundManager.instance.CherryAudio();
            collision.GetComponent<Animator>().Play("isGot");
        }
        if (collision.tag == "Gem")
        {
            SoundManager.instance.GemAudio();
            collision.GetComponent<Animator>().Play("gemGot");
        }


        if (collision.tag == "DeadLine")//碰到死亡线
        {
            SoundManager.instance.GameoverAudio();
            Invoke("Restart", 2f);
        }
    }

    //死亡
    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //樱桃计数
    public void CherryCount()
    {
        Cherry += 1;
    }
    //宝石计数
    public void GemCount()
    {
        Gem += 1;
    }

}

