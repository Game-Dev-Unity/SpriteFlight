using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float minSize = 0.5f;
    private float maxSize = 2f;
    private float minSpeed = 50f;
    private float maxSpeed = 150f;
    private float maxSpinSpeed = 10f;
    public GameObject Effects;

    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Vector2 randomDirection = Random.insideUnitCircle;
        float randomSize = Random.Range(minSize, maxSize);
        float randomSpeed = Random.Range(minSpeed, maxSpeed) / randomSize;
        float randomTorque = Random.Range(-maxSpinSpeed, maxSpinSpeed);
        rb = GetComponent<Rigidbody2D>();
        

        transform.localScale = new Vector3(randomSize, randomSize, 1);
        rb.AddForce(randomDirection * randomSpeed);
        rb.AddTorque(randomTorque);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 contactPoint = other.GetContact(0).point;
        GameObject bounceEffect = Instantiate(Effects, contactPoint, Quaternion.identity);
        Destroy(bounceEffect, 0.1f);
    }
}
