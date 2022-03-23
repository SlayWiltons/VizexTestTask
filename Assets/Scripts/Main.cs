using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Vizex
{
    public class Main : MonoBehaviour
    {
        [SerializeField] private int _count;
        public float Radius = 10;
        public static GameObject[] objectConnectors;
        public static Vector3 startPosition;
        public static Vector3 endPosition;
        
        private void Awake()
        {
            objectConnectors = new GameObject[_count];
        }

        public static void DefaultColor()   //Окраска коннеккторов в начальный цвет
        {
            for (int i = 0; i < objectConnectors.Length; i++)
            {
                objectConnectors[i].GetComponent<MeshRenderer>().material.color = Color.white;
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                StartCoroutine(Canceler());
            }
        }

        private IEnumerator Canceler()  //Проверка миссклика
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit) || (Physics.Raycast(ray, out hit) && hit.transform.name != "Connector"))
            {
                DefaultColor();
            }
            yield break;
        }

    }
}