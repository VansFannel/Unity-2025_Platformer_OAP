using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;

    private float direction = 1.0f;
    //bool isFlying = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += direction * speed * Time.deltaTime * new Vector3(1.0f, .0f, .0f);
    }

    public void StopFlying()
    {
        //isFlying = false;

        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        direction = -direction;

        //isFlying = true;
    }
}
