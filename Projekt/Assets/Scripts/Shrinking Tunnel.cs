using UnityEngine;

public class ShrinkingTunnel : MonoBehaviour
{
    [SerializeField]
    private GameObject start;
    [SerializeField]
    private GameObject end;


    private GameObject player;
    private float tunnelDistance;
    private bool inside=false;
    private float initialScale=1.0f;
    private float scaleDifference;

    // Start is called before the first frame update
    void Start()
    {
        this.tunnelDistance = Vector3.Distance(this.start.transform.position, this.end.transform.position);
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.scaleDifference = this.start.transform.localScale.y / this.end.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (inside)
        {
            float distanceToEnd=Vector3.Distance(this.end.transform.position,this.player.transform.position);
            float scaleMultiplier = distanceToEnd / this.tunnelDistance;
            scaleMultiplier = initialScale-(this.initialScale / scaleDifference) - scaleMultiplier;
            this.player.GetComponent<MyController>().setNewScale(scaleMultiplier);
        }
    }

    public void setInside(bool isStart)
    {
        if (!inside)
        {
            this.inside = true;
            this.initialScale = this.player.transform.localScale.y;
        }
        else
        {
            this.inside = false;
        }
    }
}
