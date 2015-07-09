using UnityEngine;
using System.Collections;

public class Chunk : MonoBehaviour {
	public const int  AIR_CODE=0, GRASS_CODE=1, DIRT_CODE=2, STONE_CODE=3;
	private static ChunkLoader chunkLoader;
	private Block[,] blocks;
	private int chunkOffset;
	
	private void setBlocks(Block[,] blocks, int chunkOffset) {
		this.blocks=blocks;
		this.chunkOffset=chunkOffset;
	}
	
	public int getChunkOffset() {
		return chunkOffset;
	}
	
	public static Chunk ToRealChunk(int[,] chunkAsNumbers, int chunkOffset) {
		if (chunkLoader==null) chunkLoader=GameObject.Find("ChunkLoader").GetComponent<ChunkLoader>();
		Block[,] blocks=new Block[chunkLoader.CHUNK_WIDTH, chunkLoader.MAX_SKY_HEIGHT-chunkLoader.BEDROCK_LEVEL];
		for (int x=0; x<blocks.GetLength(0); x++) {
			for (int y=0; y<blocks.GetLength(1); y++) {
				Vector3 blockPosition=new Vector3(x+chunkLoader.CHUNK_WIDTH*chunkOffset, y+chunkLoader.BEDROCK_LEVEL, 0);
				blocks[x, y]=(Block)Instantiate(chunkLoader.blockPrefabs[chunkAsNumbers[x, y]],  blockPosition, Quaternion.identity);
			}
		}
		Chunk toReturn=(Chunk)Instantiate(chunkLoader.chunkPrefab, new Vector3(0, 0, 0), Quaternion.identity);
		toReturn.setBlocks(blocks, chunkOffset);
		return toReturn;
	}
}
