using DG.Tweening;
using GameOff2022.SO.Station;
using GithubGameOff2022.Player;
using GithubGameOff2022.Prop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameOff2022.Station
{
    public class StationManager : MonoBehaviour, IInteractible
    { 
        [Tooltip("Add to station scriptable here ")]
        [Space][SerializeField] private StationInfo _stationSo;
        [Space][SerializeField] private GameObject _uiContainer;
        [Space][SerializeField] private Image _stationIcon;
        [Space][SerializeField] private Image _productionFillImage;
        [Space][SerializeField] private TMP_Text _progressText;
        
        private bool _isBusy;

        private Tween _tween;

        private void Awake()
        {
            _uiContainer.gameObject.SetActive(false);
        }

        private void InstantiateItem(PlayerController player)
        {
            var go = Instantiate(_stationSo.OutputSo.Item);
            player.Hands = _stationSo.OutputSo == null ? null : new()
            {
                Instance = go,
                Item = _stationSo.OutputSo
            };
        }

        public void DoAction(PlayerController player)
        {
            _uiContainer.gameObject.SetActive(true);
            if (_stationSo.CraftingDuration > 0f)
            {
                InstantiateItem(player);
            }
            else
            {
                _isBusy = true;
                var targetValue = 1f;
                _productionFillImage.fillAmount = 0f;
                var currentValue = _productionFillImage.fillAmount;
                _tween = DOTween.To(() => currentValue,
                    setter: x => _productionFillImage.fillAmount = x, targetValue, _stationSo.CraftingDuration)
                    .OnUpdate(
                        () =>
                        {
                            var value = (_productionFillImage.fillAmount * 100f);
                            _progressText.text = $"{value:F1} %";

                        }).OnComplete(() =>
                        {

                            _productionFillImage.fillAmount = 0f;
                            _isBusy = false;
                            _progressText.text = string.Empty;
                            _uiContainer.gameObject.SetActive(false);
                            InstantiateItem(player);
                            _tween.Kill();

                        }).SetUpdate(true);
            }
        }

        public bool CanInterract(PlayerController player)
        {
            if (_isBusy)
            {
                return false;
            }
            if (_stationSo.InputSo == null) return player.Hands == null;
            return _stationSo.InputSo == player.Hands.Item;
        }

        public string GetInteractionName(PlayerController player)
        {
            return _stationSo.StationName;
        }
    }
}
