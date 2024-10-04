using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashManager : MonoBehaviour
{
    LayerMask _layersInteractedWith;

    public void SetAsChild(GameObject obj)
    {
        Debug.Log($"Setting {obj.name} as a child of {transform.name}");
        obj.transform.parent = transform;
        //Debug.Log("interacted");
    }
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}