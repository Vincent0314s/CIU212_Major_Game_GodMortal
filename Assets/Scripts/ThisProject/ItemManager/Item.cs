using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    public Items itemType;
    [TextArea(5,10)]
    public string description;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player") {
            ItemManager.i.AddItems(itemType);
            if (itemType == Items.Letter) {
                ItemManager.i.SetLetterText(description);
                collision.gameObject.GetComponent<PlayerController>().ReadLetter();
            }
            Destroy(this.gameObject);
        }
    }
}
