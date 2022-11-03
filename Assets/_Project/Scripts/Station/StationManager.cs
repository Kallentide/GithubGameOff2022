using DG.Tweening;
using GithubGameOff2022.Player;
using GithubGameOff2022.Prop;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameOff2022.Station
{
    public class StationManager : MonoBehaviour,IInteractible
    { 
        
        [Tooltip("Add to station scriptable here ")]
        [Space][SerializeField] private StationSo stationSo;
        [Space] [SerializeField] private Image stationIcon;
        [Space] [SerializeField] private Image productionFillImage;
        [Space] [SerializeField] private TMP_Text progressText;
        [Space] [SerializeField] private TMP_Text stationName;
        
        private bool _isBusy;
        private bool _canInteract;

        [Space] [SerializeField] private Transform stackTransform;


        private Tween _tween;

        private void Start()
        {
            stationName.text = stationSo.stationName;
            
        }

        private void Update()
        {
            if (_canInteract)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    ProduceItem();
                }
            }
         
        }


        private void ProduceItem()
        {

            if (_isBusy)
            {
                return;
            }
            
            
            _isBusy = true;
            var targetValue = 1f;
            productionFillImage.fillAmount = 0f;
            var currentValue = productionFillImage.fillAmount;
            _tween = DOTween.To(() => currentValue,
                    setter: x => productionFillImage.fillAmount = x, targetValue, stationSo.craftingDuration)
                .OnUpdate(
                    () =>
                    {

                        var value = (productionFillImage.fillAmount * 100);
                        progressText.text = value.ToString("F1") + " %";


                    }).OnComplete(() =>
                {

                    productionFillImage.fillAmount = 0f;
                    _isBusy = false;
                    progressText.text = "";
                    InstantiateItem();
                    _tween.Kill();

                }).SetUpdate(true);
        }

        private void InstantiateItem()
        {
            var tempObject = Instantiate(stationSo.outputSo.item);
            tempObject.transform.position = stackTransform.position;
        }

        public void DoAction(PlayerController player)
        {
            Debug.Log("interacting with station");
            ProduceItem();
        }

        public bool CanInterract(PlayerController player)
        {
            return !_isBusy;
        }

        public string GetInteractionName(PlayerController player)
        {
            return stationSo.stationName;
        }
    }
}
