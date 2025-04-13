using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEventHelper : MonoBehaviour
{
    public UnityEvent OnAttackPerformed;
    public UnityEvent Reload;
    public UnityEvent AnimationScene;

    public void TriggerAttack()
    {
        OnAttackPerformed?.Invoke();
    }

    public void Reloading()
    {
        Reload?.Invoke();
    }

    public void Scene()
    {
        AnimationScene?.Invoke();
    }
}
