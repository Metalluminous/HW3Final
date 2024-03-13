using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class scoring : MonoBehaviour
{   
    public TMP_Text pisteet;
    
    public static int pointAmount;

    void Start()
    {
        
    }

    
    void Update()
    {
        pisteet.text = "POINTS"+ pointAmount.ToString();
    }
}
