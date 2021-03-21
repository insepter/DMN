namespace Insepter.DM.Score
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using Common.UI.CountNumber;
    public class ScoreController : MonoBehaviour
    {
        public static float totalDepth = 1000, coinRecieve = 5000, dimondRecieve = 2000, expRecieve = 1000000;
        public GameObject lvUpPanel;

        ScoreUI _scoreUI;
        float _remainExp = 0, _maxExp = 1000, _presentLv = 0;
        void Awake()
        {
            _scoreUI = GetComponent<ScoreUI>();
        }
        IEnumerator Start()
        {
            _scoreUI.SetAllUI(("0", "0", $"{_remainExp} / {_maxExp}", $"{_presentLv}"));
            _scoreUI.SetExpValue(_remainExp / _maxExp);

            yield return StartCoroutine(_scoreUI.coinTxt.IESetCountNumber(coinRecieve, 1f, format: "N0"));
            yield return StartCoroutine(_scoreUI.depthTxt.IESetCountNumber(totalDepth, 1f, format: "N0"));
            yield return StartCoroutine(_scoreUI.expTxt.IESetCountExpNumber(_scoreUI.expSld, _maxExp, 2f, expRecieve, 1f, result =>
            {
                lvUpPanel.SetActive(true);
                StartCoroutine(_scoreUI.lvTxt.IESetCountNumber(result, 1f, delay: .1f, defaultVal: _presentLv, format: "N0"));
            }, defaultVal: _remainExp, format: "N0"));

        }
    }
}
