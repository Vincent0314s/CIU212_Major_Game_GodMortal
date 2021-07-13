using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public Items itemType;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player") {
            ItemManager.i.AddItems(itemType);
            if (itemType == Items.Letter) {
                collision.gameObject.GetComponent<PlayerController>().ReadLetter();
            }
            Destroy(this.gameObject);
        }
    }
}
