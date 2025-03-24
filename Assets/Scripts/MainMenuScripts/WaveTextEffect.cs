using UnityEngine;
using TMPro;

public class WaveTextEffect : MonoBehaviour
{
    public TMP_Text textMeshPro; // Assign your TMP text object in the Inspector
    public float transitionDuration = 3.0f; // Time to transition from black to white
    public float waveSpeed = 2.0f; // Speed of the wave effect
    public float waveTransitionDuration = 4.0f; // Duration for transitioning into wave colors
    public float waveLength = 0.5f; // Controls how long or short the wave appears

    // The colors for the wave effect
    public Color waveStartColor = new Color(0.03f, 0.39f, 0.62f); // #07659F (blue)
    public Color waveEndColor = new Color(0.75f, 0.10f, 0.95f); // #BF1AF3 (purple)

    private TMP_TextInfo textInfo;
    private Color currentStartColor = Color.black; // Initial color: black
    private Color currentEndColor = Color.black; // Initially set to black
    private float transitionTime = 0f; // Time counter for the transition
    private float waveTransitionTime = 0f; // Time counter for wave colors transition
    private bool isInWaveTransition = false; // Flag to track if we're in wave transition

    void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TMP_Text>();
        }
        textInfo = textMeshPro.textInfo;
        textMeshPro.ForceMeshUpdate(); // Ensure mesh data is up-to-date
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
        }
        else if (!isInWaveTransition)
        {
            // Start the wave transition after it becomes white
            isInWaveTransition = true;
        }

        // Handle wave color transition after text turns white
        if (isInWaveTransition && waveTransitionTime < waveTransitionDuration)
        {
            waveTransitionTime += Time.deltaTime;
            float t = waveTransitionTime / waveTransitionDuration;
            currentStartColor = Color.Lerp(Color.white, waveStartColor, t); // Transition to wave start color
            currentEndColor = Color.Lerp(Color.white, waveEndColor, t); // Transition to wave end color
        }

        // Apply the wave effect
        AnimateText();
    }

    private void AnimateText()
    {
        textMeshPro.ForceMeshUpdate();
        textInfo = textMeshPro.textInfo;

        // Get the number of characters in the text
        int characterCount = textInfo.characterCount;
        if (characterCount == 0) return;

        float time = Time.time * waveSpeed;

        // Iterate over each character in the text
        for (int i = 0; i < characterCount; i++)
        {
            if (!textInfo.characterInfo[i].isVisible) continue;

            // Calculate the wave effect using waveLength
            float progress = Mathf.Sin((i * waveLength) - time);
            progress = (progress + 1) / 2; // Normalize progress to range [0, 1]

            // Interpolate the color based on the wave progress
            Color characterColor = Color.Lerp(currentEndColor, currentStartColor, progress);

            // Get the vertex colors of the character
            int vertexIndex = textInfo.characterInfo[i].vertexIndex;
            Color32[] vertexColors = textInfo.meshInfo[textInfo.characterInfo[i].materialReferenceIndex].colors32;

            // Apply the color to each vertex of the character
            vertexColors[vertexIndex + 0] = characterColor;
            vertexColors[vertexIndex + 1] = characterColor;
            vertexColors[vertexIndex + 2] = characterColor;
            vertexColors[vertexIndex + 3] = characterColor;
        }

        // Update the mesh with the new colors
        for (int i = 0; i < textInfo.meshInfo.Length; i++)
        {
            textInfo.meshInfo[i].mesh.colors32 = textInfo.meshInfo[i].colors32;
            textMeshPro.UpdateGeometry(textInfo.meshInfo[i].mesh, i);
        }
    }
}
