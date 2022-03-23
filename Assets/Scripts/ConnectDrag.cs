using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Vizex;

namespace Vizex.Connect
{
    public class ConnectDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private GameObject _linePrefab;
        private Camera _camera;
        private LineRenderer lineRend;
        private Vector3 screenPoint;
        private GameObject objDragThrough;
        private bool isDragging;

        void Start()
        {
            _camera = Camera.main;
            lineRend = GameObject.Find("DraggingLine").GetComponent<LineRenderer>();
            screenPoint = _camera.WorldToScreenPoint(transform.position);
            isDragging = false;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Main.startPosition = transform.position;
            if (gameObject.GetComponent<MeshRenderer>().material.color == Color.yellow)
            {
                isDragging = true;
            }

            if (gameObject.GetComponent<MeshRenderer>().material.color == Color.white)
            {
                isDragging = false;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (isDragging == true)
            {
                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    for (int i = 0; i < Main.objectConnectors.Length; i++)
                    {
                        if (hit.transform.position != Main.startPosition &&
                            hit.transform.position == Main.objectConnectors[i].transform.position)
                        {
                            objDragThrough = hit.transform.gameObject;
                            if (hit.transform.gameObject.GetComponent<MeshRenderer>().material.color == Color.blue)
                            {
                                hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
                            }
                        }
                    }

                }

                if (!Physics.Raycast(ray, out hit) || hit.transform.gameObject != objDragThrough)
                {
                    for (int i = 0; i < Main.objectConnectors.Length; i++)
                    {
                        if (Main.objectConnectors[i].GetComponent<MeshRenderer>().material.color == Color.yellow &&
                            Main.objectConnectors[i] == objDragThrough)
                        {
                            objDragThrough.GetComponent<MeshRenderer>().material.color = Color.blue;
                        }
                    }
                }

                Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
                Vector3 curPosition = _camera.ScreenToWorldPoint(curScreenPoint);
                lineRend.SetPosition(0, Main.startPosition);
                lineRend.SetPosition(1, curPosition);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (isDragging == true)
            {
                RaycastHit hit;
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.transform.gameObject.name == "Connector" && hit.transform.gameObject != gameObject)
                    {
                        Main.endPosition = hit.transform.gameObject.transform.position;
                        Instantiate(_linePrefab);
                        Main.DefaultColor();
                    }
                    else
                    {
                        Main.DefaultColor();
                    }
                }
                else
                {
                    Main.DefaultColor();
                }
                lineRend.SetPosition(0, new Vector3(0, 0, 0));
                lineRend.SetPosition(1, new Vector3(0, 0, 0));
                isDragging = false;
            }
        }
    }
}
