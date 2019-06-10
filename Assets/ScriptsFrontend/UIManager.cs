using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private GameObject heldChip;

    void Update()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        newPos.z = 0;
        transform.position = newPos;

        if (Input.GetMouseButtonUp(0) && heldChip != null)
        {
            heldChip.transform.parent = null;
        }
    }

    public void SpawnChip(GameObject chipPreset)
    {
        heldChip = Instantiate(chipPreset, transform);
    }
}
