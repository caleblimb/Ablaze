using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Arrows : MonoBehaviour
{

   public float thrust = 1.0f;
   public Rigidbody rb;
   // Start is called before the first frame update
   void Start()
    {
      rb = GetComponent<Rigidbody>();
   }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKey(KeyCode.UpArrow))
      {
         rb.AddForce(0, 0, thrust, ForceMode.Impulse);
      }
        if (Input.GetKey(KeyCode.DownArrow))
      {
         rb.AddForce(0, 0, -thrust, ForceMode.Impulse);
      }
      if (Input.GetKey(KeyCode.LeftArrow))
      {
         rb.AddForce(-thrust, 0, 0, ForceMode.Impulse);
      }
      if (Input.GetKey(KeyCode.RightArrow))
      {
         rb.AddForce(thrust, 0, 0, ForceMode.Impulse);
      }
   }

   
}
