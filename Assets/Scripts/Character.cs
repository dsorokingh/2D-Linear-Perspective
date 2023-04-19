using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Character : ObjectInLocation
{
    float speed = 2.0f;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float deltaX, float deltaZ)
    {
        float relativeSpeed = speed * relativeObjectHeight;
        ChangeObjectPosition(deltaX * relativeSpeed, deltaZ * relativeSpeed);
    }
}
