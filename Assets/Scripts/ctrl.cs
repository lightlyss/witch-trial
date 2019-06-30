using UnityEngine;

public class Ctrl : MonoBehaviour {
  public float moveSpeed = 4f;
  public float jumpForce = 27.6f;
  public float abyssHeight = -100f;

  private bool airborne = true;
  private bool dead = false;
  private Animator ani;
  private Rigidbody2D rb;
  private SpriteRenderer sr;

  public void Start() {
    ani = this.GetComponent<Animator>();
    rb = this.GetComponent<Rigidbody2D>();
    sr = this.GetComponent<SpriteRenderer>();
    ani.SetBool("airborne", airborne);
    ani.SetFloat("yvel", rb.velocity.y);
  }

  public void OnCollisionEnter2D(Collision2D c2d) {
    if (c2d.gameObject.GetComponent(typeof(PlatformEffector2D)) != null) {
      airborne = false;
      ani.SetBool("airborne", airborne);
      ani.SetFloat("yvel", rb.velocity.y);
    }
  }

  public void OnCollisionExit2D(Collision2D c2d) {
    if (c2d.gameObject.GetComponent(typeof(PlatformEffector2D)) != null) {
      airborne = true;
      ani.SetBool("airborne", airborne);
      ani.SetFloat("yvel", rb.velocity.y);
    }
  }

  public void Update() {
    if (dead) return;

    var xdir = Input.GetAxisRaw("Horizontal");
    var ydir = Input.GetAxisRaw("Vertical");
    var v = rb.velocity;

    // Quit on death
    if (Input.GetButton("Cancel") || rb.position.y < abyssHeight) {
      dead = true;
      ani.SetBool("dead", dead);
    }

    // X axis motion
    if (xdir > 0) {
      sr.flipX = false;
      rb.velocity = new Vector2(moveSpeed, v.y);
    } else if (xdir < 0) {
      sr.flipX = true;
      rb.velocity = new Vector2(-moveSpeed, v.y);
    } else rb.velocity = new Vector2(0, v.y);
    ani.SetInteger("xdir", (int) xdir);

    // Jumping
    ani.SetFloat("yvel", v.y);
    if (ydir > 0 && !airborne) {
      rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
      airborne = true;
      ani.SetBool("airborne", airborne);
    }
  }
}
