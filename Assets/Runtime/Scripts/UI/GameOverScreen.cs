using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private GameMode gameMode;
    [SerializeField] private MedalRewardCalculator medalRewardsCalculator;

    [Space] [Header("Elements")]
    [SerializeField] private Image medalImage;
    [SerializeField] private TextMeshProUGUI currentScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;

    [Space] [Header("Containers")]
    [SerializeField] private CanvasGroup gameOverContainer;
    [SerializeField] private CanvasGroup statsContainer;
    [SerializeField] private CanvasGroup buttonsContainer;

    [Space] [Header("Stats Tween")]
    [SerializeField] private float statsTweenDelaySeconds = 0.3f;
    [SerializeField] private float statsTweenTimeSeconds = 0.5f;
    [SerializeField] private Transform statsContainerStart;

    [Space] [Header("GameOver Tween")]
    [SerializeField] private Transform gameOverContainerStart;
    [SerializeField] private float gameOverTweenTimeSeconds = 0.5f;

    [Space] [Header("Buttons Tween")]
    [SerializeField] private float buttonsTweenDelaySeconds = 0.5f;
    [SerializeField] private float buttonsTweenTimeSeconds = 0.5f;


    private void OnEnable()
    {
        UpdateUI();
        StartCoroutine(Show());
    }

    private void UpdateUI()
    {
        currentScoreText.text = gameMode.Score.ToString();
        highScoreText.text = gameMode.BestScore.ToString();

        Medal medal = medalRewardsCalculator.GetMedalForScore(gameMode.Score);
        if (medal != null)
        {
            medalImage.sprite = medal.MedalSprite;
        }
        else
        {
            medalImage.gameObject.SetActive(false);
        }
    }

    public void OnRetryClicked()
    {
        gameMode.ReloadGame();
    }

    public void OnQuitClicked()
    {
        gameMode.QuitGame();
    }

    private IEnumerator Show()
    {
        gameOverContainer.alpha = 0;
        gameOverContainer.blocksRaycasts = false;

        statsContainer.alpha = 0;
        statsContainer.blocksRaycasts = false;

        buttonsContainer.alpha = 0;
        buttonsContainer.blocksRaycasts = false;

        yield return StartCoroutine(AnimateCanvasGroup(gameOverContainer, gameOverContainerStart.position, gameOverContainer.transform.position, gameOverTweenTimeSeconds));
        yield return new WaitForSeconds(statsTweenDelaySeconds);

        yield return StartCoroutine(AnimateCanvasGroup(statsContainer, statsContainerStart.position, statsContainer.transform.position, statsTweenTimeSeconds));
        yield return new WaitForSeconds(buttonsTweenDelaySeconds);

        yield return StartCoroutine(AnimateCanvasGroup(buttonsContainer, buttonsContainer.transform.position, buttonsContainer.transform.position, buttonsTweenTimeSeconds));
    }

    private IEnumerator AnimateCanvasGroup(CanvasGroup group, Vector3 from, Vector3 to, float time)
    {
        Tween fadeTween = group.DOFade(1, time);
        group.transform.position = from;
        Tween transformTween = group.transform.DOMove(to, time);

        yield return fadeTween.WaitForKill();
        group.blocksRaycasts = true;
    }
}
