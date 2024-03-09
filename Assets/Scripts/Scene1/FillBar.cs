using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FillBar : MonoBehaviour
{
   // public Transform player;
    public Image fillBar;
    public TextMeshProUGUI valueText;
    public void UpdateBar(int currentHealth, int maxValue)
    {
        fillBar.fillAmount = (float)currentHealth / (float)maxValue;
        valueText.text = currentHealth.ToString() + " / " + maxValue.ToString();
        //if (player != null && fillBar != null)
        //{
           
        //    Vector3 playerPosition = player.position;

            
        //    Vector3 screenPosition = Camera.main.WorldToScreenPoint(playerPosition);

        //    fillBar.transform.position = screenPosition;
        //}
    }
}
