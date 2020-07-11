using UnityEngine;


public class AddGaps : MapFunction
{

    public float seed;
    public int minWidth;
    public int maxWidth;
    public float chanceOfGap;

    public override void Apply(ref int[,] map, float seed)
    {
        bool? gap = null;

        int sectionWidth = 0;
        int currentGapSize = 0;

        for (int x = 0; x < map.GetUpperBound(0); x++)
        {
            sectionWidth++;
            if (sectionWidth >= minWidth || gap == null)
            {
                float r = Random.Range(0f, 1f);
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
    }
}