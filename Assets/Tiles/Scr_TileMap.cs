using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct Tilemap
{
   public int width;
   public int height;
   public Vector3 origin;
   public GameObject[,] tiles;
}

internal enum Direction
{
   north,
   east,
   south,
   west
}

public class Scr_TileMap : MonoBehaviour
{
   private static int cameraHeight = 5;



   private GameObject[] selection;
   public GameObject selector;
   public GameObject[,] maptiles;
   public Tilemap map;
   public List<Scr_Fire> heatSources;
   public void AddHeatSource(Scr_Fire scr_Fire)
   {
      heatSources.Add(scr_Fire);
   }
    // Start is called before the first frame update
    void Start()
    {
      selection = ((lvl_1)GameObject.Find("Base").GetComponent(typeof(lvl_1))).selection;
      map.width = (int)transform.localScale.x;
      map.height = (int)transform.localScale.z;

      map.tiles = new GameObject[map.width, map.height];
      map.origin = transform.position - new Vector3(transform.localScale.x / 2, 0 - transform.localScale.y / 2, transform.localScale.z / 2);

      FillRandom();
   }

   private void FillRandom()
   {
      for (int y = 0; y < map.height; y++)
      {
         for (int x = 0; x < map.width; x++)
         {
            if (x == 0 && y == 0)
               map.tiles[0,0] = selector;
            else
            {
               map.tiles[x,y] = InstantiateTile(x, y);
            }
         }
      }
   }
   
   private GameObject InstantiateTile(int indexX, int indexY)
   {
      GameObject newTile = selection[((indexY * map.width) + indexX) % selection.Length];
      Vector3 pos = new Vector3((float)indexX + newTile.transform.localScale.x / 2, 0f, (float)indexY + newTile.transform.localScale.z / 2);
      return (GameObject)Instantiate(newTile, map.origin + pos, new Quaternion());
   }

   public void SlideTile(Vector3 pos)
   {
      //Debug.Log("X:" + pos.x + ", Z:" + pos.z);
      //int index = ((int)(pos.z - map.origin.z) * map.width) + (int)(pos.x - map.origin.x);
      int x = (int)(pos.x - map.origin.x);
      int y = (int)(pos.z - map.origin.z);
      //Debug.Log("X:" + x + ", Y:" + y);

      for (Direction dir = Direction.north; dir <= Direction.west; dir++)
      {
         switch (dir)
         {
            case Direction.north:
               for (int i = map.height - y; i >= 0; i--)
                  UpdatePosition(x, y + i, x, y + i + 1);
               break;
            case Direction.east:
               for (int i = map.width - x; i >= 0; i--)
                  UpdatePosition(x + i, y, x + i + 1, y);
               break;
            case Direction.south:
               for (int i = y; i >= 0; i--)
                  UpdatePosition(x, y - i, x, y - i - 1);
               break;
            case Direction.west:
               for (int i = x; i >= 0; i--)
                  UpdatePosition(x - i, y, x - i - 1, y);
               break;
         }
      }
   }

   private void UpdatePosition(int x, int y, int newX, int newY)
   {
      try
      {
         if (map.tiles[newX,newY] == selector)
         {
            Vector3 oldPos = map.tiles[x, y].transform.position;
            map.tiles[newX,newY] = map.tiles[x,y];
            map.tiles[x,y] = selector;

            Vector3 newPos = new Vector3(
               newX + map.origin.x + (map.tiles[newX, newY].transform.localScale.x / 2),
               map.origin.y,
               newY + map.origin.z + (map.tiles[newX, newY].transform.localScale.z / 2));

            iTween.MoveTo(map.tiles[newX, newY],
               newPos,
               0.5f); ;

            //Debug.Log(newPos.x - oldPos.x);
            //Debug.Log(newPos.y - oldPos.y);
            //Debug.Log(newPos.z - oldPos.z);

            Scr_Tile scr = (Scr_Tile)map.tiles[newX, newY].GetComponent(typeof(Scr_Tile));
            scr.UpdateEnitiesPosion(newPos - oldPos);
            //UpdateCamera(x, y);
           //checkMatches();
         }
      }
      catch (System.IndexOutOfRangeException) { }
   }

   private void UpdateCamera(int inedexX, int indexY)
   {
      iTween.MoveTo(GameObject.Find("Camera"),
   new Vector3(
   inedexX + map.origin.x + (selector.transform.localScale.x / 2),
   cameraHeight,
   indexY + map.origin.z + (selector.transform.localScale.z / 2)),
   5f);

   }

   // Update is called once per frame
   void Update()
    {
        
    }

}
