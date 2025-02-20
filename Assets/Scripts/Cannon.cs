using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject shellPrefab;
    public float shootSpeed = 2f;

    IEnumerator ShootCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(shootSpeed);
            //sprite's foward is opposite
            GameObject shellObj = Instantiate(shellPrefab, transform.position + transform.right * -1.5f, Quaternion.identity);
            
            if (shellObj.TryGetComponent<Shell>(out Shell shell))
            {
                shell.SetDirection(transform.right * -1f);
            }
            else
            {
                shell = shellObj.AddComponent<Shell>();
                shell.SetDirection(transform.right * -1f);
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(ShootCoroutine());
    }
}
