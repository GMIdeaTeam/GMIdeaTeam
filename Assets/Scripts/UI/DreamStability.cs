using Idea.Mode;
using Idea.Player;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class DreamStability : MonoBehaviour
{
    public ModeController modeController;
    public Camera editCamera;
    public PlayerData playerData;
    private PostProcessVolume postProcessVolume;
    private Grain grain;
    private Vignette vignette;
    //void FixedUpdate()
    //{
    //    GetComponent<Image>().fillAmount = playerData.HP / 100.0f;
    //}

    private void Start()
    {
        
    }

    public void Update()
    {
        if(modeController.IsEditMode)
        {
            postProcessVolume = editCamera.GetComponent<PostProcessVolume>();
            postProcessVolume.profile.TryGetSettings(out vignette);
            postProcessVolume.profile.TryGetSettings(out grain);
            grain.intensity.value += 0.02f * Time.deltaTime;
            grain.size.value += 0.02f * Time.deltaTime;
            grain.lumContrib.value -= 0.02f * Time.deltaTime;
            vignette.intensity.value += 0.02f * Time.deltaTime;
        }
    }
}