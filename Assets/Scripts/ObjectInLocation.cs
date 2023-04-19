using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectInLocation : MonoBehaviour
{
    [SerializeField] float initialObjectHeight;
    [SerializeField] WayToFindInitialValues wayToFindInitialValues;
    [SerializeField] float initialObjectDistance;
    [SerializeField] float initialObjectPosition;
    [SerializeField] Location location;

    float objectDistance;
    float objectPosition;
    protected float objectHeight;
    protected float relativeObjectHeight;

    const float w = 1.0f;
    
    float tgA;
    float tgB;
    float horizonLineHeight1;    

    public enum WayToFindInitialValues
    {
        FindByInitialObjectDistance,
        FindByInitialObjectPosition,
        FindByInitialObjectYCoordinate
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {        
        switch (wayToFindInitialValues)
        {
            case WayToFindInitialValues.FindByInitialObjectDistance:
                {
                    objectDistance = initialObjectDistance;
                    objectPosition = FindObjectPositionByObjectDistance();
                    transform.position = new Vector3(transform.position.x, location.ConvertMetersToUnits(objectPosition), transform.position.z);
                    break;
                }
            case WayToFindInitialValues.FindByInitialObjectPosition:
                {
                    objectPosition = initialObjectPosition;
                    objectDistance = FindObjectDistanceByObjectPosition();
                    transform.position = new Vector3(transform.position.x, location.ConvertMetersToUnits(objectPosition), transform.position.z);
                    break;
                }
            case WayToFindInitialValues.FindByInitialObjectYCoordinate:
                {
                    objectPosition = location.ConvertUnitsToMeters(transform.position.y);
                    objectDistance = FindObjectDistanceByObjectPosition();
                    break;
                }
        }

        PreparationForFindingObjectHeight();
        objectHeight = FindObjectHeight();
        relativeObjectHeight = objectHeight/initialObjectHeight;
        transform.localScale = new Vector3(relativeObjectHeight, relativeObjectHeight, relativeObjectHeight);
    }

    // Update is called once per frame
    void Update()
    {

    }

    float FindObjectPositionByObjectDistance()
    {
        float tgC = location.horizonLineHeight / (objectDistance + location.distanceToDistancePoint);
        float tgD = location.horizonLineHeight / (location.locationWidth / 2);
        float d1 = objectDistance * tgD / (tgC + tgD);
        return d1 * tgC;
    }

    float FindObjectDistanceByObjectPosition()
    {
        float tgD = location.horizonLineHeight / (location.locationWidth / 2);
        return ((objectPosition * location.distanceToDistancePoint) + (objectPosition * location.horizonLineHeight / tgD)) / (location.horizonLineHeight - objectPosition);
    }

    void PreparationForFindingObjectHeight()
    {
        if (initialObjectHeight != location.horizonLineHeight)
        {
            tgA = location.horizonLineHeight / w;
            if (initialObjectHeight > location.horizonLineHeight)
            {
                float initialObjectHeight1 = initialObjectHeight - location.horizonLineHeight;
                tgB = initialObjectHeight1 / w;
            }
            else
            {
                horizonLineHeight1 = location.horizonLineHeight - initialObjectHeight;
                tgB = horizonLineHeight1 / w;
            }
        }
    }

    float FindObjectHeight()
    {
        if (initialObjectHeight == location.horizonLineHeight) return location.horizonLineHeight - objectPosition;
        else
        {
            float w1 = objectPosition / tgA;
            if (initialObjectHeight > location.horizonLineHeight)
            {
                float objectHeight1 = (w - w1) * tgB;
                float objectHeight2 = location.horizonLineHeight - objectPosition;
                return objectHeight1 + objectHeight2;
            }
            else
            {
                if (objectPosition < initialObjectHeight)
                {
                    float objectHeight1 = w1 * tgB;
                    float objectHeight2 = initialObjectHeight - objectPosition;
                    return objectHeight1 + objectHeight2;
                }
                else
                {
                    float horizonLineHeight11 = (w - w1) * tgB;
                    return horizonLineHeight1 - (objectPosition - initialObjectHeight) - horizonLineHeight11;
                }
            }
        }
    }

    protected void ChangeObjectPosition(float deltaX, float deltaObjectDistance)
    {
        objectDistance = objectDistance + deltaObjectDistance;
        objectPosition = FindObjectPositionByObjectDistance();
        transform.position = new Vector3(transform.position.x + location.ConvertMetersToUnits(deltaX), location.ConvertMetersToUnits(objectPosition), transform.position.z);
        
        objectHeight = FindObjectHeight();
        relativeObjectHeight = objectHeight / initialObjectHeight;
        transform.localScale = new Vector3(relativeObjectHeight, relativeObjectHeight, relativeObjectHeight);
    }
}
