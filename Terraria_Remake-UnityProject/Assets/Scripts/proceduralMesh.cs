using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class proceduralMesh : MonoBehaviour{

	public Vector3[] vertices;
	public Vector2[] uvs;
	public int[] triangulos;
	public void makeMeshData (Vector3 meshPos,int posTriangles) {
		if(vertices.Length <= 0){
			//Definir posiçao dos vertices
			vertices = new Vector3[]{
				new Vector3(0,0,meshPos.z),
				new Vector3(0,1,meshPos.z),
				new Vector3(1,0,meshPos.z),
				new Vector3(1,1,meshPos.z)
			};

			uvs = new Vector2[]{
				new Vector2(0,0),
				new Vector2(0,1),
				new Vector2(1,0),
				new Vector2(1,1)
			};
			//Definir orden de construcao dos triangulos
			triangulos = new int[]{0,1,2,2,1,3};

			print("fez a mesh");
		}
	}

	//Atualizar a Malha/Mesh;
	public void createMesh(Mesh _mesh){
		if(vertices.Length >= 0){
			_mesh.Clear();
			_mesh.vertices = vertices;
			_mesh.triangles = triangulos;
			_mesh.uv = uvs;
			print("Atualizou a Mesh");
		}
		
	}
	public void clearMesh(Mesh _mesh){
		if(vertices.Length > 0){
			_mesh.Clear();
			print("Limpou a mesh");
		}
		
	}
}
