using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

using System.Collections;

public class UIRayCast : MonoBehaviour {

    public List<RaycastResult> Raycast()
    {
        //Code to be place in a MonoBehaviour with a GraphicRaycaster component
        GraphicRaycaster gr = this.GetComponent<GraphicRaycaster>();
        //Create the PointerEventData with null for the EventSystem
        PointerEventData ped = new PointerEventData(null);
        //Set required parameters, in this case, mouse position
        ped.position = Input.mousePosition;
        //Create list to receive all results
        List<RaycastResult> results = new List<RaycastResult>();
        //Raycast it
        gr.Raycast(ped, results);
        return results;
    }
}
