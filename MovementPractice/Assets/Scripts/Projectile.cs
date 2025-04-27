using Unity.Mathematics;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private float lifeTime;
    private Animator graphicsAnimator;
    private BoxCollider2D boxCollider;

     void Awake()
    {
        graphicsAnimator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();

    }
     void Update()
    {
        if (hit)
        {
            return;
        }
        float movementSpeed = speed * Time.deltaTime* direction;
        transform.Translate(movementSpeed,0,0);
        lifeTime += Time.deltaTime;
        if (lifeTime > 5)
            Deactive();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        graphicsAnimator.SetTrigger("explode");
    }
    public void SetDirection(float _direction)
    {
        lifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;
        float LocalScaleX = transform.localScale.x;
        if (Mathf.Sign(LocalScaleX) != direction)
            LocalScaleX = -LocalScaleX;
        transform.localScale =new Vector3(LocalScaleX, transform.localScale.y, transform.localScale.z);
    }
    private void Deactive()
    {
        gameObject.SetActive(false);
    }
}
