using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class MapFunctions
{
    public static int[,] GenerateArray(int width, int height, bool empty)
    {
        int[,] map = new int[width, height];
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            for (int y = 0; y < map.GetUpperBound(1); y++)
            {
                if (empty)
                {
                    map[x, y] = 0;
                }
                else
                {
                    map[x, y] = 1;
                }
            }
        }

        return map;
    }

    /// <summary>
    /// Draws the map to the screen
    /// </summary>
    /// <param name="map">Map that we want to draw</param>
    /// <param name="tilemap">Tilemap we will draw onto</param>
    /// <param name="tile">Tile we will draw with</param>
    public static void RenderMap(int[,] map, Tilemap tilemap, TileBase tile)
    {
        tilemap.ClearAllTiles(); //Clear the map (ensures we dont overlap)
        for (int x = 0; x < map.GetUpperBound(0); x++) //Loop through the width of the map
        {
            for (int y = 0; y < map.GetUpperBound(1); y++) //Loop through the height of the map
            {
                if (map[x, y] == 1) // 1 = tile, 0 = no tile
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), tile);
                }
            }
        }
    }


    /// <summary>
    /// Creates a perlin noise int array for the top layer of a level
    /// </summary>
    /// <param name="width">Width of the array</param>
    /// <param name="height">Height of the array</param>
    /// <param name="seed">Seed used with perlin function</param>
    /// <returns>An array of ints generated through perlin noise</returns>
    public static int[,] PerlinNoise(int[,] map, float seed)
    {
        int newPoint;
        //Used to reduced the position of the perlin point
        float reduction = 0.5f;
        //Create the perlin
        for (int x = 0; x < map.GetUpperBound(0); x++)
        {

            newPoint = Mathf.FloorToInt((Mathf.PerlinNoise(x, seed) - reduction) * map.GetUpperBound(1));

            //Make sure the noise starts near the halfway point of the height
            newPoint += (map.GetUpperBound(1) / 2);
            for (int y = newPoint; y >= 0; y--)
            {
                map[x, y] = 1;
            }
        }
        return map;
    }



    public static int[,] AddGaps(int[,] map, float seed, int minWidth, int maxWidth, float chanceOfGap)
    {
        bool? gap = null;

        int sectionWidth = 0;
        int currentGapSize = 0;

        for (int x = 0; x < map.GetUpperBound(0); x++)
        {


            // 

            sectionWidth++;
            if (sectionWidth >= minWidth || gap == null)
            {
                float r = Random.Range(0f, 1f);
                Debug.Log(r);
                if (r < chanceOfGap)
                    gap = true;
                else
                    gap = false;
                sectionWidth = 0;
            }

            if (gap == true && currentGapSize < maxWidth)
            {
                for (int y = map.GetUpperBound(1); y >= 0; y--)
                {
                    map[x, y] = 0;
                }
                currentGapSize++;
            }
            else
            {
                currentGapSize = 0;
            }
        }

        return map;
    }



    /// <summary>
    /// Generates a smoothed random walk top.
    /// </summary>
    /// <param name="map">Map to modify</param>
    /// <param name="seed">The seed for the random</param>
    /// <param name="minSectionWidth">The minimum width of the current height to have before changing the height</param>
    /// <returns>The modified map with a smoothed random walk</returns>
    public static int[,] RandomWalkTopSmoothed(int[,] map, float seed, int minSectionWidth)
    {
        //Seed our random
        System.Random rand = new System.Random(seed.GetHashCode());

        //Determine the start position
        int lastHeight = Random.Range(0, map.GetUpperBound(1));

        //Used to determine which direction to go
        int nextMove = 0;
        //Used to keep track of the current sections width
        int sectionWidth = 0;

        //Work through the array width
        for (int x = 0; x <= map.GetUpperBound(0); x++)
        {
            //Determine the next move
            nextMove = rand.Next(2);

            //Only change the height if we have used the current height more than the minimum required section width
            if (nextMove == 0 && lastHeight > 0 && sectionWidth > minSectionWidth)
            {
                lastHeight--;
                sectionWidth = 0;
            }
            else if (nextMove == 1 && lastHeight < map.GetUpperBound(1) && sectionWidth > minSectionWidth)
            {
                lastHeight++;
                sectionWidth = 0;
            }
            //Increment the section width
            sectionWidth++;

            //Work our way from the height down to 0
            for (int y = lastHeight; y >= 0; y--)
            {
                map[x, y] = 1;
            }
        }

        //Return the modified map
        return map;
    }


}
