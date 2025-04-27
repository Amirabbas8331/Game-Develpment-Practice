using Unity.Mathematics;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    private float cooldownTimer=math.INFINITY;
    private Animator graphicsAnimator;
    private PlayerMovement movement;
    void Awake()
    {
        graphicsAnimator = GetComponent<Animator>();
        movement = GetComponent<PlayerMovement>();
    }
     void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer>attackCooldown && movement.canAttack())
            attack();
        cooldownTimer += Time.deltaTime;
    
       

    }

    private void attack()
    {
        graphicsAnimator.SetTrigger("attack");
        cooldownTimer = 0;
        //pool object
        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindFireball()
    {
        for (int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
              return i;
        }
        return 0;
        
    }
}
