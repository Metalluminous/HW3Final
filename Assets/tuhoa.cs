using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;




public class tuhoa : MonoBehaviour
{
   private float x;
   public Rigidbody luoti;

   //public static GameObject vihollinen2;
   public GameObject vihollinen;
   
  
   Vector3 loppupaikka = new Vector3(0.7294f, 4.201f, -3.48f);

   private float pituus;

   public float speed;

  
   void Update()
   {
      speed = 5;
      var liike =  speed * Time.deltaTime;

      Vector3 uusLoppuPaikka = Vector3.MoveTowards(vihollinen.transform.localPosition, loppupaikka, liike);
      vihollinen.transform.position = uusLoppuPaikka;
      
      if ((nappi2.ajastin2>60) | scoring.pointAmount<0 )
      {
         Destroy(vihollinen);
      }

   }

   
   private void OnTriggerEnter(Collider other)
   {
      bool juttu1 = other.gameObject.CompareTag("luoti");

      if (juttu1 == true)
      {
      
         scoring.pointAmount = (scoring.pointAmount) + 3;
         
         Destroy(vihollinen);
      }

      bool juttu2 = other.gameObject.CompareTag("talo");

      if (juttu2 == true)
      {
      
         scoring.pointAmount = (scoring.pointAmount)-2;
         
         Destroy(vihollinen);
      }

      if (nappi2.kulma>(-10))
      {
         
         Destroy(vihollinen);
      }
      

         
   }
}
