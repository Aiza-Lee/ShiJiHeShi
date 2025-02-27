using System;
using UnityEngine;

public class CameraSlope : MonoBehaviour {

	[Range(-1, 1)] public float HorizObl, VertObl;

    public void SetObliqueness() {
        // Matrix4x4 mat = Camera.main.projectionMatrix;
        Matrix4x4 mat = GetComponent<Camera>().projectionMatrix;
        mat[0, 2] = HorizObl;
        mat[1, 2] = VertObl;
        GetComponent<Camera>().projectionMatrix = mat;
    }
	void Start() {
		SetObliqueness();
	}
} 