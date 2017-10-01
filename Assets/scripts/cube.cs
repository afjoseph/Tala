using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/**
 * - Get an ideal rotation
 * - Get a random rotation on two axis and set the cube to be so at the beginning
 * - The sliders would have a range to figure out those two axis
 */
public class cube : MonoBehaviour {
    private float idealX;
    private float idealY;
    private float idealZ;
    public bool didUnlock = false;
    public Slider slider;
    public Color designatedColor;

    private float epsillon = 30;
    public Text cubeScore;

    void Start () {
		// Assign cube score
        cubeScore.text = "0.00";

		// Get random rotation
        idealX = Random.Range (0, 360);
        idealY = Random.Range (0, 360);
        idealZ = Random.Range (0, 360);

		// find a current rotation that's far enough from the ideal one
        var currentY = Random.Range (0, 360);
        while (Mathf.Abs (currentY - idealY) < epsillon) {
            currentY = Random.Range (0, 360);
        }

        transform.eulerAngles = new Vector3 (idealX, currentY, idealZ);
        slider.value = currentY;
        Debug.Log ("IdealY: " + idealY);
    }

    //Slider is now the value of Y. Just compare it
    public void onSliderChanged (float value) {
        transform.eulerAngles = new Vector3 (idealX, value, idealZ);
        var delta = Mathf.Abs (value - idealY);
        //lerp color
        var avgPoint = ((idealY - epsillon) + (idealY + epsillon)) / 2.0f;
        var colorDelta = (360f - Mathf.Abs (value - avgPoint)) / 360f;
        GetComponent<Renderer> ().material.color = Color.Lerp (Color.white, designatedColor, colorDelta);

		cubeScore.text = colorDelta.ToString("0.00");

        if (delta > epsillon) {
            Debug.Log ("delta: [" + delta + "]");
            didUnlock = false;
            return;
        }

        Debug.Log ("unlocked");
        didUnlock = true;
    }
}