using Idea.ModeController;
using Idea.Player;
using UnityEngine;
using UnityEngine.UI;

public class DreamStability : MonoBehaviour
{
    public ModeController modeController;
    public PlayerData playerData;

    void FixedUpdate()
    {
        GetComponent<Image>().fillAmount = playerData.HP / 100.0f;
    }

}