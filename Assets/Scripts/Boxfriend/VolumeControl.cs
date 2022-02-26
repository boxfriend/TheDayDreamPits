using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] private AudioMixer _mixer;
    [SerializeField] private Slider _slider;

    private void Start ()
    {
        var volumeSetting = PlayerPrefs.GetFloat("Volume", 0.75f);
        _mixer.SetFloat("MasterVolume", ConvertToDecibel(volumeSetting));
        _slider.value = volumeSetting;
    }

    public void SetVolume()
    {
        var volumeSetting = _slider.value;
        _mixer.SetFloat("MasterVolume", ConvertToDecibel(volumeSetting));
        PlayerPrefs.SetFloat("Volume", volumeSetting);
    }

    private float ConvertToDecibel (float number) => Mathf.Log10(number) * 20;
}
