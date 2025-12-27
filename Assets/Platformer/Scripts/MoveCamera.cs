using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool autoMove = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float move = 0.0f;
        if (autoMove)
        {
            move = speed * Time.deltaTime;
        }

        transform.position += new Vector3(move, 0.0f, 0.0f);
    }
}
