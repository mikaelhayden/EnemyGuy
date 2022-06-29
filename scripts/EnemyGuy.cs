using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Script para controlar o Enemy Guy
public class EnemyGuy : MonoBehaviour
{
    //vari�veis para controlar a:
    public float speed;     //velocidade
    public float walkTime;  //Tempo que ele anda
    public float timer;     //vari�ve para controlar o tempo

    public bool walkRight = true;   //vari�vel para saber se o guy est� na esquerda ou n�o

    public int health;      //guarda o n�vel de vida do enemyGuy
    public int d = 1;       //vari�vel d para controlar o dado que ele solta no player, quanto maior, mais o player perde vida

    private Rigidbody2D rig;
    //Vari�vel para trabalhar com o Rigdbody2D, vamos chamar ela, apenas quando o personagem tem RigidBody2D, pro 3D � a mesma coisa
    private Animator anim;
    //Vari�vel para trabalharmos com o Animation, chamar apenas quando o personagem tiver algum animation

    // Start � chamado antes do update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    //fixedUpdate � ideal para usar quando se utiliza o RigidBody
    //"A utiliza��o do FixedUpdate � recomendada quando queremos gerenciar principalmente componentes de f�sica,
    //como o Rigidbody, por exemplo, pois como ele � chamado numa taxa fixa e precisa, n�o teremos problemas de 
    //irregularidades em c�lculos da f�sica do jogo(como aplicar a for�a num objeto de formas inconstantes � cada quadro)"
    void FixedUpdate()
    {
        timer += Time.deltaTime;    //conta o tempo

        //quando o tempo for maior ou igual ao tempo limite que a gente estipuou pra ele andar, o enemy muda de dire��o
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
    //quando ele receber um hit, ele executa a nima��o de hit, descrece a vida do enemy(inimigo) e 
    //quando a vida dele for menor ou igual a 0, ele � destruido com o comando destroy

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
