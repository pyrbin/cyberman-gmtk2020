using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public List<LevelGenerator> levels;

    private int currentLevelIndex = 0;

    void Awake()
    {
        levels[currentLevelIndex].gameObject.SetActive(true);
    }

    public void NextLevel()
    {
        print("changing levels");
        GameObject.FindGameObjectWithTag("Player").gameObject.transform.localPosition = new float3(0f, 0f, 0f);
        levels[currentLevelIndex].gameObject.SetActive(false);
        currentLevelIndex++;
        if (currentLevelIndex > levels.Count)
        {
            return; // We Win
        }
        levels[currentLevelIndex].gameObject.SetActive(true);
    }

}
