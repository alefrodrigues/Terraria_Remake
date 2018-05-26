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
	public void updateUv(Mesh _mesh,float _uvX,float _uvY,float divUv){
		Vector2[] _uvs = _mesh.uv;
		_uvs[0] = new Vector2(_uvs[0].x/_uvX,_uvs[0].y/_uvY);
		_uvs[1] = new Vector2(_uvs[1].x/_uvX,_uvs[1].y/_uvY);
		_uvs[2] = new Vector2(_uvs[2].x/_uvX,_uvs[2].y/_uvY);
		_uvs[3] = new Vector2(_uvs[3].x/_uvX,_uvs[3].y/_uvY);

		/*_uvs = new Vector2[]{
				new Vector2(_uvs[0].x/divUv,_uvs[0].y/divUv),
				new Vector2(_uvs[0].x/divUv,_uvs[0].y/divUv),
				new Vector2(_uvs[0].x/divUv,_uvs[0].y/divUv),
				new Vector2(_uvs[0].x/divUv,_uvs[0].y/divUv)
		};*/
		uvs = _uvs;
		createMesh(_mesh);
	}
	public void clearMesh(Mesh _mesh){
		if(vertices.Length > 0){
			_mesh.Clear();
			print("Limpou a mesh");
		}
		
	}
}
