using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script para controlar o Enemy Guy
public class EnemyGuy : MonoBehaviour
{
    //variáveis para controlar a:
    public float speed;     //velocidade
    public float walkTime;  //Tempo que ele anda
    public float timer;     //variáve para controlar o tempo

    public bool walkRight = true;   //variável para saber se o guy está na esquerda ou não

    public int health;      //guarda o nível de vida do enemyGuy
    public int d = 1;       //variável d para controlar o dado que ele solta no player, quanto maior, mais o player perde vida

    private Rigidbody2D rig;
    //Variável para trabalhar com o Rigdbody2D, vamos chamar ela, apenas quando o personagem tem RigidBody2D, pro 3D é a mesma coisa
    private Animator anim;
    //Variável para trabalharmos com o Animation, chamar apenas quando o personagem tiver algum animation

    // Start é chamado antes do update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    //fixedUpdate é ideal para usar quando se utiliza o RigidBody
    //"A utilização do FixedUpdate é recomendada quando queremos gerenciar principalmente componentes de física,
    //como o Rigidbody, por exemplo, pois como ele é chamado numa taxa fixa e precisa, não teremos problemas de 
    //irregularidades em cálculos da física do jogo(como aplicar a força num objeto de formas inconstantes à cada quadro)"
    void FixedUpdate()
    {
        timer += Time.deltaTime;    //conta o tempo

        //quando o tempo for maior ou igual ao tempo limite que a gente estipuou pra ele andar, o enemy muda de direção
        if(timer >= walkTime)  
        {
            walkRight = !walkRight;
            timer = 0f;
        }

        if(walkRight)
        {
            transform.eulerAngles = new Vector2(0, 180);
            rig.velocity = Vector2.right * speed;
        }

        else
        {
            transform.eulerAngles = new Vector2(0, 0);
            rig.velocity = Vector2.left * speed;
        }
       
    }

    //hit e morte do guy
    //quando ele receber um hit, ele executa a nimação de hit, descrece a vida do enemy(inimigo) e 
    //quando a vida dele for menor ou igual a 0, ele é destruido com o comando destroy

    public void Damage(int dmg)
    {
        health = health - dmg;
        anim.SetTrigger("hit");

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //um oncollisionenter para sabe se  o enemy colidiu com o player
    //se ele colidir, o play perde vida
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Player>().damage(d);
        }
    }
}
