using UnityEngine;

public class timeVariance : MonoBehaviour
{
    private float fixedDeltaTime;
    private float timeScale;
    // Start is called before the first frame update
    void Awake()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
        this.timeScale = Time.timeScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            this.slowDown();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            this.speedUp();
        }
    }
    public void slowDown()
    {
        Time.fixedDeltaTime = this.fixedDeltaTime * 0.0f;
        Time.timeScale = this.timeScale * 0.0f;
    }
    public void speedUp()
    {
        Time.fixedDeltaTime = this.fixedDeltaTime;
        Time.timeScale = this.timeScale;
    }
}
