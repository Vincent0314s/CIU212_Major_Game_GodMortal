using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineGrowEffect : MonoBehaviour
{
    private MeshRenderer[] growMaterials;
    public float timeToGrow = 2f;
    public float updateRate = 0.05f;

    private void OnEnable()
    {

        growMaterials = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < growMaterials.Length; i++)
        {
            growMaterials[i].material.SetFloat("_Grow",-1);
            StartCoroutine(GrowVine(growMaterials[i].material));

        }
    }

    IEnumerator GrowVine(Material _m) {
        float growValue = _m.GetFloat("_Grow");

        while (growValue < 1) {
            growValue += 1 / (timeToGrow/updateRate);
            _m.SetFloat("_Grow",growValue);

            yield return new WaitForSeconds(updateRate);
        }

        if (growValue >=1) {
            gameObject.SetActive(false);
        }
    }
}
