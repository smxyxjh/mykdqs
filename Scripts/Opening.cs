using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Opening : MonoBehaviour
{
    public VideoPlayer prologue;
    public VideoPlayer intro;
    public void PlayPrologue()
    {
        prologue.Play();
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayPrologue();
        prologue.loopPointReached += PrologueLoop;
        intro.loopPointReached += IntroLoop;
    }
    private void PrologueLoop(VideoPlayer souce)
    {
        intro.Play();
    }
    private void IntroLoop(VideoPlayer souce)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
