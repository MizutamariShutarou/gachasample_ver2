using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

//日本語対応
public class LoadingManager : MonoBehaviour
{
    private static LoadingManager _instance = default;

    public static LoadingManager Instance => _instance;

    [SerializeField]
    private Image _loadingImage = default;

    [SerializeField]
    private GameObject _loadingGameObject = default;

    public Image LoadingImage => _loadingImage;

    private CancellationTokenSource _cts;

    private void Awake()
    {
        if (_instance)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        ActiveLoadingWindow(false);
        _loadingImage.fillAmount = 0f;
    }

    public void ActiveLoadingWindow(bool flag)
    {
        _loadingGameObject.SetActive(flag);
        ResetSliderValue();
    }

    public async UniTask ChangeSliderValue(float amount, CancellationToken ct)
    {
        _loadingImage.fillAmount = amount;
        await UniTask.Yield(PlayerLoopTiming.Update, cancellationToken:ct);
    }

    private void ResetSliderValue()
    {
        _loadingImage.fillAmount = 0f;
    }
}
