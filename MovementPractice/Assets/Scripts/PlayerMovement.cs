using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D body;
    [SerializeField]private float speed;
    [SerializeField] private float JumpHeight;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    private Animator graphicsAnimator;
    private BoxCollider2D boxCollider;
    private float wallJumpCoolDown;
    private float horizontal;


    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        graphicsAnimator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
       
    }
   
    void Update()
    {
         horizontal = Input.GetAxis("Horizontal");
       
        if (horizontal > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontal < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        //Set animator bool
        graphicsAnimator.SetBool("Run", horizontal!=0);
        graphicsAnimator.SetBool("Grounded", isGrounded());

        if (wallJumpCoolDown > 0.2f)
        {
            //Flip right and left
            body.linearVelocity = new Vector3(horizontal * speed, body.linearVelocity.y, 0);

            if (onWall()&&!isGrounded())
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 7;
            }
            if (Input.GetKey(KeyCode.UpArrow))
                Jump();

        }
        else
            wallJumpCoolDown += Time.deltaTime;

       
    }
    private void Jump()
    {
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, JumpHeight);
            graphicsAnimator.SetTrigger("Jump");
        }
        else if(onWall() && !isGrounded())
        {
            if (horizontal == 0)
            {
             body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
             transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);

            }
            else
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            wallJumpCoolDown = 0;
           
        }
      
    
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
     
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,
            boxCollider.bounds.size,0,Vector2.down,0.1f, groundLayer);
        return raycastHit.collider!=null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center,
            boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }











}
