using System;
using Unity.VisualScripting;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    PlayerController _player;
    float _limitX_L = 0f;
    float _limitX_R = 0f;

    public void CalculateStageLimitX()
    {
        Ground[] grounds = FindObjectsByType<Ground>(FindObjectsSortMode.None);

        foreach (var ground in grounds)
        {
            if (ground.MinBoundX < _limitX_L)
                _limitX_L = ground.MinBoundX;

            if (ground.MaxBoundX > _limitX_R)
                _limitX_R = ground.MaxBoundX;
        }
    }

    void _FollowPlayer()
    {
        Vector3 newPos = new Vector3(_player.transform.position.x, transform.position.y, transform.position.z);
        transform.position = newPos;
    }

    void _SetPlayer(object sender, EventArgs e)
    {
        _player = GameManager.Instance.Player;
    }

    bool _IsInLimit()
    {
        float camX_L = Camera.main.ViewportToWorldPoint(Vector3.zero).x;
        float camX_R = Camera.main.ViewportToWorldPoint(Vector3.one).x;

        if (camX_L <= _limitX_L || camX_R >= _limitX_R)
        {
            return true;
        }
        else
            return false;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager.Instance.onSetPlayer += _SetPlayer;
    }


    // Update is called once per frame
    void LateUpdate()
    {
        if (!_IsInLimit())
            _FollowPlayer();
    }
}
