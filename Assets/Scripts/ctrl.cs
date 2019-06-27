using UnityEngine;

public class ctrl : MonoBehaviour {
  private Animator ani;
  private Rigidbody2D rb;
  private SpriteRenderer sr;

  private bool airborne = false;

  void Start() {
    ani = this.GetComponent<Animator>();
    rb = this.GetComponent<Rigidbody2D>();
    sr = this.GetComponent<SpriteRenderer>();
  }

  void OnCollisionEnter2D(Collision2D c2d) {
    if (c2d.gameObject.GetComponent(typeof(PlatformEffector2D)) != null) {
      airborne = false;
    }
  }

  void Update() {
    var xdir = Input.GetAxisRaw("Horizontal");
    var ydir = Input.GetAxisRaw("Vertical");
    var v = rb.velocity;

    if (xdir > 0) {
      ani.SetInteger("xdir", 1);
      sr.flipX = false;
      rb.velocity = new Vector2(5, v.y);
    } else if (xdir < 0) {
      ani.SetInteger("xdir", -1);
      sr.flipX = true;
      rb.velocity = new Vector2(-5, v.y);
    } else {
      ani.SetInteger("xdir", 0);
      rb.velocity = new Vector2(0, v.y);
    }

    if (ydir > 0 && !airborne) {
      rb.AddForce(new Vector2(0, 5.5f), ForceMode2D.Impulse);
      airborne = true;
    }
  }
}
