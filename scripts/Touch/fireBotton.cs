using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireBotton : MonoBehaviour
{
    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void firebow()
    {
        player.touchFire = true;
    }
}
