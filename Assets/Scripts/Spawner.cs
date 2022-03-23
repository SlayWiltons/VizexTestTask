using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Vizex
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject _prefab;
        [SerializeField] private Main _main;
        public static GameObject[] objConnectors;

        void Start()
        {
            var posShift = _main.Radius;
            for (int i = 0; i < Main.objectConnectors.Length; i++)
            {
                var posCurrent = new Vector3(Random.Range(-posShift, posShift),
                    Random.Range(-posShift, posShift),
                    Random.Range(-posShift, posShift));
                var connector = Instantiate(_prefab, posCurrent, Quaternion.identity);
                connector.transform.parent = transform;
                Main.objectConnectors[i] = connector.transform.Find("Connector").gameObject;
            }
        }
    }
}