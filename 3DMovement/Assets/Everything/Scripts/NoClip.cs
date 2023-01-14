using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class NoClip : MonoBehaviour
{
	[SerializeField] private float radius;
	[SerializeField] private float distance;

	[SerializeField] private AnimationCurve offsetCurve;

	[SerializeField] private LayerMask clippingLayerMask;

	private Vector3 _originalLocalPosition;

	private void Start() => _originalLocalPosition = transform.localPosition;

	private void Update()
	{
		if (Physics.SphereCast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0.0f)), radius, out var hit, distance, clippingLayerMask))
		{
			transform.localPosition = _originalLocalPosition + new Vector3(0.0f, 0.0f, offsetCurve.Evaluate(hit.distance / distance));
		}
		else
		{
			transform.localPosition = _originalLocalPosition;
		}
	}
}
