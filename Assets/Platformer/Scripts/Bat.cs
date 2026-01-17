using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;

    private float direction = 1.0f;

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime * new Vector3(1.0f, .0f, .0f);
    }

    public void StopFlying()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        direction = -direction;
    }
}
