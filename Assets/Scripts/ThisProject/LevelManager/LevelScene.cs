using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScene : MonoBehaviour
{
    public bool canBeActivedInTheBeginning;
    void Awake()
    {
        gameObject.SetActive(canBeActivedInTheBeginning);

    }

    void Update()
    {
        //Should do this with the delegate
        //gameObject.SetActive(canBeActived);
    }
}
