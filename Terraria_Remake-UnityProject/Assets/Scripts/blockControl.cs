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

	public bool updateMesh;
	public float uvX;
	public float uvY;
	public float divUv;

	public Vector2 v1;
	public Vector2 v2;
	public Vector2 v3;
	public Vector2 v4;
	void Awake(){
		mesh = GetComponent<MeshFilter>().mesh;
		pMesh = GetComponent<proceduralMesh>();
		player = GameObject.FindWithTag("Player").transform;
	}
	void Start () {
		/*pMesh.makeMeshData(positionMeshData,posTriangles);
		pMesh.createMesh(mesh);
		pMesh.updateUv(mesh,v1,v2,v3,v4);*/
	}
	void Update(){
		if(updateMesh){
			//pMesh.updateUv(mesh,v1,v2,v3,v4);
			updateMesh = false;
		}
	}
}
