using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SesYoneticisi : MonoBehaviour
{
    public Ses[] sounds;
    void Start()
    {
        foreach(Ses s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
        }

        PlaySound("OyunSesi");
    }


    public void PlaySound(string name)
    {
         foreach(Ses s in sounds)
        {
            if(s.name == name)
                s.source.Play();
        }
    }
}
