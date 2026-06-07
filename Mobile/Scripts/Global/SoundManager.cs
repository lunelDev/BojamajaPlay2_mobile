using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    public float fadeSpeed = 80f;
    public AudioMixer audioMixer;

    public AudioSource bgm;
    public AudioSource sfx;
    public AudioSource countdown;
    public AudioSource timer;
    public AudioSource fail;
    public AudioSource success;
    public AudioSource star;
    public AudioSource end_sfx;

    public AudioClip[] audioClips;
    public AudioClip[] StarSFXs;
    public Dictionary<string, AudioClip> lookupTable = new Dictionary<string, AudioClip>();

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this.gameObject);
        else Instance = this;
    }

    void Start()
    {
        foreach (AudioClip clip in audioClips)
        {
            lookupTable.Add(clip.name, clip);
        }

        PlayBGM();

        Timer.RoundEnd += OnRoundEnd;
    }

    void OnDisable()
    {
        Timer.RoundEnd -= OnRoundEnd;
    }
    public void End_Sfx()
    {
        StartCoroutine(_End_Sfx());
    }
    IEnumerator _End_Sfx()
    {
        end_sfx.clip = audioClips[0]; //빵빠래
        end_sfx.Play();
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if (!end_sfx.isPlaying)
            {
                end_sfx.clip = audioClips[6]; //결과 브금
                PlayBGM();
               
            }
        }
    }
    public void PlaySFX(string clipName, float volume = 1f)
    {
        sfx.PlayOneShot(GetClipByName(clipName), volume);
    }

    public void PlayStar()
    {
        int randSound = Random.Range(0, StarSFXs.Length);
        Debug.Log(randSound);
        star.PlayOneShot(StarSFXs[randSound]);
    }
    public AudioClip GetClipByName(string clipName)
    {
        return lookupTable[clipName];
    }

    public void PlayBGM()
    {
        StopCoroutine("FadeIn");
        StopCoroutine("FadeOut");
        StartCoroutine("FadeIn");
    }

    public void StopBGM()
    {
        StopCoroutine("FadeOut");
        StopCoroutine("FadeIn");
        StartCoroutine("FadeOut");
    }

    private void OnRoundEnd()
    {
        StopBGM();

        if (timer.isPlaying)
            timer.Stop();
    }

    IEnumerator FadeIn()
    {
        float vol = 0f;
        audioMixer.SetFloat("volumeBGM", vol);

        while (vol < 0f)
        {
            vol += Time.deltaTime * fadeSpeed;
            audioMixer.SetFloat("volumeBGM", vol);
            yield return new WaitForEndOfFrame();
        }
        audioMixer.SetFloat("volumeBGM", 0f);
    }

    IEnumerator FadeOut()
    {
        float vol = 0f;
        audioMixer.SetFloat("volumeBGM", vol);

        while (vol > -80f)
        {
            vol -= Time.deltaTime * fadeSpeed;
            audioMixer.SetFloat("volumeBGM", vol);
            yield return new WaitForEndOfFrame();
        }
        audioMixer.SetFloat("volumeBGM", -80f);
    }

}
