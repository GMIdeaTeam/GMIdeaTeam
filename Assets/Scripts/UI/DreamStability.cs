using Idea.Player;
using UnityEngine;
using UnityEngine.UI;

public class DreamStability : MonoBehaviour
{
    public PlayerData playerData;

    void FixedUpdate()
    {
        GetComponent<Image>().fillAmount = playerData.HealthPoint / 100.0f;
    }

}