using UnityEngine;
using UnityEngine.UI;

public class ConesRigidCollision : MonoBehaviour {
    public static int s_conesHitCounter = 0;
    static Text conesHitCounter;
    bool isHit = false;

	// Use this for initialization
	void Start () {
        conesHitCounter = GameObject.Find("ConesHitCounter").GetComponent<Text>();
	}

    void Update()
    {
        conesHitCounter.text = (s_conesHitCounter).ToString();
    }

    void OnCollisionExit(Collision col)
    {
        if (!isHit)
            s_conesHitCounter++;
        isHit = true;
    }
}
