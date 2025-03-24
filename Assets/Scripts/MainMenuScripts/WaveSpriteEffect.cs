using UnityEngine;

public class WaveSpriteEffect : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // Assign your sprite in the Inspector
    public float transitionDuration = 3.0f; // Time to transition from black to white
    public float waveSpeed = 2.0f; // Speed of the wave effect
    public float waveTransitionDuration = 4.0f; // Duration for transitioning into wave colors
    public float waveLength = 0.5f; // Controls how long or short the wave appears

    // The colors for the wave effect
    public Color waveStartColor = new Color(0.03f, 0.39f, 0.62f); // #07659F (blue)
    public Color waveEndColor = new Color(0.75f, 0.10f, 0.95f); // #BF1AF3 (purple)

    private Color currentStartColor = Color.black; // Initial color: black
    private Color currentEndColor = Color.black; // Initially set to black
    private float transitionTime = 0f; // Time counter for the transition
    private float waveTransitionTime = 0f; // Time counter for wave colors transition
    private bool isInWaveTransition = false; // Flag to track if we're in wave transition

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    void Update()
    {
        // Handle black to white transition first
        if (transitionTime < transitionDuration)
        {
            transitionTime += Time.deltaTime;
            float t = transitionTime / transitionDuration;
            // Smoothly transition from black to white
            currentStartColor = Color.Lerp(Color.black, Color.white, t);
            currentEndColor = currentStartColor;
            spriteRenderer.color = currentStartColor;
        }
        else if (!isInWaveTransition)
        {
            // Start the wave transition after it becomes white
            isInWaveTransition = true;
        }

        // Handle wave color transition after sprite turns white
        if (isInWaveTransition && waveTransitionTime < waveTransitionDuration)
        {
            waveTransitionTime += Time.deltaTime;
            float t = waveTransitionTime / waveTransitionDuration;
            currentStartColor = Color.Lerp(Color.white, waveStartColor, t); // Transition to wave start color
            currentEndColor = Color.Lerp(Color.white, waveEndColor, t); // Transition to wave end color
        }

        // Apply the wave effect
        ApplyWaveEffect();
    }

    private void ApplyWaveEffect()
    {
        float time = Time.time * waveSpeed;
        float progress = Mathf.Sin(time * waveLength);
        progress = (progress + 1) / 2; // Normalize progress to range [0, 1]

        // Interpolate the color based on the wave progress
        Color spriteColor = Color.Lerp(currentEndColor, currentStartColor, progress);
        spriteRenderer.color = spriteColor;
    }
}
