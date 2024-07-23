using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(GameUI))]
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    [SerializeField]
    private int katanaCount;

    [Header("Katana Spawning")]
    [SerializeField]
    private Vector2 katanaSpawnPosition;
    [SerializeField]
    private GameObject katanaObject;

    public GameUI GameUI { get; private set; }

    private void Awake()
    {
        Instance = this;
        GameUI = GetComponent<GameUI>();
    }

    private void Start()
    {
        GameUI.SetInitialDisplayedKatanaCount(katanaCount);
        SpawnKatana();
    }

    public void OnSuccessfulKatanaHit()
    {
        if (katanaCount > 0)
        {
            SpawnKatana();
        }
        else
        {
            StartGameOverSequence(true);
        }
    }

    private void SpawnKatana()
    {
        katanaCount--;
        Instantiate(katanaObject, katanaSpawnPosition, Quaternion.identity);
    }

    public void StartGameOverSequence(bool win)
    {
        StartCoroutine("GameOverSequenceCoroutine", win);
    }

    private IEnumerator GameOverSequenceCoroutine(bool win)
    {
        if (win)
        {
            yield return new WaitForSecondsRealtime(0.3f);
            RestartGame();
        }
        else
        {
            GameUI.ShowRestartButton();
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
