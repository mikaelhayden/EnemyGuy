using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    private Transform player;
    public float smooth;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per fram
    void LateUpdate()
    {
        if(player.position.x >= -1)
        {
            Vector3 following = new Vector3(player.position.x, transform.position.y, transform.position.z);

            if (player.position.y >= 1)
            {
                following = new Vector3(player.position.x, player.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, following, smooth * Time.deltaTime);
            }
            else if(player.position.y < 1)
            {
                following = new Vector3(player.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.Lerp(transform.position, following, smooth * Time.deltaTime);
            }

            transform.position = Vector3.Lerp(transform.position, following, smooth * Time.deltaTime);
        }
        
    }
}
