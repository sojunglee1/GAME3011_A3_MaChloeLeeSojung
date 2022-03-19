using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GUIManager : MonoBehaviour
{
    public Animator fadeAnimator;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeLeftText;

    public GameObject statsPanel;
    public GameObject resultsPanel;
    public TextMeshProUGUI resultsScoreText;

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            scoreText.text = $"Score: \n{GameManager.inst.score}";
            timeLeftText.text = $"Score: \n{(int)GameManager.inst.timeLeft}";

            if ((int)GameManager.inst.timeLeft <= 0)
            {
                statsPanel.SetActive(false);
                resultsPanel.SetActive(true);
                resultsScoreText.text = $"Score: \n{GameManager.inst.score}";
            }
        }
    }

    public void PlayFadeAnimation(string sceneName)
    {
        fadeAnimator.Play("Fade");
        switch(sceneName)
        {
            case "Game":
                StartCoroutine(LoadGame(fadeAnimator.GetCurrentAnimatorClipInfo(0).Length, sceneName));
                break;
            case "MainMenu":
                StartCoroutine(LoadGame(fadeAnimator.GetCurrentAnimatorClipInfo(0).Length, sceneName));
                break;
        }

    }

    IEnumerator LoadGame(float time, string sceneName)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(sceneName);
    }
}
