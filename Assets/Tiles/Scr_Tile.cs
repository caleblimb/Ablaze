using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Tile : MonoBehaviour
{
   private Scr_TileMap scr;
   public List<GameObject> surfaceEntities;
    // Start is called before the first frame update
    void Start()
    {
        scr = (Scr_TileMap)GameObject.Find("Base").GetComponent(typeof(Scr_TileMap));
    }

    // Update is called once per frame
    void Update()
    {

      if (Input.GetMouseButtonDown(0)) // Click on left mouse button
      {
         RaycastHit hit;
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

         if (Physics.Raycast(ray, out hit))
         {
            scr.SlideTile(hit.transform.position);
         }
      }
   }

   public void UpdateEnitiesPosion(Vector3 position)
   {
      foreach (GameObject entity in surfaceEntities)
      {
         iTween.MoveTo(entity, entity.transform.position + position, 0.5f);
      }
   }

   void OnCollisionEnter(Collision collision)
   {
      surfaceEntities.Add(collision.gameObject);
   }

   void OnCollisionExit(Collision collision)
   {
      surfaceEntities.Remove(collision.gameObject);
   }
}
