using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Character character;

    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaZ = Input.GetAxis("Vertical");
        if(deltaX != 0 || deltaZ != 0) character.Move(deltaX * Time.deltaTime, deltaZ * Time.deltaTime);
    }
}