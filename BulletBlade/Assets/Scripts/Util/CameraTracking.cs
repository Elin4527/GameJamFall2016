using UnityEngine;
using System.Collections;

public class CameraTracking : MonoBehaviour {

    public enum CameraType
    {
        INSTANT, DECAY_PAN, TIME_DECAY_PAN, CONSTANT_PAN, TIME_CONSTANT_PAN
    }
    public BaseCharacter player;
    public CameraType panType;
    public float SPEED = 1;
    public float TIME = 1;
    public float zOffset = -1;
    float moveSpeed;
    float acceleration;
    Vector3 destination;
    Vector3 d;
    Player p;
    

	// Use this for initialization
	void Start () {
        p = player.GetComponent<Player>();
    }

    void Update()
    {
        if (p != null && !p.gameObject.activeSelf)
        {
            if (transform.position == destination && p.lives != 0)
            {
                p.gameObject.SetActive(true);
                p.GetComponent<Player>().setInvulnerable(1.5f, true);
                p.mana = Mathf.Max(p.mana, p.maxMana / 2);
            }
        }
    }
	
	// Update is called once per frame
	void LateUpdate () {
        setCameraDestination(player.cameraPos);

        if(panType == CameraType.INSTANT)
            transform.position = destination;

        transform.position += moveSpeed * d.normalized * Time.deltaTime;
        moveSpeed -= acceleration * Time.deltaTime;

        if(Vector3.Dot((destination - transform.position), d) <= 0.0f || moveSpeed < 0.0f)
        {
            moveSpeed = 0.0f;
            acceleration = 0.0f;
            transform.position = destination;
        }
	}

    void setCameraPosition(Vector3 v)
    {
        v.z += zOffset;
        transform.position = destination = v;
        d = Vector3.zero;
        moveSpeed = 0.0f;
        acceleration = 0.0f;
    }

    void setCameraDestination(Vector3 v)
    {
        v.z += zOffset;
        if (v == destination) return;
        destination = v;
        d = v - transform.position;

        switch (panType)
        {
            case CameraType.CONSTANT_PAN:
                moveSpeed = SPEED;
                acceleration = 0.0f;
                break;
            case CameraType.TIME_CONSTANT_PAN:
                moveSpeed = d.magnitude / TIME;
                acceleration = 0.0f;
                break;
            case CameraType.DECAY_PAN:
                moveSpeed = SPEED * 2.0f;
                acceleration = (moveSpeed * moveSpeed) / (2 * (v - transform.position).magnitude);
                break;
            case CameraType.TIME_DECAY_PAN:
                moveSpeed = d.magnitude * 2.0f / TIME;
                acceleration = (moveSpeed * moveSpeed) / (2 * (v - transform.position).magnitude);
                break;
        }
        
    }
}
