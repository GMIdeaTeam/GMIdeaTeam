using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public bool isActivated = false;

    [SerializeField]
    private GameObject InventoryBase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TryOpenInventory();
    }

    void TryOpenInventory()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            isActivated = !isActivated;
        }
        if (isActivated)
            OpenInventory();
        else
            CloseInventory();
    }

    private void OpenInventory()
    {
        InventoryBase.SetActive(true);
    }

    private void CloseInventory()
    {
        InventoryBase.SetActive(false);
    }

}
