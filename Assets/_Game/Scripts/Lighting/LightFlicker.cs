using UnityEngine.Rendering.Universal;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    private Light2D flickerLight;
    private bool isExpanding = false;
    private int frameCount = 0;

    public float flickerLightMaxRadius;
    public float flickerLightMinRadius;
    public float radiusIncrementPerUpdate;
    public float intensityIncrementPerUpdate;
    public int framesPerLightUpdate;
    public Color LightColor
    {
        get => flickerLight.color;
        set => flickerLight.color = value;
    }

    void Awake()
    {
        flickerLight = GetComponentInChildren<Light2D>();
    }

    void Update()
    {
        frameCount++;

        if(frameCount >= framesPerLightUpdate)
        {
            FlickerLight();
            frameCount = 0;
        }
    }

    void FlickerLight()
    {
        if(isExpanding)
        {
            if (flickerLight.pointLightOuterRadius < flickerLightMaxRadius)
            {
                flickerLight.pointLightOuterRadius += radiusIncrementPerUpdate;
                flickerLight.intensity += intensityIncrementPerUpdate;
            }
            else 
            {
                isExpanding = false;
            }
        } 
        else
        {
            if (flickerLight.pointLightOuterRadius > flickerLightMinRadius)
            {
                flickerLight.pointLightOuterRadius -= radiusIncrementPerUpdate;
                flickerLight.intensity -= intensityIncrementPerUpdate;
            }
            else
            {
                isExpanding = true;
            }
        }
    }

}
