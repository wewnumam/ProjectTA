using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScreenScaleSlider : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private Slider scaleSlider;
    [SerializeField] private TMP_Text scaleValueText;

    [Header("Scale Settings")]
    [Range(0.5f, 1.0f)]
    [SerializeField] private float defaultScale = 1.0f;

    private void Start()
    {
        // Set initial slider value and default scale
        if (scaleSlider != null)
        {
            scaleSlider.minValue = 0.5f; // Minimum rendering scale
            scaleSlider.maxValue = 1.0f; // Maximum rendering scale
            scaleSlider.value = defaultScale;

            scaleSlider.onValueChanged.AddListener(OnScaleSliderChanged);
        }

        UpdateScale(defaultScale);
    }

    private void OnScaleSliderChanged(float value)
    {
        UpdateScale(value);
    }

    private void UpdateScale(float scale)
    {
        // Update the rendering scale
        ScalableBufferManager.ResizeBuffers(scale, scale);

        // Update the UI text if available
        if (scaleValueText != null)
        {
            scaleValueText.SetText($"{(scale * 100f):F0}%");
        }
    }

    private void OnDestroy()
    {
        // Clean up event listener
        if (scaleSlider != null)
        {
            scaleSlider.onValueChanged.RemoveListener(OnScaleSliderChanged);
        }
    }
}
