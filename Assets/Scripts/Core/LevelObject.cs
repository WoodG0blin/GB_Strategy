using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal class LevelObject : MonoBehaviour, ISelectable
    {
        [SerializeField] Transform _outline;
        [field: SerializeField] public Sprite Icon { get; private set; }

        [field: SerializeField] public int Health { get; set; }

        [field: SerializeField] public int MaxHealth { get; private set; }

        public void Highlight(bool on)
        {
            //Debug.Log(this + " highlighted");
            if (_outline == null)
            {
                _outline = transform.Find("Outline");
                if (_outline == null)
                {
                    _outline = Instantiate(new GameObject("Outline").transform, transform);
                    _outline.SetParent(transform);
                    MeshRenderer rend = _outline.gameObject.AddComponent<MeshRenderer>();
                    MeshFilter mesh = _outline.gameObject.AddComponent<MeshFilter>();
                    mesh.mesh = transform.GetComponent<MeshFilter>().mesh;
                    _outline.localScale = new Vector3(1.5f, 0.01f, 1.5f);
                    rend.material = transform.GetComponent<Renderer>().sharedMaterial;
                    Color color = transform.GetComponent<Renderer>().material.color;
                    rend.material.color = new Color(color.r, color.g, color.b, 0.3f);
                }
            }

            _outline.gameObject.SetActive(on);
        }
    }
}
