using UnityEngine;
using System.Collections;

public static class RectEx {
	
	public static Rect Multiply (this Rect source, float multiplier) {
		return new Rect( source.x * multiplier,
		                 source.y * multiplier,
		                 source.width * multiplier,
		                 source.height * multiplier );
	}
	
	public static Rect[] RectGrid(Rect gridOutline, int colums, int rows) {
		
		if ( colums < 1 || rows < 1 ) {
			Debug.LogError("RectGrid colums and rows must be 1 or greater.");
			return null;
		}
		
		Rect[] recArray = new Rect[colums * rows];
		float columStep = gridOutline.width/colums;
		float rowStep = gridOutline.height/rows;
		
		for (int iColum = 0; iColum < colums; iColum++) {
			for (int iRow = 0; iRow < rows; iRow++) {
				recArray[colums * iRow + iColum] = new Rect( gridOutline.x + columStep * iColum,
				                                             gridOutline.y + rowStep * iRow,
				                                             columStep,
				                                             rowStep );
			}
		}
		
		return recArray;
	}
	
}
