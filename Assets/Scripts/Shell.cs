using UnityEngine;

public class Shell : MonoBehaviour
{
    Vector3 _direction;
    public float lifeTime = 3f;
    public float speed = 1f;

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }

    private void Update()
    {
        transform.position += _direction * Time.deltaTime * speed;
    }
}
