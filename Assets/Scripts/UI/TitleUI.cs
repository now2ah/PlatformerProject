using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUI : MonoBehaviour
{
    float _titleProgressTextAlpha = 1f;
    bool _titleProgressTextFlag = false;

    Vector3 _titleImage1Pos;
    Vector3 _titleImage2Pos;

    public TextMeshProUGUI titleProgressText;
    public float titleProgressTextAlphaFade = 2f;

    public Image titleImage1;
    public Image titleImage2;
    public float titleImageSpeed = 2f;

    IEnumerator ProgressTextEffectCoroutine()
    {
        while(true)
        {
            if (_titleProgressTextFlag)
            {
                _titleProgressTextAlpha -= titleProgressTextAlphaFade * Time.deltaTime;
            }
            else if (!_titleProgressTextFlag)
            {
                _titleProgressTextAlpha += titleProgressTextAlphaFade * Time.deltaTime;
            }

            if (_titleProgressTextAlpha <= 0)
                _titleProgressTextFlag = false;
            else if (_titleProgressTextAlpha >= 1)
                _titleProgressTextFlag = true;

            Color newColor = new Color(titleProgressText.color.r, titleProgressText.color.g, titleProgressText.color.b, _titleProgressTextAlpha);

            titleProgressText.color = newColor;
            yield return null;
        }
    }

    IEnumerator TitleImageEffectCoroutine()
    {
        titleImage1.transform.position += new Vector3(titleImage1.transform.position.x - 5, 0, 0);
        titleImage2.transform.position += new Vector3(titleImage2.transform.position.x + 5, 0, 0);
        while(Vector2.Distance(titleImage1.transform.position, _titleImage1Pos) > 0.1f &&
            Vector2.Distance(titleImage2.transform.position, _titleImage2Pos) > 0.1f)
        {
            titleImage1.transform.position += Vector3.right * Time.deltaTime * titleImageSpeed;
            titleImage2.transform.position += Vector3.right * -1f * Time.deltaTime * titleImageSpeed;
            yield return null;
        }
    }

    void _GetInput()
    {
        if (Input.anyKey)
            SceneManager.LoadScene(1);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _titleImage1Pos = titleImage1.transform.position;
        _titleImage2Pos = titleImage2.transform.position;
        StartCoroutine(ProgressTextEffectCoroutine());
        StartCoroutine(TitleImageEffectCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        _GetInput();
    }
}
