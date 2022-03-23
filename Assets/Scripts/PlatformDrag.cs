using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Vizex;

namespace Vizex.Platform
{
    public class PlatformDrag : MonoBehaviour, IBeginDragHandler, IDragHandler
    {
        [SerializeField] private GameObject _original;
        private Vector3 _screenPoint;
        private Vector3 _offset;
        private Camera _camera;

        private void Start()
        {
            _camera = Camera.main;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _screenPoint = _camera.WorldToScreenPoint(_original.transform.position);
            _offset = _original.transform.position -
                      _camera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                          _screenPoint.z));
            for (int i = 0; i < Main.objectConnectors.Length; i++)
            {
                Main.objectConnectors[i].GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _screenPoint.z);
            Vector3 curPosition = _camera.ScreenToWorldPoint(curScreenPoint) + _offset;
            _original.transform.position = curPosition;
        }
    }
}