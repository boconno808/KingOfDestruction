using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{

    public GameObject[] items;
    public static int ITEM_TYPE_SCOOTER = 0;
    public static int ITEM_TYPE_ENERGY_DRINK = 1;
    Vector3 position;
    Vector3 origin;
    Vector3 range;
    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position;
        range = transform.localScale / 2.0f;
        spawn(ITEM_TYPE_SCOOTER, 1);
        spawn(ITEM_TYPE_ENERGY_DRINK, 1);

    }

    public void spawn(int item_type, int num){
        for (int i = 0; i < num; i++)
        {

            Vector3 randomRange = new Vector3(Random.Range(-range.x, range.x),
                                              origin.y,
                                              Random.Range(-range.z, range.z));

            Vector3 randomCoordinate = origin + randomRange;
            
            var obj = Instantiate(items[item_type], randomCoordinate, transform.rotation);
        }

    }

    IEnumerator waitForCoolTime(float coolTime, int gameType)
    {
        yield return new WaitForSeconds(coolTime);
        spawn(gameType, 1); 
    }

    public void respawn(float coolTime, int gameType)
    {
        StartCoroutine(waitForCoolTime(coolTime,gameType));

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
