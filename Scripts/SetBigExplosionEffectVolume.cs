using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetBigExplosionEffectVolume : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GetComponent<AudioSource>().volume = GlobalVariable.SoundVolume;
    }
}
