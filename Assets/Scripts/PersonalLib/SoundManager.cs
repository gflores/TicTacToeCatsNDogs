using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	static public SoundManager instance;

	public float musicVolumeRatio = 1f;
	public float SFXVolumeRatio = 1f;

	public AudioSource cat_sound;
	public AudioSource dog_sound;

	public AudioSource mainMenuMusic;
	public AudioSource mainGameMusic;


	public AudioSource current_music_played {get; set;}

	public void ChangeVolumeForCurrentMusic(float newVolumeRatio){
		musicVolumeRatio = newVolumeRatio;
		current_music_played.volume = current_music_played.GetComponent<AudioWrapper>().initialVolume * musicVolumeRatio;
	}
	public void ChangeVolumeForSFX(float newVolumeRatio){
		SFXVolumeRatio = newVolumeRatio;
	}

	void Awake(){
		if (instance == null)
			instance = this;
		else
			Destroy (this.gameObject);

		DontDestroyOnLoad(this.gameObject) ;
	}
	
	public void ForcePlayMusic(AudioSource music){
		PlayMusic(music);
	}
	void PlayMusic(AudioSource music, float volume = 1f)
	{
		if (current_music_played != null)
			current_music_played.Stop();
		current_music_played = music;
//		music.volume = volume;
		music.volume = music.GetComponent<AudioWrapper> ().initialVolume * musicVolumeRatio;
		music.Play();
	}
	public void PlayIfDifferent(AudioSource music, float volume = 1f){
		if (current_music_played != music)
			PlayMusic(music, volume);
	}
	
	public void PauseAndMute()
	{
		AudioListener.pause = true;
	}
	public void UnpauseAndUnmute()
	{
		
		AudioListener.pause = false;
	}
	public void PlayIndependant(AudioSource sound)
	{
		sound.volume = sound.GetComponent<AudioWrapper> ().initialVolume * SFXVolumeRatio;
		sound.PlayOneShot(sound.clip, sound.volume);
		if (sound.pitch < 0)
			sound.time = sound.clip.length;
	}
	public IEnumerator LaunchVolumeFade(AudioSource music, float time, float from = 0f, float to = 1f)
	{
		float delta_volume = to - from;
		
		music.volume = from;
		if (time > 0)
		{
			for (float curr_t = 0; curr_t < time; curr_t += Time.deltaTime)
			{
				music.volume += delta_volume * (Time.deltaTime / time);
				yield return new WaitForSeconds(0.001f);
			}
		}
		music.volume = to;
	}
}
