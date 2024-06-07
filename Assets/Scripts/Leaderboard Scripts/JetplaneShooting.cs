using UnityEngine;
using UnityEngine.UI;

public class JetplaneShooting : MonoBehaviour
{


    public GameObject bulletPrefab; // assign bullet prefab in the editor
    public Transform bulletInstantiatePoint;
    public float bulletSpeed = 10f;
    public float bulletLifetime = 3f; // in seconds
    public int maxBullets = 5; // maximum number of bullets the player can have
    public int currentBullets; // current number of bullets the player has
    private bool canShoot = false; // flag to indicate whether the player can shoot
    private GameObject bulletUIImages;

    public AudioSource audioSource;
    public AudioClip bulletAudioClip;
    public AudioClip blastSFX;
    private void Start()
    {
        currentBullets = 0;
        bulletUIImages = GameObject.FindGameObjectWithTag("bulletImageUI");

    }

    private void Update()
    {
        if (canShoot && Input.GetKeyDown(KeyCode.Space)) // Replace KeyCode.Space with the key you want to use for shooting
        {
            // Instantiate a new bullet prefab at the bullet instantiate point's position and rotation
            GameObject bullet = Instantiate(bulletPrefab, bulletInstantiatePoint.position, bulletInstantiatePoint.rotation);
            audioSource.PlayOneShot(bulletAudioClip);
            // Add force to the bullet in the direction the jetplane is facing
            Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(transform.forward * bulletSpeed, ForceMode.VelocityChange);

            // Destroy the bullet after a set amount of time
            Destroy(bullet, bulletLifetime);

            currentBullets--;
            if (bulletUIImages != null)
            {

                bulletUI bulletUIImagesr = bulletUIImages.GetComponent<bulletUI>();

                if (bulletUIImages != null)
                {
                    bulletUIImagesr.UpdateBullet(currentBullets);
                }
            }



            if (currentBullets == 0)
            {
                canShoot = false;
            }

            //if(currentBullets >= maxBullets){
            //    currentBullets = 0;
            ///}

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            audioSource.PlayOneShot(blastSFX);
            if (audioSource.isPlaying)
            {
                Debug.Log("Collides");
                Destroy(collision.gameObject); // destroy the obstacle game object when hit by a bullet
                Destroy(gameObject);
            }
             // destroy the bullet game object
            currentBullets--;
        }
    }

    public void ActivatePowerup()
    {
        GameObject UIActivate = GameObject.FindGameObjectWithTag("bulletImageUI");
        if (UIActivate != null)
        {
            // Enable all child objects of UIActivate
            foreach (Transform child in UIActivate.transform)
            {
                child.gameObject.GetComponent<Image>().enabled = true;
            }
        }
        Debug.Log("Activated Activated");
        currentBullets = 5; // set current bullets to 5
        canShoot = true;
    }

    public void CurrentBullets()
    {
        Debug.Log(currentBullets);
    }
}