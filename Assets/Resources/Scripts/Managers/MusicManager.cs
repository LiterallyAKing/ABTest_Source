using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.Audio;
using DG.Tweening;

public class MusicManager : MonoBehaviour {
	
	public AudioMixer mainMixer;
	public AudioMixerGroup musicGroup;
	public AudioMixerGroup sfxGroup;
	public AudioClip[] Songs;
	private AudioSource[] SongDatabase;

	void Start(){
		createDatabase (ref SongDatabase, ref Songs, musicGroup);
	}
		
	private void createDatabase(ref AudioSource[] audios, ref AudioClip[] clips, AudioMixerGroup mixergroup){
		audios = new AudioSource[clips.Length];
		for (int i = 0; i < clips.Length; i++) {
			GameObject child = new GameObject ("Player_" + i);
			child.transform.parent = gameObject.transform;
			audios [i] = child.AddComponent<AudioSource> () as AudioSource;
			audios [i].clip = clips [i];
			//audios [i].mute = true;
			audios [i].playOnAwake = false;
			audios [i].loop = true;
			audios [i].outputAudioMixerGroup = mixergroup;
		}
	}


	public void PlaySong(string songname){
		int i = GetSongIndex (songname);
		PlaySong (i);
	}
	public void PlaySong(int songindex){
		SongDatabase [songindex].Play ();
	}
	public void PauseSong(string songname){
		int i = GetSongIndex (songname);
		PauseSong (i);
	}
	public void PauseSong(int songindex){
		SongDatabase [songindex].Pause ();
	}
	public void StopSong(string songname){
		int i = GetSongIndex (songname);
		StopSong (i);
	}
	public void StopSong(int songindex){
		SongDatabase [songindex].Stop ();
	}
	public void SetSongVolume(string songname, float vol){
		int i = GetSongIndex (songname);
		SetSongVolume (i, vol);
	}
	public void SetSongVolume(int songindex, float vol){
		SongDatabase [songindex].volume = vol;
	}
	public void SongFadeIn(string songname, float transitionTime){
		int i = GetSongIndex (songname);
		FadeIn (ref SongDatabase [i], transitionTime);
	}
	public void SongFadeIn(int songindex, float transitionTime){
		FadeIn (ref SongDatabase [songindex], transitionTime);
	}
	public void SongFadeOut(string songname, float transitionTime){
		int i = GetSongIndex (songname);
		FadeOut (ref SongDatabase [i], transitionTime);
	}
	public void SongFadeOut(int songindex, float transitionTime){
		FadeOut (ref SongDatabase [songindex], transitionTime);
	}
	public void SongCrossFade(string songFrom, string songTo, float transitionTime){
		int songA = GetSongIndex (songFrom);
		int songB = GetSongIndex (songTo);
		SongCrossFade (songA,songB, transitionTime);
	}
	public void SongCrossFade(int songFrom, int songTo, float transitionTime){
		CrossFade (ref SongDatabase [songFrom], ref SongDatabase [songTo], transitionTime);
	}




	private void FadeIn(ref AudioSource aud, float dur){
		aud.Play ();
		aud.volume = 0;
		aud.DOFade (1f, dur);
	}
	private void FadeOut(ref AudioSource aud, float dur){
		aud.volume = 1f;
		aud.DOFade (0f, dur);
	}
	private void CrossFade(ref AudioSource song1, ref AudioSource song2, float dur){
		FadeOut (ref song1, dur);
		FadeIn (ref song2, dur);
	}


	private int GetSongIndex(string songname){
		for (int i = 0; i < SongDatabase.Length; i++) {
			if (SongDatabase [i].clip.name == songname) {
				return i;
			}
		}
		return 0;
	}


}
