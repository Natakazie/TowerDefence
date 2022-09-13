using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder : MonoBehaviour
{

    [SerializeField] private new Camera camera;
    [SerializeField] private GameObject[] loadout;
    [SerializeField] private LayerMask floor;
    [SerializeField] private LayerMask house;
    private int selected = -1;

    private int houseLayer;


    private Vector3 cursorTarget;
    [SerializeField] private Vector3 houseBounds;
    private bool overGround = false;
    private RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        houseLayer = ((int)Mathf.Log(house, 2));
        for (int i = 0; i < loadout.Length; i++)
        {
            loadout[i] = Instantiate(loadout[i], Vector3.zero, Quaternion.identity);
            loadout[i].SetActive(false);
        }
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
        if (overGround && selected >=0)
        {

            cursorTarget = hit.point;
            cursorTarget.x = Mathf.RoundToInt(cursorTarget.x);
            cursorTarget.y += 0.3f;
            cursorTarget.z = Mathf.RoundToInt(cursorTarget.z);
            loadout[selected].transform.position = cursorTarget;
        }

    }
    void HandleCursor()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0) && selected >= 0)
        {
            Collider[] obsticles = Physics.OverlapBox(cursorTarget, houseBounds, loadout[selected].transform.rotation, house);
            if (overGround && obsticles.Length == 0)
            {
                GameObject placed = Instantiate(loadout[selected], loadout[selected].transform.position, loadout[selected].transform.rotation);
                placed.layer = houseLayer;
                placed.GetComponent<BoxCollider>().enabled = true;
                placed.GetComponent<Unit>().OnCreate();
                return;
            }
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            loadout[selected].SetActive(false);
            selected = -1;

        }
    }
    public void Select(int build)
    {
        selected = build;
        loadout[selected].SetActive(true);
    }
}
