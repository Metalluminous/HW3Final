using System;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;


public class nappi2 : MonoBehaviour
{

    private bool _isPressed;
    public GameObject Canvas1;
    public GameObject Canvas2;
    public GameObject Canvas3;
    public GameObject Canvas4;
    public GameObject tuhottava;

    public float aika = 1;
    public float aika2 = 6000;
    float ajastin;
    public static float ajastin2;

    public GameObject vipu;
    static public float kulma;


    private float x;
    private GameObject uusiVihollinen;
    
    public GameObject vihollinen1;
    public Rigidbody palikka;
    public TMP_Text mission;

    private float pituus;

    
    void Start()
    { 
        Canvas2.SetActive(false);
        Canvas3.SetActive(false);
        Canvas4.SetActive(false);
        scoring.pointAmount = 10;
    }

    void Update()
    {

        kulma = vipu.GetComponent<HingeJoint>().angle;
        
        if (kulma >5)
            Pressed();

        if (kulma <(-5))
            Released();


    

        
    }


    private void Pressed()
    {   
        
        Canvas1.SetActive(false);
        Canvas2.SetActive(false);
        Canvas3.SetActive(false);
        Canvas4.SetActive(false);

        ajastin += Time.deltaTime;
        
        if (ajastin >=aika)
        {
            
            
            Vector3 randomSijainti = new Vector3
            (
                UnityEngine.Random.Range(-70,70),
                UnityEngine.Random.Range(6,8),
                UnityEngine.Random.Range(-9, 46)
            );
            GameObject uusiVihollinen = Instantiate(vihollinen1, randomSijainti, UnityEngine.Random.rotation);

            ajastin -= aika;
        }
        
        ajastin2 += Time.deltaTime;


        if ((ajastin2>60)&&(scoring.pointAmount>30))
        {
            Canvas2.SetActive(true);
            
            ajastin = 0;
            Destroy(uusiVihollinen);
        }

        if ((ajastin2>60)&&(scoring.pointAmount is >0 and <30))
        {
            
            
            ajastin = 0;
            Canvas4.SetActive(true);
            Destroy(uusiVihollinen);
        }

        if ((scoring.pointAmount<0))
        {
            
            
            ajastin = 0;
            Canvas3.SetActive(true);
            Destroy(uusiVihollinen);
        }
        
    }

   

    private void Released()
    {
        
        ajastin2 = 0;
        ajastin = 0;
        scoring.pointAmount = 10;
        Canvas2.SetActive(false);
        Canvas3.SetActive(false);
        Canvas4.SetActive(false);
        Canvas1.SetActive(true);
        

    

    }
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
}
