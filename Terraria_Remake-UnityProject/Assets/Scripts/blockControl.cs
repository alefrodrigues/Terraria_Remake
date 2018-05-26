﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class blockControl : MonoBehaviour {
	proceduralMesh pMesh;
	Mesh mesh;
	public Transform player;
	public Vector3 positionMeshData;
	public int posTriangles;

	public bool updateMesh;
	public float uvX;
	public float uvY;
	public float divUv;

	public float a;
	public float b;
	public float c;
	public float d;
	public float e;
	public float f;
	public float g;
	public float h;
	void Awake(){
		mesh = GetComponent<MeshFilter>().mesh;
		pMesh = GetComponent<proceduralMesh>();
		player = GameObject.FindWithTag("Player").transform;
	}
	void Start () {
		pMesh.makeMeshData(positionMeshData,posTriangles);
		pMesh.createMesh(mesh);
	}
	void Update(){
		if(updateMesh){
			pMesh.updateUv(mesh,a,b,c,d,e,f,g,h);
			updateMesh = false;
		}
	}
}
