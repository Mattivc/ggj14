using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathEx { // TODO Test and fix class
	
	public const float e = 2.71828182845905f;
	
	public static bool LineIntersection ( Vector2 line1Start, Vector2 line1End, Vector2 line2Start, Vector2 line2End )
	{
		Vector2 dummy;
		return LineIntersection( line1Start, line1End, line2Start, line2End, out dummy );
	}
	
	public static bool LineIntersection ( Vector2 line1Start, Vector2 line1End, Vector2 line2Start, Vector2 line2End, out Vector2 point )
	{
		Vector2 d1 = line1End - line1Start;
		Vector2 d2 = line2End - line2Start;
		
		Vector2 p1 = line1Start;
		Vector2 p2 = line2Start;
		
		if ( d1.sqrMagnitude < Mathf.Epsilon || d2.sqrMagnitude < Mathf.Epsilon ) {
			point = Vector2.zero;
			return false;
		}
		
		float t = -1.0f;
		float s = -1.0f;
		
		point = Vector2.zero;
		
		if ( Mathf.Abs( d1.x ) > Mathf.Epsilon ) {
			t = ( ( p2.y - p1.y )*d1.x - ( p2.x*d1.y - p1.x*d1.y ) )/( d2.x*d1.y - d2.y*d1.x );
			s = ( p2.x + t*d2.x - p1.x )/d1.x;
			
			point = p1 + d1*s;
		} else if ( Mathf.Abs( d1.y ) > Mathf.Epsilon) {
			t = ( ( p2.x - p1.x )*d1.y - ( p2.y*d1.x - p1.y*d1.x ) )/( d2.y*d1.x - d2.x*d1.y );
			s = ( p2.y + t*d2.y - p1.y )/d1.y;
			
			point = p1 + d1 * s;
		}
		
		if ( s < 0.0f || s > 1.0f ) {
			return false;
		}
		if (t < 0.0f || t > 1.0f) {
			return false;
		}
		
		return true;
	}
	
	private static Vector2 ClosestPointOnLine ( Vector2 lineStart, Vector2 lineEnd, Vector2 point, bool segmentClamp )
    {
        Vector2 AP = point - lineStart;
        Vector2 AB = lineEnd - lineStart;
       
        float ab2 = AB.x*AB.x + AB.y*AB.y;
        float ap_ab = AP.x*AB.x + AP.y*AB.y;
        float t = ap_ab/ab2;
       
        if( segmentClamp ) {
            if( t < 0.0f ) {
				t = 0.0f;	
			} else if ( t > 1.0f ) {
				t = 1.0f;
			}
        }
       
        Vector2 closest = lineStart + AB*t;
        return closest;
    }
	
	/// <summary>
	/// Find the line of intersection between two planes.
	/// </summary>
	public static bool PlaneIntersection( out Vector3 linePoint, out Vector3 lineVec, Vector3 plane1Normal, Vector3 plane1Position, Vector3 plane2Normal, Vector3 plane2Position ) {
	    linePoint = Vector3.zero;
	    lineVec = Vector3.zero;
	   
	    lineVec = Vector3.Cross( plane1Normal, plane2Normal );
	     
	    Vector3 ldir = Vector3.Cross( plane2Normal, lineVec );
	    float denominator = Vector3.Dot( plane1Normal, ldir );
	   
	    //Prevent divide by zero and rounding errors by requiring about 5 degrees angle between the planes.
	    if( Mathf.Abs( denominator ) > 0.006f ) {
	        Vector3 plane1ToPlane2 = plane1Position - plane2Position;
	        float t = Vector3.Dot( plane1Normal, plane1ToPlane2 )/denominator;
	        linePoint = plane2Position + t*ldir;
	        return true;
	    } else {
			return false;
		}
	}
	
	/// <summary>
	/// Get the intersection between a line and a plane.
	/// </summary>
	public static bool LinePlaneIntersection( out Vector3 intersection, Vector3 linePoint, Vector3 lineVec, Vector3 planeNormal, Vector3 planePoint ) {
	    float length;
	    float dotNumerator;
	    float dotDenominator;
	    Vector3 vector;
	    intersection = Vector3.zero;
	   
	    //calculate the distance between the linePoint and the line-plane intersection point
	    dotNumerator = Vector3.Dot( ( planePoint - linePoint ), planeNormal );
	    dotDenominator = Vector3.Dot( lineVec, planeNormal );
	   
	    //line and plane are not parallel
	    if ( dotDenominator != 0.0f ) {
	        length =  dotNumerator/dotDenominator;
	       
	        //create a vector from the linePoint to the intersection point
//	        vector = SetVectorLength(lineVec, length);
			vector = lineVec.normalized * length;
	       
	        //get the coordinates of the line-plane intersection point
	        intersection = linePoint + vector; 
	       
	        return true;
	    } else { //output not valid
			return false;
		}
	}
	
	public static Vector3[] BuildBezier(Vector3[] controlPoints)
    {
        if (controlPoints.Length < 4)
            return new Vector3[0];

        List<Vector3> bezierPoints = new List<Vector3>();


        for (int i = 0; i < controlPoints.Length - 4; i+=3)
        {
            Vector3 p0 = controlPoints[i];
            Vector3 p1 = controlPoints[i+1];
            Vector3 p2 = controlPoints[i+2];
            Vector3 p3 = controlPoints[i+3];

   
            for (float t = 0; t <= 1.01f; t += 0.02f)
            {
                Vector3 sample = (1 - t) * (1 - t) * (1 - t) * p0;

                sample += 3 * (1 - t) * (1 - t) * t * p1;
                sample += 3 * (1 - t) * t * t * p2;
                sample += t * t * t * p3;

                bezierPoints.Add(sample);
            }
        }

        return bezierPoints.ToArray();
    }

    public static Vector3[] BuildBezierFromPath(Vector3[] pathPoints, Vector3 startTangent, Vector3 endTangent, float smoothness)
    {
        if (pathPoints.Length < 2)
            return new Vector3[0];

        List<Vector3> bezierPoints = new List<Vector3>();

        Vector3 enterTangent = startTangent;

        for (int i = 0; i < pathPoints.Length - 1; i++)
        {
            //enterTangent = enterTangent.normalized;

            Vector3 p0 = pathPoints[i];
			Vector3 p3 = pathPoints[i+1];
			
			
			Vector3 p0_p3 = p3 - p0;
			
			float tangentScale = p0_p3.magnitude * smoothness;
			
			if (enterTangent.sqrMagnitude > 0.0001f)
			{
				enterTangent = enterTangent.normalized * tangentScale;
			}
			
            Vector3 p1 = p0 + enterTangent;
			Vector3 leaveTangent = endTangent;

            if (i+2 < pathPoints.Length)
            {
                leaveTangent = pathPoints[i+2] - p3;
				
				leaveTangent = ((leaveTangent.normalized + p0_p3.normalized)/2.0f).normalized * tangentScale;
            }
			
            Vector3 p2 = p3 - leaveTangent;
			
			//Whatever tangent goes out should start the next section for continuity.
            enterTangent = leaveTangent;
   
            for (float t = 0; t <= 1.01f; t += 0.02f)
            {
                Vector3 sample = (1 - t) * (1 - t) * (1 - t) * p0;

                sample += 3 * (1 - t) * (1 - t) * t * p1;
                sample += 3 * (1 - t) * t * t * p2;
                sample += t * t * t * p3;

                bezierPoints.Add(sample);
            }
        }

        return bezierPoints.ToArray();
    }
	
	// Trigonometric functions
	
	public static float Csc ( float f ) {
		return 1f / Mathf.Sin( f );
	}
	
	public static float Sec ( float f ) {
		return 1f / Mathf.Cos( f );
	}
	
	public static float Cot ( float f ) {
		return 1f / Mathf.Tan( f );
	}
	
	// Hyperbolic functions
	
	public static float Sinh ( float f ) {
		return ( Mathf.Pow( e, f ) - Mathf.Pow( e, -f ) ) / 2f;
	}
	
	public static float Cosh ( float f ) {
		return ( Mathf.Pow( e, f ) + Mathf.Pow( e, -f ) ) / 2f;
	}
	
	public static float Tanh ( float f ) {
		return Sinh( f ) / Cosh ( f );
	}
	
	public static float Coth ( float f ) {
		return Cosh( f ) / Sinh ( f );
	}
	
	public static float Sech ( float f ) {
		return 2f / ( Mathf.Pow( e, f ) + Mathf.Pow( e, -f ) );
	}
	
	public static float Csch ( float f ) {
		return 2f / ( Mathf.Pow( e, f ) - Mathf.Pow( e, -f ) );
	}
	
}