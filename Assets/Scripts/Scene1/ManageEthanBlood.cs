using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageEthanBlood : MonoBehaviour
{
    public static ManageEthanBlood instance;

    public int scene1_CurrentHealth;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
