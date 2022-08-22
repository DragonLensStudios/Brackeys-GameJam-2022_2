using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    // Start is called before the first frame update
    private List<Light2D> lights = new List<Light2D>();
    private int nextUpdate = 1;
    private int currentFrameCount = 0;
    private bool innerLightOn = true;
    private bool outerLightOn = true;
    private bool isInnersTurn = false;
    private bool isOutersTurn = true;

    public float flickerLowerFrequency;
    public float flickerUpperFrequency;

    void Start()
    {
        GameObject[] flickers = GameObject.FindGameObjectsWithTag("Flicker");
        foreach(GameObject gameObject in flickers)
        {
            lights.Add(GameObjectToLight2D(gameObject));
        }
    }

    // Update is called once per frame
    void Update()
    {

        currentFrameCount++;
        if (currentFrameCount >= nextUpdate)
        {
            //Randomise when the next update will come, this can be done via the object this script is attached to. 
            nextUpdate =((int)Random.Range(flickerLowerFrequency, flickerUpperFrequency));
            UpdateLights();
            currentFrameCount = 0;
        }
    }

    void UpdateLights()
    {
        float target = Mathf.InverseLerp(flickerLowerFrequency, flickerUpperFrequency, nextUpdate);
        float intensity = Mathf.SmoothStep(0.25f, 0.5f, target);
        for (int i = 0; i < lights.Count; i++)
        {
            Light2D light = lights[i];
            
            //Turn inside light off
            if (innerLightOn && !outerLightOn && isInnersTurn && light.name == "Candle Light 2D Flicker Inner")
            {
                light.intensity = intensity;
                innerLightOn = false;
                isOutersTurn = false;
                isInnersTurn = true;
                break;
            }

            //Turn inside light on 
            if (!innerLightOn && !outerLightOn && light.name == "Candle Light 2D Flicker Inner")
            {
                light.intensity = intensity;
                innerLightOn = true;
                isInnersTurn = false;
                isOutersTurn = true;
                break;
            }

            //Turn outside light off
            if (outerLightOn && innerLightOn && light.name == "Candle Light 2D Flicker Outer")
            {
                light.intensity = intensity;
                outerLightOn = false;
                isInnersTurn = true;
                isOutersTurn = false;
                break;
            }

            //Turn outside light on 
            if (innerLightOn && !outerLightOn && isOutersTurn && light.name == "Candle Light 2D Flicker Outer")
            {
                light.intensity = intensity;
                outerLightOn = true;
                isOutersTurn = true;
                isInnersTurn = false;
                break;
            }
        }
    }

    public static Light2D GameObjectToLight2D(GameObject gameObject)
    {
        return gameObject.GetComponent<Light2D>();
    }
}
