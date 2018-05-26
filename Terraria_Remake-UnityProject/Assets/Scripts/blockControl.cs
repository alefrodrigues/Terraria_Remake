using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter))]
public class blockControl : MonoBehaviour {
	proceduralMesh pMesh;
	Mesh mesh;
	public Transform player;
	public Vector3 positionMeshData;
	public int posTriangles;
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
		
	}
}
