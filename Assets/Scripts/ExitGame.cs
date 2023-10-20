using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExitGame : MonoBehaviour
{

    public RawImage blackScreen;
    public float fadeSpeed;
    Color screenColor;
    private float fadeAmount;
    bool startFade = false;

    // Update is called once per frame
    void Update()
    {

        if (startFade)
        {
            screenColor = blackScreen.color;
            if (blackScreen.GetComponent<RawImage>().color.a < 1)
            {
                fadeAmount = screenColor.a + (fadeSpeed * Time.deltaTime);
                screenColor = new Color(screenColor.r, screenColor.g, screenColor.b, fadeAmount);
                blackScreen.color = screenColor;
            }
            else
            {
                Application.Quit();
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameObject[] pickups;
            pickups = GameObject.FindGameObjectsWithTag("pickup");
            float total = pickups.Length;
            if (!startFade && total == 0)
            {
                startFade = true;
            }
        }
    }
}
