using UnityEngine;

public class Ground : MonoBehaviour
{
    Collider2D _collider;
    float _minBoundX;
    float _maxBoundX;

    public float MinBoundX { get => _minBoundX; set => _minBoundX = value; }
    public float MaxBoundX { get => _maxBoundX; set => _maxBoundX = value; }

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _minBoundX = _collider.bounds.min.x;
        _maxBoundX = _collider.bounds.max.x;
    }
}
