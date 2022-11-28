using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DreamStability : MonoBehaviour
{
    public ModeController modeController;
    GameObject DreamBar;
    public int gauge = 100;

    // Start is called before the first frame update
    void Start()
    {
        DreamBar = GameObject.Find("DreamBar");
    }

    void Update()
    {
        if (modeController.checkEditMode() == true)
        {
            DreamBar.GetComponent<Image>().fillAmount -= 0.02f * Time.deltaTime;
        }
    }

}