using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningKeybind : MonoBehaviour
{
    public ParticleSystem particles;
    public Light pointLight;
    public float maxIntensity = 20;
    public float lightFlashTime = 0.8f;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(lightFlash());
        }
    }

    IEnumerator lightFlash()
    {
        particles.Play(true);
        pointLight.intensity = maxIntensity;
        yield return new WaitForSeconds(lightFlashTime);
        pointLight.intensity = 0;
    }
}
