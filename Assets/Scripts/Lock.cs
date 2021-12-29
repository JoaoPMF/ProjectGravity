using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Lock : MonoBehaviour
{
    public bool locked = true;

    public abstract void Unlock();

    public abstract void _Lock();

    public abstract void Check(float value);

    public abstract void Check(float value, PodController controller);
}
