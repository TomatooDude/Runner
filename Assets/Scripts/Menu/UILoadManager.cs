using System.Collections;

using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

internal class UILoadManager : MonoBehaviour
{
    public AbstractCharacter _character;
    public PlatformFactory _platform;
    public DeathMenu _DeathMenu;
    public Camera _camera;
    public TimerCountdown _timer;
    public Pause _pause;
    public AudioSource _audio;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public  void Pause()
    {
        PauseGame();
        _pause._pauseUI.SetActive(true);
    }
    internal void Trigger()
    {

        _character._isPlay = false;

        _character._animator.Play("Death");


        PauseGame();
        _audio.Pause();
        StartCoroutine(DeathPanel());

    }

    IEnumerator DeathPanel()
    {

        yield return new WaitForSecondsRealtime(2f);

        this._character.transform.position = _platform.PrefabSpawned[2]._playerSpawnPosition.transform.position;////

        this._character._line = 0;
        this._character._targetPos = this._character._rb.transform.position;
        ScoreManager.instance._gameUI.SetActive(false);

        _DeathMenu.deathPanel.SetActive(true);
        _character._isPlay = true;
    }

    public void MainMenu()
    {
        ResumeGame();
        ScoreManager.coin = 0;
        SceneManager.LoadScene("MainMenu");
    }


    public void Resume()
    {
        if (ScoreManager.coin >= 1)
        {

            _DeathMenu.deathPanel.SetActive(false);
            ScoreManager.instance._gameUI.SetActive(true);
           // ResumeGame();
           // _DeathMenu._audio.Play();
           // ScoreManager.coin -= 1;
            StartCoroutine(ResumeGamePause());

        }

    }

    public void ResumeOnPause()
    {
        _DeathMenu.deathPanel.SetActive(false);
        _pause._pauseUI.SetActive(false);
        ResumeGame();
        ScoreManager.instance._gameUI.SetActive(true);
        _character._isPlay = true;
    }
    IEnumerator ResumeGamePause()
    {
        _character._isPlay = false;
        _character._animator.SetBool("Run", false);
        _timer._timer.SetActive(true);
        _timer.StartCorutine();
        yield return new WaitForSecondsRealtime(5f);

        ResumeGame();
        _audio.Play();
        ScoreManager.coin -= 1;
        _character._animator.SetBool("Run", true);
        _character._isPlay = true;

    }

    public void Restart()
    {
        _audio.Stop();
        ResumeGame();

        ScoreManager.coin = 0;
        SceneManager.LoadScene("LVL");
        ScoreManager.instance._gameUI.SetActive(true);
    }

    internal void ResumeGame()
    {
        Time.timeScale = 1;
    }
    void PauseGame()
    {
        Time.timeScale = 0;
    }
}