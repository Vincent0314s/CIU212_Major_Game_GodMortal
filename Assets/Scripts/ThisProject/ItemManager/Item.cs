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

            switch (itemType) {
                case Items.HealthPotion:
                    AnalyticsManager.AddCollectedPotion();
                    break;
                case Items.Letter:
                    AnalyticsManager.AddCollectedLetter();
                    ItemManager.i.SetLetterText(description);
                    collision.gameObject.GetComponent<PlayerController>().ReadLetter();
                    break;
            }
            Destroy(this.gameObject);
        }
    }
}
