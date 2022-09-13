using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{

    [SerializeField] private new Camera camera;
    [SerializeField] private GameObject preview;
    [SerializeField] private LayerMask floor;
    [SerializeField] private LayerMask house;
    private int houseLayer;


    private Vector3 cursorTarget;
    [SerializeField] private Vector3 houseBounds;
    private bool overGround = false;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        houseLayer = ((int)Mathf.Log(house, 2));
    }

    // Update is called once per frame
    void Update()
    {
        ScanPosition();
        HandleCursor();


    }
    void ScanPosition()
    {
        overGround = Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out hit, 100f, floor);
        if (overGround)
        {
            
            cursorTarget = hit.point;
            cursorTarget.x = Mathf.RoundToInt(cursorTarget.x);
            cursorTarget.y += 1f;
            cursorTarget.z = Mathf.RoundToInt(cursorTarget.z);
            preview.transform.position = cursorTarget;

        }

    }
    void HandleCursor()
    {
        
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Collider[] obsticles = Physics.OverlapBox(cursorTarget, houseBounds,preview.transform.rotation,house);
            Debug.Log("Recognized");
            if (overGround && obsticles.Length == 0)
            {
                preview.GetComponent<BoxCollider>().enabled = true;
                
                Instantiate(preview, preview.transform.position, preview.transform.rotation).layer = houseLayer;
                preview.GetComponent<BoxCollider>().enabled = false;

                Debug.Log("Done");
                return;
               
            }
             Debug.Log("can't Build here " + obsticles.Length +" " + house.value + " " + houseLayer);
        }
    }

}
