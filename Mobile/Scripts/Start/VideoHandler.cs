using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoHandler : MonoBehaviour
{
    //public Camera ca; 
    public RawImage mScreen = null;
    public VideoPlayer mVideoPlayer = null;
    public float wid;
    public float hig; 

    // Start is called before the first frame update
    void Start()
    {
        wid = Screen.width;
        hig = Screen.height; 

        if (mScreen != null && mVideoPlayer != null)
        {
           
            StartCoroutine(PrepareVideo());
        }
    }

    protected IEnumerator PrepareVideo()
    {
    
        // 비디오 준비
        mVideoPlayer.Prepare();

      //  mScreen.rectTransform.sizeDelta = new Vector2(wid, hig);
        // 비디오가 준비되는 것을 기다림
        while (!mVideoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(0.5f);
        }

        // VideoPlayer의 출력 texture를 RawImage의 texture로 설정한다
        mScreen.texture = mVideoPlayer.texture;
    }

    public void PlayVideo()
    {
        if (mVideoPlayer != null && mVideoPlayer.isPrepared)
        {
            // 비디오 재생
            mVideoPlayer.Play();
        }
    }

    public void StopVideo()
    {
        if (mVideoPlayer != null && mVideoPlayer.isPrepared)
        {
            // 비디오 멈춤
            mVideoPlayer.Stop();
        }
    }

}
