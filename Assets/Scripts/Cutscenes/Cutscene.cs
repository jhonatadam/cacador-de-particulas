using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutscene : MonoBehaviour {

	public Animator[] animators;
	protected Player player;
	protected Animator playerAn;
	protected Animation animationPl;

	protected float timer = 0f;
	protected bool counting = false;
	protected bool updateOn = false;

	protected int scope = 0;

	public virtual void StartScene() { Execute(); }
	public virtual void Execute() { Exit(); }
	public virtual void Exit() {}


	public virtual void startTimer() {
		timer = Time.time;
		counting = true;
	}
	public virtual void stopTimer() {
		timer = Time.time;
		counting = false;
	}
	public virtual void pauseTimer() {
		Time.timeScale = 0;
	}

	public float getCutsceneTime() {
		return Time.time - timer;
	}
}