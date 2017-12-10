using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementScreen : MonoBehaviour {
    public void onClick_nextLevel()
    {
		CameraFade.GetInstance().FadeOut();
    }
}
