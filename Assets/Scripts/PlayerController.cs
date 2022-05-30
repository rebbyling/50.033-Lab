using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public float maxSpeed;
    public float upSpeed;
    public Transform enemyLocation;
    public TextMeshProUGUI scoreText;
    
    private Rigidbody2D marioBody;
    private SpriteRenderer marioSprite;
    private int score = 0;
    private Animator marioAnimator;
    private AudioSource marioAudio;
    
    private bool onGroundState = true;
    private bool faceRightState = true;
    private bool countScoreState = false;

    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        marioBody = GetComponent<Rigidbody2D>();

        //instantiate mario sprite
        marioSprite = GetComponent<SpriteRenderer>();
        marioAnimator  =  GetComponent<Animator>();
        marioAudio = GetComponent<AudioSource>();
    }   

    // called when the cube hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground")) {
            onGroundState = true; //on ground
            marioAnimator.SetBool("onGround", onGroundState);
            countScoreState = false;
            scoreText.text = "Score: " + score.ToString();
        };

        if (col.gameObject.CompareTag("Brick")) {
            onGroundState = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Gomba!");
            marioBody.velocity = Vector2.zero;
            SceneManager.LoadScene("SampleScene");
        }
    }

    // Update is called once per frame
    void Update()
    {
    // toggle state
      if (Input.GetKeyDown("a") && faceRightState){
          faceRightState = false;
          marioSprite.flipX = true;
          if (Mathf.Abs(marioBody.velocity.x) >  1.0) 
	        marioAnimator.SetTrigger("onSkid");
      }

      if (Input.GetKeyDown("d") && !faceRightState){
          faceRightState = true;
          marioSprite.flipX = false;
          if (Mathf.Abs(marioBody.velocity.x) >  1.0) 
	        marioAnimator.SetTrigger("onSkid");
      }

      // when jumping, and Gomba is near Mario and we haven't registered our score
      if (!onGroundState && countScoreState)
      {
          if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
          {
              countScoreState = false;
              score++;
              Debug.Log(score);
          }
      }

      

    
    }

    void FixedUpdate()
    {
        marioAnimator.SetFloat("xSpeed", Mathf.Abs(marioBody.velocity.x));
        marioAnimator.SetBool("onGround", onGroundState);
        // dynamic rigidbody
      float moveHorizontal = Input.GetAxis("Horizontal");
      if (Mathf.Abs(moveHorizontal) > 0){
          Vector2 movement = new Vector2(moveHorizontal, 0);
          if (marioBody.velocity.magnitude < maxSpeed)
            marioBody.AddForce(movement * speed); 
      }
      if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
          // stop
          marioBody.velocity = Vector2.zero;
      }
      if (Input.GetKeyDown("space") && onGroundState){
          marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
          onGroundState = false;
          countScoreState = true;
          PlayJumpSound();
      }
    }

    void PlayJumpSound(){
	    marioAudio.PlayOneShot(marioAudio.clip);
    }
}
