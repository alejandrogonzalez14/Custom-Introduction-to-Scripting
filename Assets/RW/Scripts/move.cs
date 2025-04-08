using UnityEngine;

public class move : MonoBehaviour
{
    public Vector3 movementSpeed;
    public Space space;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(movementSpeed * Time.deltaTime, space);
    }
}
