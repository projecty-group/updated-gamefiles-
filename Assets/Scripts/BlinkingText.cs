using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BlinkingText : MonoBehaviour
{
    public float blinkInterval = 0.2f; // Blink interval in seconds

    private TextMeshProUGUI textComponent;

    private void Start()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            textComponent.enabled = !textComponent.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}