using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class InventoryOpenerANDManager : NetworkBehaviour
{
    [Header("Keybinds")]
    [SerializeField] KeyCode CursorToggleKey = KeyCode.T;
    [SerializeField] KeyCode InventoryToggle = KeyCode.Tab;

    [Header("healthBar Stuff")]
    public Slider HealthBar;
    [Header("InventoryBar Stuff")]
    [SerializeField] GameObject Inventorybar;
    [SerializeField] Transform pos1;
    [SerializeField] Transform pos2;
    [SerializeField] bool IsVis = true;
    
    private bool IsLocked = false;

    public void disable()
    {
        HealthBar.gameObject.SetActive(true);
        OnToggleCursor();
    }
    public void enable()
    {
        HealthBar.gameObject.SetActive(false);
        Inventorybar.transform.position = pos2.position;

    }
    private void Start()
    {
        //Cursor.lockState=CursorLockMode.Locked;
        //NewStuff
        enable();
    }

    private void Update()
    {
        if (!IsOwner) return;
        
        if (Input.GetKeyDown(InventoryToggle))
        {
            OnToggleInventory();
        }
        if (Input.GetKeyDown(CursorToggleKey))
        {
            OnToggleCursor();
        }
    }
    public void OnToggleCursor()
    {
        IsLocked = !IsLocked;
        Cursor.lockState = IsLocked ? CursorLockMode.Locked : CursorLockMode.None;
    }
    public void OnToggleInventory()
    {
        IsVis = !IsVis;
        if (IsVis)
        {
            Inventorybar.transform.position = pos1.position;
        }
        else
        {
            Inventorybar.transform.position = pos2.position;
        }
    }
}