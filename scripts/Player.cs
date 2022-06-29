using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //velocidade, vida e força de pulo do personagem
    public int health;
    public int flechas;
    public int d = 1;
    private int scene;

    public float speed;
    public float jumpForce;
    public float movement;

    //objeto flecha que irá sair do arco e ponto de onde essa flecha vai sair
    public GameObject Bow;
    public GameObject jogador;
    public GameObject mobile;
    public GameObject mobile2;
    public GameObject mobile3;
    public GameObject mobile4;
    public Transform Point;
    public Transform detectGround;

    public LayerMask ground;

    //variáveis boleeana para saber uma determinada condição
    public bool isGround;
    private bool doubleJump;
    private bool isFire;

    public bool isMobile;
    public bool touchJump;
    public bool touchFire;

    private Rigidbody2D rig;
    private Animator anim;

    private AudioSource[] sounds;
    private AudioSource noise1;


    


    // Start is called before the first frame update
    void Start()
    {
        flechas = 10;
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sounds = GetComponents<AudioSource>();
        noise1 = sounds[0];

        //DontDestroyOnLoad(gameObject);
        health = PlayerPrefs.GetInt("lifes");
        gameController.instance.UpdateLives(health);
        scene = SceneManager.GetActiveScene().buildIndex;

        if (isMobile == false)
        {
            mobile.SetActive(false);
            mobile2.SetActive(false);
            mobile3.SetActive(false);
            mobile4.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        fire();
        jump();

    }
    void FixedUpdate()
    {
        move();
    }


    //função para fazer a movimentação do personagem(Direita e esquerda)
    void move()
    {

        if(isMobile == false)
        {
            movement = Input.GetAxis("Horizontal");
        }

        //adiciona velocidade ao player  no eixo x e y;
        rig.velocity = new Vector2(movement * speed, rig.velocity.y);

        if(movement == 0 && isGround == true && isFire == false)
        {
            anim.SetInteger("Transition", 0);
        }

        //andando pra direita
        if (movement > 0)
        {
            if(isGround == true && isFire == false)
            {
                anim.SetInteger("Transition", 1);
            }
            
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        //andando pra esquerda
        if (movement < 0)
        {
            if (isGround == true && isFire == false)
            {
                anim.SetInteger("Transition", 1);
            }

            transform.eulerAngles = new Vector3(0, 180, 0);
        }
    }

    //Função para fazer o player pular
    void jump()
    {
        isGround = Physics2D.OverlapCircle(detectGround.position, 0.2f, ground); //detecta se player está no chão

        if (Input.GetButtonDown("Jump") || touchJump == true)
        {
            touchJump = false;  //touchJump variável para a versão mobile
            if(isGround == true)
            {
                anim.SetInteger("Transition", 2);
                rig.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                doubleJump = true;
            }

            else
            {
                if(doubleJump == true)
                {
                    anim.SetInteger("Transition", 2);
                    rig.AddForce(new Vector2(0, jumpForce*0.5f), ForceMode2D.Impulse);
                    doubleJump = false;
                }
            }
        }
    }
    
    //função para fazer o player atirar flechas, chamando a corrotina
    void fire()
    {
        StartCoroutine("bow");
    }

    //corrotita para fazer o player atirar flechas
    IEnumerator bow()
    {
        if(Input.GetKeyDown(KeyCode.E) || touchFire == true)
        {
            
            flechas--;  //contador de flechas
            gameController.instance.UpdateBow(flechas);
            
            touchFire = false;
            isFire = true;
            anim.SetInteger("Transition", 3);

            if (flechas >=1)  //limite de flechas do personagem é 10 em cada fase;
            {
                noise1.Play();
                GameObject bow = Instantiate(Bow, Point.position, Point.rotation);

                if (transform.rotation.y == 0)
                {
                    bow.GetComponent<Bow>().isRight = true;
                }

                if (transform.rotation.y == 180)
                {
                    bow.GetComponent<Bow>().isRight = false;
                }
            }
            else
            {
                flechas = 1;
            }

            yield return new WaitForSeconds(0.2f);
            isFire = false;
            anim.SetInteger("Transition", 0);
            
        }
    }

    //função pro personagem sofrer dano
    //o personagem sofre dano: descrece a hearth e executa a animação hit

    public void damage(int dmg)
    {
        health = health - dmg;
        gameController.instance.UpdateLives(health);
        anim.SetTrigger("hit");

        if(transform.rotation.y == 0)
        {
            transform.position += new Vector3(-1f, 0, 0); //joga o personagem pra longe ao sofrer o dano(esquerda)
        }

        if(transform.rotation.y == 180)
        {
            transform.position += new Vector3(1f, 0, 0);  //joga o personagem pra longe ao sofrer o dano(direita)
        }

        if(health <= 0)
        {
            PlayerPrefs.SetInt("scre", 0);
            PlayerPrefs.SetInt("lifes", 1);
            jogador.SetActive(false);
            gameController.instance.gameOver();
            //chamar o game over pois o personagem morreu, perdeu todas as vidas
        }
    }

    public void increaseHealth(int val) //função para crescer a vida ao comer um coração
    {
        health = health + val;
        gameController.instance.UpdateLives(health);
    }

    void OnCollisionEnter2D(Collision2D coll)   //função colision
    {
        if (coll.gameObject.layer == 9) //chama o game over se o player cair
        {
            health = health - d;
            gameController.instance.UpdateLives(health);

            if (health <= 0)
            {
                PlayerPrefs.SetInt("scre", 0);
                PlayerPrefs.SetInt("lifes", 1);
                jogador.SetActive(false);
                gameController.instance.gameOver();
                //chamar o game over pois o personagem morreu, perdeu todas as vidas
            }
            else
            {
                SceneManager.LoadScene(scene);
            }

        }

        if (coll.gameObject.layer == 10)        //passa o player de fase
        {
            SceneManager.LoadScene(2);
        }

        if (coll.gameObject.layer == 11)        //passa o player de fase
        {
            SceneManager.LoadScene(3);
        }

        if (coll.gameObject.layer == 12)        //passa o player de fase
        {
            gameController.instance.endScreen();
            jogador.SetActive(false);
            mobile.SetActive(false);
        }
    }
}
