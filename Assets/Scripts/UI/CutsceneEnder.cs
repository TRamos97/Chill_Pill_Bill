using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class CutsceneEnder : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer vid;
    public string nextScene;

    void Start() 
    { 
        vid.loopPointReached += CheckOver; 
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            EndCutscene(nextScene);
        }
    }

    void CheckOver(UnityEngine.Video.VideoPlayer vp)
    {
        print("Video Is Over");
        EndCutscene(nextScene);
    }

    void EndCutscene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
