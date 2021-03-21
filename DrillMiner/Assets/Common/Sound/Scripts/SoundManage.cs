using UnityEngine;
using UnityEngine.Audio;
using Common.Sound;
using Common.Singleton;

public class SoundManage : ExSingleton<SoundManage>
{
    public SoundController soundController;
    [Header("SourceSound")]
    [SerializeField] AudioSource[] _sources;
    [Header("Mixer")]
    [SerializeField] AudioMixer _mainMixer;

    #region PlayBGM
    public void PlayBGM(GetSoundBg bgm) => _sources[0].SetSound(soundController.soundBgStore, bgm.typeName, bgm.mode);
    public void PlayBGM(ETypeSoundBG bgm, ETypeMode mode = ETypeMode.Normal, int slot = 0) => _sources[0].SetSound(soundController.soundBgStore, bgm, mode, slot);
    #endregion

    #region PlayEffect
    public void PlayEffect(GetSoundEff eff) => _sources[1].SetSound(soundController.soundEffectStore, eff.typeName, eff.mode);
    public void PlayEffect(ETypeSoundEffect eff, ETypeMode mode = ETypeMode.Normal, int slot = 0) => _sources[1].SetSound(soundController.soundEffectStore, eff, mode, slot);
    #endregion

    #region PlayEffectInterAction
    public void PlayEffectInterAction(GetSoundSoundInterAction trigger) => _sources[2].SetSound(soundController.soundInterActionStore, trigger.typeName, trigger.mode);
    public void PlayEffectInterAction(ETypeSoundInterAction trigger, ETypeMode mode = ETypeMode.Normal, int slot = 0) => _sources[2].SetSound(soundController.soundInterActionStore, trigger, mode, slot);
    #endregion

    #region PlayEffectSkill
    public void PlayEffectSkill(GetSoundSkill skill) => _sources[3].SetSound(soundController.soundSkillStore, skill.typeName, skill.mode);
    public void PlayEffectSkill(ETypeSoundSkill skill, ETypeMode mode = ETypeMode.Normal, int slot = 0) => _sources[3].SetSound(soundController.soundSkillStore, skill, mode, slot);
    #endregion

    #region PlayEffectReply
    public void PlayEffectReply(GetSoundReply reply) => _sources[4].SetSound(soundController.SoundReplyStore, reply.typeName, reply.mode);
    public void PlayEffectReply(ETypeSoundReply reply, ETypeMode mode = ETypeMode.Normal, int slot = 0) => _sources[4].SetSound(soundController.SoundReplyStore, reply, mode, slot);
    #endregion

    #region PlayEffectController
    public void PlayEffectController(GetSoundController controller) => _sources[5].SetSound(soundController.SoundControllerStore, controller.typeName, controller.mode);
    public void PlayEffectController(ETypeSoundController controller, ETypeMode mode = ETypeMode.Normal, int slot = 0) => _sources[5].SetSound(soundController.SoundControllerStore, controller, mode, slot);
    #endregion

    public void SetAllMute(bool isActive) => System.Array.ForEach(_sources, item => item.mute = isActive);
}

#region OutterClass
public abstract class BaseDetailSound<T>
{
    public T typeName;
    public AudioClip[] soundClip;
}

[System.Serializable] public class SoundBg : BaseDetailSound<ETypeSoundBG> { }
[System.Serializable] public class SoundEff : BaseDetailSound<ETypeSoundEffect> { }
[System.Serializable] public class SoundSoundInterAction : BaseDetailSound<ETypeSoundInterAction> { }
[System.Serializable] public class SoundSkill : BaseDetailSound<ETypeSoundSkill> { }
[System.Serializable] public class SoundReply : BaseDetailSound<ETypeSoundReply> { }
[System.Serializable] public class SoundUserController : BaseDetailSound<ETypeSoundController> { }

public abstract class BaseGetSound<T>
{
    public T typeName;
    public ETypeMode mode;
    public int selectionSlot;
}
[System.Serializable] public class GetSoundBg : BaseGetSound<ETypeSoundBG> { }
[System.Serializable] public class GetSoundEff : BaseGetSound<ETypeSoundEffect> { }
[System.Serializable] public class GetSoundSoundInterAction : BaseGetSound<ETypeSoundInterAction> { }
[System.Serializable] public class GetSoundSkill : BaseGetSound<ETypeSoundSkill> { }
[System.Serializable] public class GetSoundReply : BaseGetSound<ETypeSoundReply> { }
[System.Serializable] public class GetSoundController : BaseGetSound<ETypeSoundController> { }
#endregion
