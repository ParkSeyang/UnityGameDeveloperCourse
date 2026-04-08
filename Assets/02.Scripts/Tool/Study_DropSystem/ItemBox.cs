using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    public Transform[] itemSlots;
    private GameObject[] itemArray;
    
    // Start is called before the first frame update
    void Start()
    {
        itemArray = new GameObject[itemSlots.Length];
    }

    public void ClearBox()
    {
        for (int i = 0; i < itemArray.Length; i++)
        {
            if (itemArray[i] != null)
            {
                Destroy(itemArray[i].gameObject);
                itemArray[i] = null;
            }
        }
    }

    public void SetBox(GameObject[] items)
    {
        Queue<Transform> slotQueue = new Queue<Transform>();
        for (int i = 0; i < items.Length; i++)
        {
            slotQueue.Enqueue(itemSlots[i].transform);
        }
        
        for (int i = 0; i < items.Length; i++)
        {
            if(items[i] == null) continue;
            var slot = slotQueue.Dequeue();
            var item = items[i];
            
            item.transform.SetParent(slot);
            item.transform.localPosition = Vector3.zero;

            itemArray[i] = item;
        }
    }
}