using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script para controlar os corações que ficam no game, que o player usa para crescer a vida
public class ItenHeart : MonoBehaviour
{
    public int healthValue; //valor da vida, de quantos em quantos a vida vai crescer

    private AudioSource sound;  //variável para o som

    void Awake()
    {
        sound = GetComponent<AudioSource>();        // chamando o som
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")    //if colide com o player
        {
            sound.Play();       //tocando o som efeito sonoro
            collision.gameObject.GetComponent<Player>().increaseHealth(healthValue);
            //chama a função increaseHealth para adicionar vidas ao personagem
            Destroy(gameObject, 0.2f);        //destroy o objeto heart(coração)
        }
    }
}
