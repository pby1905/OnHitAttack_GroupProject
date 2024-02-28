using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BloodBar : MonoBehaviour
{
    public UnityEngine.UI.Image _thanhmau;
    public void UpdateBloodBar(float bloodpre, float maxblood)
    {
        _thanhmau.fillAmount = bloodpre / maxblood;
    }
}
