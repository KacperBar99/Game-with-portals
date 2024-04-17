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
    private float startByEndScale;
    private bool startEntry;

    // Start is called before the first frame update
    void Start()
    {
        this.tunnelDistance = Vector3.Distance(this.start.transform.position, this.end.transform.position);
        this.player = GameObject.FindGameObjectWithTag("Player");
        this.startByEndScale = this.start.transform.localScale.y / this.end.transform.localScale.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (inside)
        {
            float scaleMultiplier;
            if (startEntry)
            {
                float distanceToEnd = Vector3.Distance(this.end.transform.position, this.player.transform.position);
                scaleMultiplier = distanceToEnd / this.tunnelDistance;
                this.player.GetComponent<MyController>().setNewScale(Mathf.Lerp(this.initialScale/this.startByEndScale,this.initialScale,scaleMultiplier));
            }
            else
            {
                float distanceToStart = Vector3.Distance(this.start.transform.position,this.player.transform.position);
                scaleMultiplier = distanceToStart / this.tunnelDistance;
                this.player.GetComponent<MyController>().setNewScale(Mathf.Lerp(this.initialScale, this.initialScale*this.startByEndScale,1-scaleMultiplier));
            }
            
        }
    }
    public void setInside(bool isStart)
    {
        if (!inside)
        {
            this.inside = true;
            this.initialScale = this.player.transform.localScale.y;
            this.startEntry = isStart;
        }
        else
        {
            this.inside = false;
        }
    }
}
