namespace Common.Sound
{
    using System.Collections;
    using System.Linq;
    using UnityEngine;

    public static class ExSound
    {
        public static bool isCheck;
        public static bool isFadeIn, isFadeOut;

        public static bool CheckNull(this object victim) => (victim != null) ? true : false;
        public static void SetSound<T>(this AudioSource victim, BaseDetailSound<T>[] store, T types, ETypeMode typeMode = ETypeMode.Normal, int slot = 0)
        {
            AudioClip _detail;

            #region Codition
            var _value = System.Tuple.Create(typeMode, slot);
            switch (_value)
            {
                case System.Tuple<ETypeMode, int> normal when (normal.Item1.Equals(ETypeMode.Normal) && normal.Item2 == 0):
                    _detail = CheckSoundNull(store, types);
                    break;
                case System.Tuple<ETypeMode, int> random when (random.Item1.Equals(ETypeMode.Random) && random.Item2 == 0):
                    _detail = CheckSoundRandomNull(store, types);
                    break;
                default:
                    _detail = CheckSoundSelectNull(store, types, slot);
                    break;
            }
            #endregion

            if (_detail.CheckNull())
            {
                victim.clip = _detail;
                victim.volume = 1f;
                victim.Play();
            }

        }

        #region FadeSound
        public static IEnumerator FadeIn(this AudioSource victim, AudioClip sound, float speed = .1f, float maxVolume = 1)
        {
            victim.clip = sound;
            victim.Play();

            isFadeIn = !isCheck;
            isFadeOut = isCheck;

            victim.volume = 0;
            while (victim.volume < maxVolume && isFadeIn)
            {
                victim.volume += speed;
                yield return null;
            }
        }
        public static IEnumerator FadeOut(this AudioSource victim, float speed = .1f)
        {
            isFadeIn = isCheck;
            isFadeOut = !isCheck;
            while (victim.volume >= speed && isFadeOut)
            {
                victim.volume -= speed;
                yield return null;
            }
        }
        #endregion

        #region CheckValue
        // Normal //
        public static AudioClip CheckSoundNull<T>(BaseDetailSound<T>[] store, T types)
        {
            if (store.Count(item => item.typeName.Equals(types)) > 0)
            {
                var _store = store.Where(item => item.typeName.Equals(types)).First();
                return _store.soundClip.Length > 0 ? _store.soundClip.First() : default;
            }
            Debug.LogWarning($"Don't Have Sound");
            return default;
        }
        // Random //
        public static AudioClip CheckSoundRandomNull<T>(BaseDetailSound<T>[] store, T types)
        {
            if (store.Count(item => item.typeName.Equals(types)) > 0)
            {
                var _store = store.Where(item => item.typeName.Equals(types)).First();
                return _store.soundClip.Length > 0 ? _store.soundClip[UnityEngine.Random.Range(0, _store.soundClip.Length)] : default;
            }
            Debug.LogWarning($"Don't Have Sound");
            return default;
        }
        // Select //
        public static AudioClip CheckSoundSelectNull<T>(BaseDetailSound<T>[] store, T types, int slot)
        {
            if (store.Count(item => item.typeName.Equals(types)) > 0)
            {
                var _store = store.Where(item => item.typeName.Equals(types)).First();
                if (_store.soundClip.Length > 0 && _store.soundClip.Length - 1 >= slot)
                {
                    return _store.soundClip[slot];
                }
                Debug.LogWarning($"EmplySlot: _store.soundClip[{slot}]");
                return default;
            }
            Debug.LogWarning($"Don't Have Sound");
            return default;
        }
        #endregion
    }
}
