using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScooterController : MonoBehaviour
{
    public float coolTimeInSec = 5;
    GameObject player;
    float timer;

    ItemController itemController;
    GameObject spawnArea;
    Animator playerAnim;
    RonyControls ronyControls; 
    public int speedIncrease = 5;
    bool activated = false;
 
    public AudioSource consumedSound;
    // Start is called before the first frame update
    void Awake() {
        player = GameObject.FindGameObjectWithTag("Player");
        ronyControls = player.GetComponent<RonyControls>();

        spawnArea = GameObject.FindGameObjectWithTag("SpawnArea");
        itemController = spawnArea.GetComponent<ItemController>();
    }

    void Update()
    {
        timer += Time.time;

    }

    //OnTriggerEnter lets you collide without collision because something weird is happening with collision
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Destroy(this.gameObject);
            itemController.respawn(coolTimeInSec, ItemController.ITEM_TYPE_SCOOTER);
            ronyControls.increaseSpeed(speedIncrease, coolTimeInSec);
            consumedSound.Play();
        }
    }

}
