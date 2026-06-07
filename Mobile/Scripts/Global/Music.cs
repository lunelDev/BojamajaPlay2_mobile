using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Music : MonoBehaviour
{
    public Slider Bgm_Volume;
    public Slider Sfx_Volume;
    public SoundManager audio;

    public float BGMbackVol;
    public float SfxbackVol;

    private float MAXSound = 0;
    private float MINSound = -80;
    // Start is called before the first frame update
    void Awake()
    {
        //키 값을 가지고 있지 않을 때,
        if (!PlayerPrefs.HasKey("BackVolum") && !PlayerPrefs.HasKey("BackVolum"))
        {
            //기본 사운드 값
            BGMbackVol = MAXSound;
            SfxbackVol = MAXSound;
            PlayerPrefs.SetFloat("BGMVolum", BGMbackVol); //BGM 사운드
            PlayerPrefs.SetFloat("SFXVolum", SfxbackVol);   //효과음 사운드
        }
        else
        {
            if (SceneManager.GetActiveScene().name.Equals("Main"))
            {
                Bgm_Volume.value = PlayerPrefs.GetFloat("BGMVolum");
                Sfx_Volume.value = PlayerPrefs.GetFloat("SFXVolum");
            }
            if(audio!= null)
            {
                audio.audioMixer.SetFloat("volumeBGM", BGMbackVol);
                audio.audioMixer.SetFloat("volumeSFX", SfxbackVol);
            }   
        }
    }
    // Update is called once per frame
    void Update()
    {
        //현재 씬이 메인인지 아닌지 판단
        if (SceneManager.GetActiveScene().name.Equals("Main"))
        {

            BGMSlider_Setting();
            SFXSlider_Setting();
        }
        else
        {
            GameBGM();
            GameSFX();
        }
    }
    ///////////메인에서 사용되는 사운드///////////////////////
    public void BGMSlider_Setting()
    {

        // Bgm_Volume.value = BGMbackVol;
        BGMbackVol = Bgm_Volume.value;
        audio.audioMixer.SetFloat("volumeBGM", BGMbackVol);
        PlayerPrefs.SetFloat("BGMVolum", BGMbackVol);
        PlayerPrefs.Save();
    }
    public void SFXSlider_Setting()
    {
        //Sfx_Volume.value = SfxbackVol;
        SfxbackVol = Sfx_Volume.value;
        audio.audioMixer.SetFloat("volumeSFX", SfxbackVol);
        PlayerPrefs.SetFloat("SFXVolum", SfxbackVol);
        PlayerPrefs.Save();
    }
    ///////////////////////////////////////////////////////////////

    ////////////////게임에서 사용되는 사운드 저장//////////////////////
    public void GameBGM()
    {
        BGMbackVol = PlayerPrefs.GetFloat("BGMVolum");
        audio.audioMixer.SetFloat("volumeBGM", BGMbackVol);
    }
    public void GameSFX()
    {
        if (audio.audioMixer)
            SfxbackVol = PlayerPrefs.GetFloat("SFXVolum");
        audio.audioMixer.SetFloat("volumeSFX", SfxbackVol);
    }
    /////////////////////////////////////////////////////////////
}
