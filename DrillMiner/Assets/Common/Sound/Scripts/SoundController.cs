namespace Common.Sound
{
    using UnityEngine;
    public class SoundController : MonoBehaviour
    {
        public ETypeSoundBG typeSoundBG;
        [Header("SoundBg")] public SoundBg[] soundBgStore;
        [Header("SoundEff")] public SoundEff[] soundEffectStore;
        [Header("SoundSoundInterAction")] public SoundSoundInterAction[] soundInterActionStore;
        [Header("SoundSkill")] public SoundSkill[] soundSkillStore;
        [Header("SoundReply")] public SoundReply[] SoundReplyStore;
        [Header("SoundController")] public SoundUserController[] SoundControllerStore;

        void Awake()
        {
            if (SoundManage.instance)
                SoundManage.instance.soundController = this;
        }
        void Start()
        {
            SoundManage.instance.PlayBGM(typeSoundBG);
        }
    }
}
