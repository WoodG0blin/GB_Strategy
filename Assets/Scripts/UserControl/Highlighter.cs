using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Strategy
{
    internal class Highlighter
    {
        private ISelectable _currentSelected;
        public void HighLight(ISelectable selected)
        {
            HighLight(_currentSelected, false);
            _currentSelected = selected;
            HighLight(_currentSelected, true);
        }
        private void HighLight(ISelectable selected, bool on)
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

                if (GetComponentInBaseOrChildren<MeshFilter>(element, out var originalMesh))
                    mesh.mesh = originalMesh.mesh;

                _outline.localScale = new Vector3(1.5f, 0.01f, 1.5f);

                if (GetComponentInBaseOrChildren<Renderer>(element, out var render))
                {
                    rend.material = render.sharedMaterial;
                    Color color = render.material.color;
                    rend.material.color = new Color(color.r, color.g, color.b, 0.3f);
                }
            }

            _outline.gameObject.SetActive(on);
        }

        private static bool GetComponentInBaseOrChildren<T>(Transform go, out T result)
        {
            T comp;
            if (!go.TryGetComponent<T>(out comp))
                comp = go.GetComponentInChildren<T>();
            result = comp;
            return result != null;
        }
    }
}
