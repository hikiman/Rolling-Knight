using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float Move;
    public float speed;
    public float jump;
    public Vector2 boxSize;
    public float castDistance;
    public LayerMask groundLayer;
    public Animator animator;
    public CoinManager coinManager;
    private bool isFacingRight;

    public GameObject attackPoint;
    public float damage;
    public float radius;
    public LayerMask enemies;

    public float rollSpeed = 8f;   
    public float rollDuration = 0.5f; 
    private bool isRolling = false; 
    private float rollCooldown = 0.7f;
    private float lastRollTime;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        isFacingRight = true;
        lastRollTime = -rollCooldown; 
    }

    // Update is called once per frame
    void Update()
    {
        Move = Input.GetAxisRaw("Horizontal");

        
        if (!isRolling)
        {
            rb.velocity = new Vector2(Move * speed, rb.velocity.y);

            
            if (Input.GetButtonDown("Jump") && isGrounded())
            {
                rb.AddForce(new Vector2(rb.velocity.x, jump * 10));
            }

            if (Move != 0)
            {
                animator.SetBool("isRun", true);
            }
            else
            {
                animator.SetBool("isRun", false);
            }

            animator.SetBool("isJump", !isGrounded());

            if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded() && Time.time > lastRollTime + rollCooldown)
            {
                StartRoll();
            }
        }

        if (!isFacingRight && Move > 0)
        {
            Flip();
        }
        else if (isFacingRight && Move < 0)
        {
            Flip();
        }
    }

    private void StartRoll()
    {
        if (!isRolling && isGrounded())
        {
            isRolling = true;
            lastRollTime = Time.time;

            animator.SetBool("isRoll", true);
            StartCoroutine(PerformRoll());
        }
    }

   
    private IEnumerator PerformRoll()
    {
        float rollDirection = isFacingRight ? 1f : -1f;

        if (Move == 0)
        {
            rb.velocity = new Vector2(rollDirection * rollSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(Move * rollSpeed, rb.velocity.y);
        }

        gameObject.tag = "Untagged";

        yield return new WaitForSeconds(rollDuration);

        animator.SetBool("isRoll", false);

        isRolling = false;

        gameObject.tag = "Player";
    }

    public bool isGrounded()
    {
        if (Physics2D.BoxCast(transform.position, boxSize, 0, -transform.up, castDistance, groundLayer))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
            coinManager.coinCount++;
        }
    }

    public void attack()
    {
        Collider2D[] enemy = Physics2D.OverlapCircleAll(attackPoint.transform.position, radius, enemies);

        Debug.Log("Атака! Найдено потенциальных целей: " + enemy.Length);

        foreach (Collider2D enemyGO in enemy)
        {
            Debug.Log("Попытка атаковать: " + enemyGO.gameObject.name);

            // Проверяем наличие компонента EnemyHealth
            EnemyHealth enemyHealth = enemyGO.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                Debug.Log("Нанесён урон обычному врагу: " + damage);
                enemyHealth.health -= damage;
            }

            // Проверяем наличие компонента BossHealth
            BossHealth bossHealth = enemyGO.GetComponent<BossHealth>();
            if (bossHealth != null && !bossHealth.isInvulnerable)
            {
                Debug.Log("Нанесён урон боссу: " + damage);
                bossHealth.health -= damage;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(attackPoint.transform.position, radius);
    }
}