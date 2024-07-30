using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankIcon : MonoBehaviour
{
    public List<GameObject> RankIconList;
    public Transform parentContainer;
    private GameObject currentPrefabInstance;


    // Update is called once per frame
    public void SetRankIcon(int index)
    {
        if (index >= 0 && index < RankIconList.Count)
        {
            if (currentPrefabInstance != null)
            {
                Destroy(currentPrefabInstance);
            }
            currentPrefabInstance = Instantiate(RankIconList[index], parentContainer);
        }
        else
        {
            Debug.LogWarning("Invalid index: " + index);
        }
    }
}
