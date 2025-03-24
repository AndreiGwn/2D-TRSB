using System.Collections;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    public CanvasGroup splashPanel;          // Reference to the CanvasGroup of the splash screen
    public AudioSource introAudioSource;    // Reference to the AudioSource for the intro sound
    public AudioSource mainMenuAudioSource; // Reference to the AudioSource for the main menu sound
    public float fadeDuration = 4f;         // How long the fade lasts
    public float displayTime = 15f;         // How long to display the splash screen

    private void Start()
    {
        // Start the splash screen process
        StartCoroutine(HandleSplashScreen());
    }

    private IEnumerator HandleSplashScreen()
    {
        // Ensure the splash screen starts fully visible
        splashPanel.alpha = 1;

        // Play the intro sound
        if (introAudioSource != null)
        {
            introAudioSource.Play();
        }

        // Wait for 7 seconds before starting the main menu sound
        yield return new WaitForSeconds(7f);

        // Start the main menu sound
        if (mainMenuAudioSource != null)
        {
            mainMenuAudioSource.Play();
        }

        // Continue displaying the splash screen for the remaining display time
        float remainingTime = Mathf.Max(0f, displayTime - 4f);
        yield return new WaitForSeconds(remainingTime);

        // Fade out the splash screen
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            splashPanel.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure it's fully faded out
        splashPanel.alpha = 0;

        // Optionally disable the splash screen
        splashPanel.gameObject.SetActive(false);
    }
}
