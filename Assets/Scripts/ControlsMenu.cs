using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ControlsMenu : MonoBehaviour
{

    public GameObject controlsMenu;

    private bool isTabHeld = false;

    // Start is called before the first frame update
    void Start()
    {
        //initially hide controls display
        controlsMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //check for TAB
        if(Input.GetKey(KeyCode.Tab))
        {
            if(!isTabHeld)
            {
                isTabHeld = true;
                ShowControls();
            }
        }
        else
        {
            if(isTabHeld)
            {
                isTabHeld = false;
                HideControls();
            }
        }
    }

    void ShowControls()
    {
        controlsMenu.SetActive(true); //show menu

    }

    void HideControls()
    {
        controlsMenu.SetActive(false); //hide
    }
}
