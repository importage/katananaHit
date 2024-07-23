using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KatanaScript : MonoBehaviour
{
    [SerializeField]
    private Vector2 throwForce;

    private bool isActive = true;

    private Rigidbody2D rb;
    private BoxCollider2D katanaCollider;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        katanaCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && isActive)
        {
            rb.AddForce(throwForce, ForceMode2D.Impulse);
            rb.gravityScale = 1;
            GameController.Instance.GameUI.DecrementDisplayKatanaCount();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isActive)
            return;

        isActive = false;

        if (collision.collider.tag == "Log")
        {
            GetComponent<ParticleSystem>().Play();
            rb.velocity = new Vector2(0, 0);
            rb.bodyType = RigidbodyType2D.Kinematic;
            transform.SetParent(collision.collider.transform);

            katanaCollider.offset = new Vector2(katanaCollider.offset.x, -0.4f);
            katanaCollider.size = new Vector2(katanaCollider.size.x, 1.2f);

            GameController.Instance.OnSuccessfulKatanaHit();
        }
        else if (collision.collider.tag == "Katana")
        {
            rb.velocity = new Vector2(rb.velocity.x, -2);
            GameController.Instance.StartGameOverSequence(false);
        }
    }
}
