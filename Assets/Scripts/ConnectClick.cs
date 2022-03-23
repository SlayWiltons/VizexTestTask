using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Vizex;

namespace Vizex.Connect
{
    public class ConnectClick : MonoBehaviour
    {
        [SerializeField] private GameObject _linePrefab;
        private Camera _camera;
        private Material _color;

        private void Start()
        {
            _camera = Camera.main;
            _color = GetComponent<MeshRenderer>().material;
        }

        public void OnMouseDown()
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name == "Connector")
                {
                    if (_color.color == Color.yellow)
                    {
                        Main.DefaultColor();
                    }
                    else if (_color.color == Color.white)
                    {
                        Main.startPosition = transform.position;
                        _color.color = Color.yellow;
                        for (int i = 0; i < Main.objectConnectors.Length; i++)
                        {
                            if (gameObject != Main.objectConnectors[i])
                            {
                                Main.objectConnectors[i].GetComponent<MeshRenderer>().material.color = Color.blue;
                            }
                        }
                    }
                    if (_color.color == Color.blue)
                    {
                        Main.endPosition = transform.position;
                        Instantiate(_linePrefab);
                        Main.DefaultColor();
                    }
                }
            }
        }
    }
}
