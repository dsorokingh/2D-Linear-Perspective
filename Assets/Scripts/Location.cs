using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Location : MonoBehaviour
{
    [SerializeField] float locationHeight;
    public float locationWidth;
    public float horizonLineHeight;
    public float distanceToDistancePoint;

    float k;

    // Start is called before the first frame update
    void Start()
    {
        float locationHeightUnits = Camera.main.orthographicSize * 2;
        k = locationHeightUnits / locationHeight;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float ConvertMetersToUnits(float m)
    {
        return m * k;
    }

    public float ConvertUnitsToMeters(float u)
    {
        return u / k;
    }
}
