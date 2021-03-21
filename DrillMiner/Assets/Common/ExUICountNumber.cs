namespace Common.UI.CountNumber
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.UI;

    public static class ExUICountNumber
    {
        public static void SetCountNumber(this Text victim, MonoBehaviour mono, float targetVal, float duration, float delay = .5f, float defaultVal = 0, string prefix = "", string format = "")
        {
            mono.StartCoroutine(IESetCountNumber(victim, targetVal, duration, delay, defaultVal, prefix, format));
        }
        public static IEnumerator IESetCountNumber(this Text victim, float targetVal, float duration, float delay = .5f, float defaultVal = 0, string prefix = "", string format = "")
        {
            if (delay > 0)
            {
                yield return new WaitForSeconds(delay);
            }
            float _current = defaultVal;
            float _currentNonZero;
            while (_current < targetVal)
            {
                // ensure "current" is not getting a value of zero, resulting in an infinite loop
                _currentNonZero = (float)(targetVal * Time.deltaTime / duration);
                if (_currentNonZero == 0)
                {
                    _currentNonZero = 1;
                }
                // step by amount that will get us to the target value within the duration
                _current += _currentNonZero;
                _current = Mathf.Clamp(_current, 0, targetVal);
                victim.text = prefix + _current.ToString(format);
                yield return null;
            }
        }
        public static IEnumerator IESetCountExpNumber(this Text victim, Slider expSld, float maxExp, float rateUpmaxExp, float targetVal, float duration, System.Action<byte> callLvUP = null, float delay = .5f, float defaultVal = 0, string prefix = "", string format = "")
        {
            float _current = defaultVal;
            float _currentNonZero;
            float _parseSld = maxExp - _current;
            byte _countLvUp = 0;

            victim.text = $"{prefix}{_current.ToString(format)} / {maxExp}";

            if (delay > 0)
            {
                yield return new WaitForSeconds(delay);
            }
            while (_current < targetVal)
            {
                // ensure "current" is not getting a value of zero, resulting in an infinite loop
                _currentNonZero = (float)(targetVal * Time.deltaTime / duration);
                if (_currentNonZero == 0)
                {
                    _currentNonZero = 1;
                }
                // step by amount that will get us to the target value within the duration
                _current += _currentNonZero;
                _current = Mathf.Clamp(_current, 0, targetVal);
                victim.text = $"{prefix}{_current.ToString(format)} / {maxExp.ToString(format)}";
                if (_current > maxExp)
                {
                    maxExp *= rateUpmaxExp;
                    _parseSld = (maxExp - _current);
                    _countLvUp++;
                }
                else
                    expSld.value = ((_current - _parseSld) / _parseSld);
                yield return null;
            }
            if (_countLvUp > 0)
                callLvUP?.Invoke(_countLvUp);
        }
    }
}
