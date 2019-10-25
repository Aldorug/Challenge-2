using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;
    private bool facingRight = true;
    public float speed;
    public Text score;
    public Text lives;
    public Text winText;
    public GameObject other;
    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioSource musicSource;
    Animator anim;
    private int livesValue = 3;
    private int scoreValue = 0;

   
    void winCondition()
    {
        if (scoreValue == 8)
        {
            winText.text = "You win! Game created by Colin Hummel!";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
       
        }


    }
    void gameOver()
    {
        if (livesValue == 0)
        {
            winText.text = "You lose! Game Created by Colin Hummel!";
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        score.text = "Score: " + scoreValue.ToString();
        lives.text = "Lives: " + livesValue.ToString();
        winText.text = "";
        musicSource.clip = musicClipOne;
        musicSource.Play();
      
        
    }

    // Update is called once per frame
    void Update()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));

        if (livesValue == 0)
        {
            Destroy(other);
        }

        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKey(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            anim.SetInteger("State", 2);
        }

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if(facingRight == true && hozMovement < 0)
        {
            Flip();
        }
       
        
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            other.gameObject.SetActive(false);
            winCondition();
        }
        else if (other.gameObject.CompareTag("Enemy"))
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            other.gameObject.SetActive(false);
            gameOver();
        }
        if(scoreValue == 4)
        {
            transform.position = new Vector2(54f, 0f);
            if(livesValue == 2)
            {
                livesValue += 1;
                lives.text = "Lives: " + livesValue.ToString();
            }
            if(livesValue == 1)
            {
                livesValue += 2;
                lives.text = "Lives: " + livesValue.ToString();
            }
        }
       
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            anim.SetInteger("State", 0);
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }


    }
}