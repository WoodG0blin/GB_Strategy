using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Strategy
{
    internal class Highlighter
    {
        public void HighLight(ISelectable selected, bool on)
        {
            if (selected == null) return;

            Transform element = (selected as MonoBehaviour).transform;

            Transform _outline = element.Find("Outline");
            
            if (_outline == null)
            {
                _outline = GameObject.Instantiate(new GameObject("Outline").transform, element);
                _outline.name = "Outline";
                _outline.SetParent(element);
                MeshRenderer rend = _outline.gameObject.AddComponent<MeshRenderer>();
                MeshFilter mesh = _outline.gameObject.AddComponent<MeshFilter>();
                mesh.mesh = element.GetComponent<MeshFilter>().mesh;
                _outline.localScale = new Vector3(1.5f, 0.01f, 1.5f);
                rend.material = element.GetComponent<Renderer>().sharedMaterial;
                Color color = element.GetComponent<Renderer>().material.color;
                rend.material.color = new Color(color.r, color.g, color.b, 0.3f);
            }

            _outline.gameObject.SetActive(on);
        }
    }
}
