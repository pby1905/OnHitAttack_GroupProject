using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DataPlayer 
{
    /*public int level;*/
    public float health;
    /*public float[] position;
*/

    public DataPlayer(PlayerHealth player)
    {
        health = player.slider.value;

        /*position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;*/
    }
}
