using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator mainMenuScreen;
    public GameObject title;
    public GameObject Sound;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void StarGame()
    {
       StartCoroutine(DelayDisplayOpening());
    }
    IEnumerator DelayDisplayOpening()
    {
        mainMenuScreen.Play("FadeOut");
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Option()
    {
        StartCoroutine(DelayDisplayAudioMenu());
    }
    IEnumerator DelayDisplayAudioMenu()
    {
        mainMenuScreen.Play("FadeOut");
        yield return new WaitForSeconds(0.5f);
        title.SetActive(false);
        Sound.SetActive(true);
    }
    public void SoundQuitGame()
    {
       StartCoroutine(DelayDisplaySounds());
    }
    IEnumerator DelayDisplaySounds()
    {
        mainMenuScreen.Play("Faderin");
        yield return new WaitForSeconds(0.5f);
        title.SetActive(true);
        Sound.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
