using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TEMP_Interact : MonoBehaviour
{
    public int interactablesLayer = 8;
    public float promptHeightAboveObject = 2;

    public BoxCollider2D interactionHitbox;
    public GameObject interactPrompt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("interactable in range");
        if (collision.gameObject.layer == interactablesLayer)
        {
            Vector3 promptPosition = new Vector3(   collision.transform.position.x,
                                                    collision.transform.position.y + promptHeightAboveObject,
                                                    collision.transform.position.z);

            Instantiate(interactPrompt, promptPosition, Quaternion.identity);
        }
    }
}
