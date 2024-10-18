using UnityEngine;

[RequireComponent(typeof(BoundsCheck))]
public class Asteroid : MonoBehaviour
{
    public GameObject asteroidPrefab;
    public float spawnRate = 1f;       
    public float spawnHeight = 6f;    
    public float minX = -8f, maxX = 8f;

    public float speed = 5f;           
    public float waveAmplitude = 3f;   
    public float waveFrequency = 2f;   
    public float rotationSpeed = 50f;  

    private float startingX;            
    private float time;
    private float direction = 1f;
    protected BoundsCheck bndCheck;

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    public Vector3 pos
    {
        get
        {
            return this.transform.position;
        }
        set
        {
            this.transform.position = value;
        }
    }

    private void Start()
    {
        startingX = Random.Range(minX, maxX);

        transform.position = new Vector3(startingX, spawnHeight, 0);

        time = 0f;
    }


    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.x -= speed * Time.deltaTime;
        pos = tempPos;
    }


    private void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;

        if (otherGO.GetComponent<ProjectileHero>() != null)
        {
            if (bndCheck.isOnScreen)
            {
                Destroy(this.gameObject);
            }
            
            Destroy(otherGO);

        }
        else
        {
            Debug.Log("Enemy hit by non-ProjectileHero: " + otherGO.name);
        }
    }

    void Update()
    {
        time += Time.deltaTime;

        float waveOffset = Mathf.Sin(time * waveFrequency) * waveAmplitude;

        transform.position = new Vector2(startingX + waveOffset * direction, transform.position.y - speed * Time.deltaTime);

        transform.Rotate(new Vector3(rotationSpeed * Time.deltaTime, rotationSpeed * Time.deltaTime, 0));

        if (bndCheck.LocIs(BoundsCheck.eScreenLocs.offDown))
        {
            Destroy(gameObject);
        }
    }

}