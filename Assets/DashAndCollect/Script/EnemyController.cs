using Nitzz.Utility;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2f;          // kecepatan musuh
    public Rigidbody2D rb;
    public Transform target;             // player

    public Vector2 moveDirection;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (target != null)
        {
            // arah ke player
            Vector2 direction = (target.position - transform.position).normalized;
            moveDirection = direction;
        }
    }

    private void FixedUpdate()
    {
        // musuh bergerak ke arah player
        rb.linearVelocity = moveDirection * moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // kalau nabrak player
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player hit by enemy!");
            AudioManager.instance.PlaySFX("hit");
            GameManage.instance.HitPlayer();
        }
    }
}
