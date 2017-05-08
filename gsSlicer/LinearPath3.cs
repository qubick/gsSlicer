﻿using System;
using System.Collections;
using System.Collections.Generic;
using g3;

namespace gs 
{
	public class LinearPath3<T>  : IBuildLinearPath<T> where T : IPathVertex
	{
		List<T> Path;
		PathTypes _pathtype;	// access via Type property

		// todo: add speed
		//  ?? extend PolyLine3d ??

		public LinearPath3(PathTypes type = PathTypes.Travel)
		{
			Path = new List<T>();
			_pathtype = type;
		}
		public LinearPath3(ILinearPath<T> copy) {
			Path = new List<T>();
			_pathtype = copy.Type;		
			foreach ( T v in copy )
				Path.Add(v);
		}

		public T this[int key] { 
			get {
				return Path[key];
			}
		}

		public bool IsLinear {
			get { return true; }
		}

		public bool IsPlanar {
			get { 
				double z = Path[0].Position.z;
                  for ( int i = 1; i < Path.Count; ++i ) {
					if ( Path[i].Position.z != z )
						return false;
				}
				return true;
			}
		}

		public PathTypes Type {
			get { return _pathtype; }
			set { _pathtype = value; }
		}

		public AxisAlignedBox3d Bounds { 
			get {
				return BoundsUtil.Bounds(this, (vtx) => { return vtx.Position; });
			}
		}


		public IEnumerator<T> GetEnumerator() {
			return Path.GetEnumerator();
		}
		IEnumerator IEnumerable.GetEnumerator() {
			return Path.GetEnumerator();
		}


		public int VertexCount {
			get { return Path.Count; }
		}
		public void AppendVertex(T v) {
			if ( Path.Count == 0 || End.Position.DistanceSquared(v.Position) > MathUtil.Epsilon )	
				Path.Add(v);
		}
		public void UpdateVertex(int i, T v)
		{
			Path[i] = v;
		}
		public T Start { 
			get { return Path[0]; }
		}
		public T End { 
			get { return Path[Path.Count-1]; }
		}
		public void ChangeType(PathTypes type) {
			Type = type;
		}
	}
}