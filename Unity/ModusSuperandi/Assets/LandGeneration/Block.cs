using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	private Chunk chunk;
	private ChunkLoader chunkLoader;
	void Start () {
		chunkLoader=GameObject.Find("ChunkLoader").GetComponent<ChunkLoader>();
	}
}
