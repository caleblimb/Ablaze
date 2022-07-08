using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Fire : MonoBehaviour
{
   private Scr_TileMap scr;
   //public float temperature = 0;
   public bool active;
    // Start is called before the first frame update
    void Start()
    {
      scr = (Scr_TileMap)GameObject.Find("Base").GetComponent(typeof(Scr_TileMap));
      scr.AddHeatSource(this);
   }

   public void Activate(bool active)
   {
      Debug.Log(active);
      transform.gameObject.SetActive(active);
      this.active = active;
   }

   public bool IsActive()
   {
      return active;
   }
    // Update is called once per frame
    void Update()
    {
        
    }
}
