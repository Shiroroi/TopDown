using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class SandevistanGauge : MonoBehaviour
{
    public Image[] gaugeBars;  // Array of UI images representing the gauge (battery-like bars)
    public Color activeColor = Color.green;  // Color when the bar is filled
    public Color glowColor = Color.yellow;  // Glow effect color for the border when full
    public float glowSpeed = 1f;  // Speed of the glow effect

    private int kills = 0;  // The current number of kills
    public int maxKills = 10;  // Max kills to fill the bar (adjust as needed)
    private bool isGlowing = false;  // To check if the glow effect is active
    private float glowTimer = 0f;  // Timer to control the blinking effect

    void Update()
    {
        // Update the gauge based on the kills
        UpdateGauge();

        // If the gauge is full, start the glowing/blinking effect
        if (kills >= maxKills)
        {
            if (!isGlowing)
            {
                StartCoroutine(BlinkBorder());
                isGlowing = true;
            }
        }
        else
        {
            if (isGlowing)
            {
                StopCoroutine(BlinkBorder());
                ResetBorderGlow();
                isGlowing = false;
            }
        }
    }

    // Method to update the gauge bar based on the number of kills
    void UpdateGauge()
    {
        // How many bars should be filled based on kills
        int barsFilled = Mathf.FloorToInt((float)kills / maxKills * gaugeBars.Length);

        // Loop through each gauge bar and update the color based on the progress
        for (int i = 0; i < gaugeBars.Length; i++)
        {
            if (i < barsFilled)
            {
                gaugeBars[i].color = activeColor;  // Fill the bar with the active color
            }
            else
            {
                gaugeBars[i].color = Color.gray;  // Empty bars are gray
            }
        }
    }

    // Register a kill and update the bar
    public void RegisterKill()
    {
        kills++;

        // After 5 kills, increase the bar by 3 blocks (adjustable)
        if (kills % 5 == 0)
        {
            // Logic to increase the gauge by 3 blocks here
            if (gaugeBars.Length > 0)
            {
                int fullBars = Mathf.FloorToInt(kills / 5);
                for (int i = 0; i < fullBars; i++)
                {
                    if (i < gaugeBars.Length)
                    {
                        gaugeBars[i].color = activeColor;  // Increase the gauge bar
                    }
                }
            }
        }
    }

    // Coroutine for blinking effect on the border
    IEnumerator BlinkBorder()
    {
        while (true)
        {
            glowTimer += Time.deltaTime * glowSpeed;
            if (glowTimer >= 1f)
            {
                glowTimer = 0f;
            }

            // Create a smooth glow effect by interpolating the color
            Color borderColor = Color.Lerp(glowColor, Color.black, Mathf.PingPong(glowTimer, 1f));
            foreach (var bar in gaugeBars)
            {
                if (bar.GetComponent<Outline>() != null)
                {
                    bar.GetComponent<Outline>().effectColor = borderColor;
                }
            }

            yield return null;
        }
    }

    // Reset the border color when the effect is over
    void ResetBorderGlow()
    {
        foreach (var bar in gaugeBars)
        {
            if (bar.GetComponent<Outline>() != null)
            {
                bar.GetComponent<Outline>().effectColor = Color.black;
            }
        }
    }
}

