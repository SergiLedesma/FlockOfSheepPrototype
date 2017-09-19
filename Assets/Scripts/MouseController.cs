using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    RaycastHit2D hit;

    [SerializeField]
    public static ArrayList selectedUnits = new ArrayList();
    [SerializeField]
    private int selectedUnitsCount = 0;

    public static bool dragging = false;

    private static float timeBeforeDragging = 0.5f;
    private static float timeLeftBeforeDragging;
    private static Vector2 mouseDragStart;

    private static Vector3 mouseClickPoint;

    [SerializeField]
    private GameObject destinationArrow;
    private static Vector3 destination;

    void Start()
    {
        mouseClickPoint = Vector3.zero;
    }

    void Update()
    {
        hit = Physics2D.Raycast(new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y), Vector2.zero, 0.0f);
        if (hit)
        {
            if (Input.GetMouseButtonDown(0))
            {
                mouseClickPoint = hit.point;
                if (hit.collider.transform.FindChild("Selected"))
                {
                    if (!IsUnitSelected(hit.collider.transform.gameObject))
                    {
                        if (!ShiftKeyDown())
                        {
                            if (selectedUnits.Count > 0)
                            {
                                Deselect();
                            }
                        }
                        selectedUnits.Add(hit.collider.transform.gameObject);
                        hit.collider.transform.FindChild("Selected").gameObject.SetActive(true);
                    }
                    else
                    {
                        if (ShiftKeyDown())
                        {
                            RemoveUnitFromSelection(hit.collider.transform.gameObject);
                        }
                        else
                        {
                            if (selectedUnits.Count > 1)
                            {
                                Deselect();
                            }
                            selectedUnits.Add(hit.collider.transform.gameObject);
                            hit.collider.transform.FindChild("Selected").gameObject.SetActive(true);
                        }
                    }
                }
                else
                {
                    if (!ShiftKeyDown())
                    {
                        Deselect();
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                if (hit.collider.gameObject.CompareTag("Background"))
                {
                    for (int i = 0; i < selectedUnits.Count; ++i)
                    {
                        GameObject arrayUnit = selectedUnits[i] as GameObject;
                        arrayUnit.GetComponent<SheepController>().SetDestination(hit.point);
                    }
                    SpawnArrow(hit.point);
                }
            }
        }
        else if (Input.GetMouseButtonDown(0))
        {
            mouseClickPoint = Vector3.zero;
            if (!ShiftKeyDown())
            {
                Deselect();
            }
        }
        selectedUnitsCount = selectedUnits.Count;
    }

    public static void Deselect()
    {
        if (selectedUnits.Count > 0)
        {
            for (int i = 0; i < selectedUnits.Count; ++i)
            {
                GameObject arrayUnit = selectedUnits[i] as GameObject;
                arrayUnit.transform.FindChild("Selected").gameObject.SetActive(false);
            }
            selectedUnits.Clear();
        }
    }

    public static bool IsUnitSelected(GameObject unit)
    {
        bool selected = false;
        if (selectedUnits.Count > 0)
        {
            for (int i = 0; i < selectedUnits.Count; ++i)
            {
                GameObject arrayUnit = selectedUnits[i] as GameObject;
                if (arrayUnit == unit)
                {
                    selected = true;
                    break;
                }
            }
        }
        return selected;
    }

    public bool RemoveUnitFromSelection(GameObject unit)
    {
        bool removed = false;
        if (selectedUnits.Count > 0)
        {
            for (int i = 0; i < selectedUnits.Count; ++i)
            {
                GameObject arrayUnit = selectedUnits[i] as GameObject;
                if (arrayUnit == unit)
                {
                    selectedUnits.RemoveAt(i);
                    arrayUnit.transform.FindChild("Selected").gameObject.SetActive(false);
                    removed = true;
                    break;
                }
            }
        }
        return removed;
    }

    public static bool ShiftKeyDown()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void SpawnArrow(Vector3 dest)
    {
        GameObject instantiatedArrow = Instantiate(destinationArrow, dest, Quaternion.identity);
        instantiatedArrow.GetComponent<DestinationArrowController>().AutoDestroy();
    }
}
