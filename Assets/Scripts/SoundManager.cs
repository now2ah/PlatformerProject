using System.Collections.Generic;
using UnityEngine;

public enum eAudioType
{
    NONE,
    BGM,
    SFX
}

public enum eBGM
{
    NONE,
    TITLE,
    GAME,
    BOSS
}

public enum eSFX
{
    NONE,
    BUTTON,
    CLEAR,
    GAMEOVER
}

public class SoundManager : Singleton<SoundManager>
{
    AudioSource _bgmAudioSource;
    AudioSource _sfxAudioSource;

    Dictionary<eBGM, AudioClip> _bgmDic;
    Dictionary<eSFX, AudioClip> _sfxDic;

    public void PlayBGM(eBGM type)
    {
        _bgmAudioSource.clip = _bgmDic[type];
        _bgmAudioSource.loop = true;
        _bgmAudioSource.Play();
    }

    AudioClip _GetAudioClip(string clipName)
    {
        return Resources.Load<AudioClip>("Sounds/" + clipName);
    }

    private void Awake()
    {
        _bgmAudioSource = gameObject.AddComponent<AudioSource>();
        _sfxAudioSource = gameObject.AddComponent<AudioSource>();
        _bgmDic = new Dictionary<eBGM, AudioClip>();
        _sfxDic = new Dictionary<eSFX, AudioClip>();
    }

    private void Start()
    {
        _bgmDic.Add(eBGM.TITLE, _GetAudioClip("BGM_title"));

        PlayBGM(eBGM.TITLE);
    }
}
