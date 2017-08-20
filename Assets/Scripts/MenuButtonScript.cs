using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonScript : MonoBehaviour {

	public void PlayButton() {
        GameLogic.Instance.LoadIntro();
    }
}
