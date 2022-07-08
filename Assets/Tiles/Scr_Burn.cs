using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Burn : MonoBehaviour
{

   //public static float e = 2.7182818f;
   public GameObject flames;
   Scr_Fire flamesInfluence;
   
   //Temperatures in Kelvin
   public float temperature = 333f;
   public float ignitionPoint = 350f + 273f;
   public float ambientTemperature = 333f;
   public float burningTemperature = 600f + 273f;
   public float surroundingTemperature = 333f;
   
   public float conductivity = .001f; //between 0=forever and 1=instantaneous

   private Mesh initialMesh;
   public Mesh swapMesh;

   //fuel is a unit of time
   public int fuel = 1000;

   //size of flame
   public float intensity = 0.1f;
   public bool isBurning = false;

   private Scr_TileMap Base;

    // Start is called before the first frame update
   void Start()
    {
      initialMesh = this.gameObject.GetComponent<MeshFilter>().mesh;

      Base = (Scr_TileMap)GameObject.Find("Base").GetComponent(typeof(Scr_TileMap));
      var main = flames.GetComponent<ParticleSystem>().main;
      main.startSize = intensity;
      flames = Instantiate(flames, transform);

      flamesInfluence = (Scr_Fire)flames.GetComponent(typeof(Scr_Fire));
      //flamesInfluence.temperature = temperature;
      //flames.GetComponent<SphereCollider>().radius = 25f * intensity;
   }

    // Update is called once per frame
    void Update()
    {
      if (fuel <= 0 && isBurning)
         BurnOut();
      isBurning = (temperature > ignitionPoint && fuel > 0);

      GetSurroundingTemperature();
      AdjustTemperature();

      //flames.transform.gameObject.SetActive(isBurning);
      flamesInfluence.Activate(isBurning);

      flames.transform.position = transform.position;
      if (isBurning)
      {
         fuel--;
      }
      //flamesInfluence.temperature = temperature;
    }

   private void BurnOut()
   {
      this.gameObject.GetComponent<MeshFilter>().mesh = swapMesh;
   }

   private void GetSurroundingTemperature()
   {
      surroundingTemperature = ambientTemperature;
      float closestDistance = float.MaxValue;
      foreach (Scr_Fire heatSource in Base.heatSources)
      {
         if (!heatSource.IsActive()) continue;
         float distance = Vector3.Distance(this.transform.position, heatSource.transform.position);
         if (distance <= 0) continue;
         if (distance < closestDistance)
            closestDistance = distance;
         //Debug.Log(influence);
      }
      float influence = (burningTemperature - ambientTemperature) / (closestDistance * closestDistance * 2); // temperature/distance^3
      surroundingTemperature += influence;
   }

   private void AdjustTemperature()
   {
      //temperature = (surroundingTemperature * (temperature - surroundingTemperature) * Mathf.Pow(e, -coolingConstant));
      temperature += (surroundingTemperature - temperature) * conductivity;
      if (isBurning)
      {
         temperature += (burningTemperature - temperature) / 50;
      }
   }

}
