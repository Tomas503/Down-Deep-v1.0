using UnityEngine;
using System.Collections;

public class InteractiveObject : MonoBehaviour {
	
	public enum eInteractiveState{
		Active, //Open
		Inactive, //Close
	}

	private eInteractiveState m_state;

	void Start()
	{
		m_state = eInteractiveState.Inactive;
		}

	public void TriggerInteraction(){

		if (!animation.isPlaying) {
			Debug.Log ("intaractive object");
			switch (m_state) {
				case eInteractiveState.Active:
					animation.Play("CloseDoor");
					m_state = eInteractiveState.Inactive;
					break;

				case eInteractiveState.Inactive:
					animation.Play("OpenDoor");
					m_state = eInteractiveState.Active;
					break;

				default:
					break;

			}
		}

	}
}
