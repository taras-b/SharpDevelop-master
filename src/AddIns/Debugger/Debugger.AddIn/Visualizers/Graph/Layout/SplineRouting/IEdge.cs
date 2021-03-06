﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the BSD license (for details please see \src\AddIns\Debugger\Debugger.AddIn\license.txt)

using System.Collections.Generic;
using System.Linq;
using System;

namespace Debugger.AddIn.Visualizers.Graph.SplineRouting
{
	/// <summary>
	/// Enables passing any type of graph (implementing IRect, IEdge) into EdgeRouter.
	/// </summary>
	public interface IEdge
	{
		IRect From { get; }
		IRect To { get; }
		//IPoint StartPoint { get; set; }
		//IPoint EndPoint { get; set; }
	}
}
