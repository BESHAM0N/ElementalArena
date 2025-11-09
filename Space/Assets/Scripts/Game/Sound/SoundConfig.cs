using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceGame
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "Prototypes/Sound Config")]
    public class SoundConfig : ScriptableObject
    {
       public List<Sound> Config;
    }
    
    [Serializable]
    public class Sound
    {
        public SourceType SourceType;
        public SoundType SoundType;
        public AudioClip AudioClip;
    }
}