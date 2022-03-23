using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Vizex;

namespace Vizex.Line
{
    public class CreateLine : MonoBehaviour
    {
        private Vector3 _startPos;
        private Vector3 _endPos;
        private GameObject _startPoint;
        private GameObject _endPoint;
        private LineRenderer _line;

        void Start() 
        {
            _startPos = Main.startPosition;
            _endPos = Main.endPosition;
            for (int i = 0; i < Main.objectConnectors.Length; i++)
            {
                if (Main.objectConnectors[i].transform.position == _startPos)
                {
                    _startPoint = Main.objectConnectors[i];
                }

                if (Main.objectConnectors[i].transform.position == _endPos)
                {
                    _endPoint = Main.objectConnectors[i];
                }
            }
            GetComponent<LineRenderer>().SetPosition(0, _startPos);
            GetComponent<LineRenderer>().SetPosition(1, _endPos);
            _line = GetComponent<LineRenderer>();

        }

        void LateUpdate() //Сохраняем линию между коннекторами в случае их перемещения
        {
            _line.SetPosition(0, _startPoint.transform.position);
            _line.SetPosition(1, _endPoint.transform.position);
        }
    }
}