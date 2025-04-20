using UnityEngine;

public class Sheep : MonoBehaviour
{

    public float runSpeed;
    public float gotHayDestroyDelay;
    private bool hitByHay;

    public float dropDestroyDelay;
    private Collider myCollider;
    private Rigidbody myRigidbody;

    private SheepSpawner sheepSpawner;

    public float heartOffset;
    public GameObject heartPrefab;

    // Start is called before the first frame update
    void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hay") && !hitByHay)
        {
            Destroy(other.gameObject);
            HitByHay();
        }
        else if (other.CompareTag("DropSheep"))
        {
            Drop();
        }

    }

    private void HitByHay()
    {
        GameStateManager.Instance.SavedSheep();

        SoundManager.Instance.PlaySheepHitClip();

        sheepSpawner.RemoveSheepFromList(gameObject);
        // Make sheep spawn 1% faster after each save
        sheepSpawner.timeBetweenSpawns *= 0.99f;

        hitByHay = true;
        runSpeed = 0;

        Destroy(gameObject, gotHayDestroyDelay);

        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity);
        TweenScale tweenScale = gameObject.AddComponent<TweenScale>();
        tweenScale.targetScale = 0;
        tweenScale.timeToReachTarget = gotHayDestroyDelay;
    }

    private void Drop()
    {
        GameStateManager.Instance.DroppedSheep();

        SoundManager.Instance.PlaySheepDroppedClip();

        sheepSpawner.RemoveSheepFromList(gameObject);
        // Whenever we dròp a sheep, increase a bit time btwn spawns to give player time to recover.
        sheepSpawner.timeBetweenSpawns += 0.5f;

        // do screen shake
        ScreenShake.Instance.StartShake(0.75f, 0.3f);

        myRigidbody.isKinematic = false;
        myCollider.isTrigger = false;
        Destroy(gameObject, dropDestroyDelay);
    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }

}
