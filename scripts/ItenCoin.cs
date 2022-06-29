using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItenCoin : MonoBehaviour
{
    public int scoreCoin;

    private AudioSource sound;

    void Awake()
    {
        sound = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            sound.Play();
            gameController.instance.UpdateScore(scoreCoin);
            Destroy(gameObject, 0.1f);
        }
    }


}
