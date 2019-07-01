using UnityEngine;

public class PlayerController : MonoBehaviour {
  public float moveSpeed = 4f;
  public float jumpForce = 27.6f;
  public float voiceVolume = 1f;
  public AudioClip deathVoice;
  public AudioClip[] jumpVoices;

  private bool airborne = true;
  private bool dead = false;
  private Animator ani;
  private Rigidbody2D rb;
  private SpriteRenderer sr;
  private AudioSource asrc;
  private BoxCollider2D bc2d;

  public void Start() {
    ani = this.GetComponent<Animator>();
    rb = this.GetComponent<Rigidbody2D>();
    sr = this.GetComponent<SpriteRenderer>();
    asrc = this.GetComponent<AudioSource>();
    bc2d = this.GetComponent<BoxCollider2D>();
    ani.SetBool("airborne", airborne);
    ani.SetFloat("yvel", rb.velocity.y);
  }

  public void OnCollisionEnter2D(Collision2D c2d) {
    if (c2d.GetContact(0).point.y <= transform.position.y - bc2d.size.y/2f) {
      airborne = false;
      ani.SetBool("airborne", airborne);
      ani.SetFloat("yvel", rb.velocity.y);
    }
  }

  public void OnCollisionExit2D(Collision2D c2d) {
    if (c2d.collider.bounds.max.y <= transform.position.y - bc2d.size.y/2f) {
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

    // Suicide
    if (Input.GetButton("Cancel")) {
      dead = true;
      ani.SetBool("dead", dead);
      asrc.PlayOneShot(deathVoice, voiceVolume);
      return;
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
    if (ydir > 0 && !airborne) {
      rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
      airborne = true;
      ani.SetBool("airborne", airborne);
      int r = (int)(Random.value * jumpVoices.Length * 2);
      if (r < jumpVoices.Length) asrc.PlayOneShot(jumpVoices[r], voiceVolume);
    }
    ani.SetFloat("yvel", v.y);
  }
}
