using Idea.ModeController;
using UnityEngine;
using UnityEngine.UI;

public class DreamStability : MonoBehaviour
{
    public ModeController modeController;
    public int gauge = 100;

    void Update()
    {
        if (modeController.IsEditMode)
        {
            GetComponent<Image>().fillAmount -= 1.0f / gauge * 2 * Time.deltaTime;
        }
    }

}