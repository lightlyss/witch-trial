using UnityEngine;

public class ctrl : MonoBehaviour {
  private Animator ani;
  private Rigidbody2D rb;
  private SpriteRenderer sr;

  void Start() {
    ani = this.GetComponent<Animator>();
    rb = this.GetComponent<Rigidbody2D>();
    sr = this.GetComponent<SpriteRenderer>();
  }

  void Update() {
    var xdir = Input.GetAxisRaw("Horizontal");
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
  }
}
