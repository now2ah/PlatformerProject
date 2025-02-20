using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    bool _isRight = false;
    SpriteRenderer _spriteRenderer;

    public float speed = 1.5f;
    public float changeDirection = 1.5f;

    void _Move()
    {
        if (!_isRight)
        {
            transform.Translate(transform.right * -1f * Time.deltaTime * speed);
        }
        else if(_isRight)
        {
            transform.Translate(transform.right * Time.deltaTime * speed);
        }
    }

    IEnumerator ChangeDirectionCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(changeDirection);
            _isRight = !_isRight;
            _spriteRenderer.flipX = !_spriteRenderer.flipX;
        }
    }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        StartCoroutine(ChangeDirectionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsRunning)
            return;

        _Move();
    }
}
