using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public Cam cam;
    public string nextStage = "";
    public float intervalSecond = 3F;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // 스테이지 클리어
            UIManager.instance.ShowStageClear();
            // 효과음
            GetComponent<AudioSource>().Play();
            cam.GetComponent<Camera>().orthographicSize = 10;

            Invoke("StageClear", intervalSecond);
        }
    }
    private void StageClear()
    {
        if (nextStage == "StageEnd")
        {
            StageEnd();
        }
        else
        {
            SceneManager.LoadScene(nextStage);
        }
    }

    private void StageEnd()
    {
        Application.Quit();
    }
}
