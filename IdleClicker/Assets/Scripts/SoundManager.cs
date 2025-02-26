using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
   [SerializeField] private AudioSource clickSound;
   [SerializeField] private AudioSource buySound;
   
   [SerializeField] private Image soundImage;
   [SerializeField] private Sprite audioOn; 
   [SerializeField] private Sprite audioOff; 

   private bool soundState = true;
   
   public void ChangeVolumeState()
   {
      if (soundState)
         EnableSounds();
      else
         DisableSounds();
      
      soundState = !soundState;
   }
   
   private void EnableSounds()
   {
      clickSound.volume = 1f;
      buySound.volume = 1f;
      
      soundImage.sprite = audioOn;
   }
   
   private void DisableSounds()
   {
      clickSound.volume = 0f;
      buySound.volume = 0f;
      
      soundImage.sprite = audioOff;
   }
   
}
