﻿// Copyright (c) AlphaSierraPapa for the SharpDevelop Team (for details please see \doc\copyright.txt)
// This code is distributed under the GNU LGPL (for details please see \doc\license.txt)

using ICSharpCode.SharpDevelop.Tests.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using ICSharpCode.CodeCoverage;
using ICSharpCode.Core;
using ICSharpCode.SharpDevelop.Editor;
using NUnit.Framework;

namespace ICSharpCode.CodeCoverage.Tests.Highlighting
{
	[TestFixture]
	public class CodeCoverageMarkersInvalidEndColumnTestFixture
	{
		List<ITextMarker> markers;
		
		[SetUp]
		public void Init()
		{
			try {
				string configFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "NCoverAddIn.Tests");
				PropertyService.InitializeService(configFolder, Path.Combine(configFolder, "data"), "NCoverAddIn.Tests");
			} catch (Exception) {}
			
			IDocument document = MockTextMarkerService.CreateDocumentWithMockService();
			ITextMarkerService markerStrategy = document.GetService(typeof(ITextMarkerService)) as ITextMarkerService;
			document.Text = "abcdefg\r\nabc";
			
			string xml = "<PartCoverReport>\r\n" +
				"\t<File id=\"1\" url=\"c:\\Projects\\XmlEditor\\Test\\Schema\\SingleElementSchemaTestFixture.cs\" />\r\n" +
				"\t<Assembly id=\"1\" name=\"XmlEditor.Tests\" module=\"C:\\Projects\\Test\\XmlEditor.Tests\\bin\\XmlEditor.Tests.DLL\" domain=\"test-domain-XmlEditor.Tests.dll\" domainIdx=\"1\" />\r\n" +
				"\t<Type name=\"XmlEditor.Tests.Schema.SingleElementSchemaTestFixture\" asmref=\"1\">\r\n" +
				"\t\t<Method name=\"GetSchema\">\r\n" +
				"\t\t<pt visit=\"1\" fid=\"1\" sl=\"1\" sc=\"1\" el=\"1\" ec=\"9\"/>\r\n" +
				"\t\t<pt visit=\"1\" fid=\"1\" sl=\"1\" sc=\"1\" el=\"1\" ec=\"0\" />\r\n" +
				"\t\t<pt visit=\"1\" fid=\"1\" sl=\"1\" sc=\"1\" el=\"1\" ec=\"-1\" />\r\n" +
				"\t\t<pt visit=\"1\" fid=\"1\" sl=\"1\" sc=\"3\" el=\"1\" ec=\"2\" />\r\n" +
				"\t\t</Method>\r\n" +
				"\t</Type>\r\n" +
				"</PartCoverReport>";
			
			CodeCoverageResults results = new CodeCoverageResults(new StringReader(xml));
			CodeCoverageMethod method = results.Modules[0].Methods[0];
			CodeCoverageHighlighter highlighter = new CodeCoverageHighlighter();
			highlighter.AddMarkers(document, method.SequencePoints);
			
			markers = new List<ITextMarker>();
			foreach (ITextMarker marker in markerStrategy.TextMarkers) {
				markers.Add(marker);
			}
		}
		
		[Test]
		public void NoMarkersAdded()
		{
			Assert.AreEqual(0, markers.Count, 
				"Should not be any markers added since all sequence point end columns are invalid.");
		}
	}
}
