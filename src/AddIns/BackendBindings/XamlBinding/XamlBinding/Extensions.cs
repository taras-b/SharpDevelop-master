// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Siegfried Pammer" email="sie_pam@gmx.at"/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Dom;
using ICSharpCode.SharpDevelop.Editor.CodeCompletion;
using ICSharpCode.XmlEditor;
using System.Xml.Linq;

namespace ICSharpCode.XamlBinding
{
	public static class Extensions
	{
		public static string[] Split(this string thisValue, StringSplitOptions options, params char[] delimiters)
		{
			if (thisValue == null)
				throw new ArgumentNullException("thisValue");
			
			return thisValue.Split(delimiters, options);
		}
		
		public static bool EndsWith(this string thisValue, StringComparison comparison, params char[] characters)
		{
			if (thisValue == null)
				throw new ArgumentNullException("thisValue");
			
			foreach (var c in characters) {
				if (thisValue.EndsWith(c.ToString(), comparison))
					return true;
			}
			
			return false;
		}
		
		public static string Replace(this string str, int index, int length, string text)
		{
			if (str == null)
				throw new ArgumentNullException("str");
			if (index < 0 || index > str.Length)
				throw new ArgumentOutOfRangeException("index", index, "Value must be between 0 and " + str.Length);
			if (length < 0 || length > str.Length)
				throw new ArgumentOutOfRangeException("length", length, "Value must be between 0 and " + str.Length);			
			if ((index + length) > str.Length)
				throw new ArgumentOutOfRangeException("index + length", index + length, "Value must be between 0 and " + str.Length);
			
			return str.Substring(0, index) + text + str.Substring(index + length);
		}
		
		public static bool Is(char value, params char[] chars)
		{
			foreach (var c in chars) {
				if (c == value)
					return true;
			}
			
			return false;
		}
		
		public static string GetWordBeforeOffset(this string text, int startIndex)
		{
			if (string.IsNullOrEmpty(text))
				return string.Empty;
			
			int offset = startIndex = Math.Min(startIndex, text.Length - 1);
			
			while (offset > -1 && char.IsWhiteSpace(text[offset]))
				offset--;
			
			while (offset > -1 && !char.IsWhiteSpace(text[offset]))
				offset--;
			
			offset = Math.Max(0, offset);
			
			return text.Substring(offset, startIndex - offset + 1).Trim();
		}
		
		public static int GetLineNumber(this XObject item)
		{
			return (item as IXmlLineInfo).LineNumber;
		}
		
		public static int GetLinePosition(this XObject item)
		{
			return (item as IXmlLineInfo).LinePosition;
		}
		
		public static void Remove(this XmlAttributeCollection coll, string name)
		{
			for (int i = 0; i < coll.Count; i++) {
				if (coll[i].Name.Equals(name, StringComparison.Ordinal)) {
					coll.RemoveAt(i);
					i--;
				}
			}
		}
		
		public static void Remove(this XmlAttributeCollection coll, string name, string namespaceURI)
		{
			for (int i = 0; i < coll.Count; i++) {
				if (coll[i].LocalName.Equals(name, StringComparison.Ordinal) && coll[i].NamespaceURI.Equals(namespaceURI, StringComparison.Ordinal)) {
					coll.RemoveAt(i);
					i--;
				}
			}
		}
		
		public static IEnumerable<ICompletionItem> RemoveEvents(this IEnumerable<ICompletionItem> list)
		{
			foreach (var item in list) {
				if (item is XamlCodeCompletionItem) {
					var comItem = item as XamlCodeCompletionItem;
					if (!(comItem.Entity is IEvent))
						yield return item;
				} else yield return item;
			}
		}
		
		public static IEnumerable<ICompletionItem> RemoveProperties(this IEnumerable<ICompletionItem> list)
		{
			foreach (var item in list) {
				if (item is XamlCodeCompletionItem) {
					var comItem = item as XamlCodeCompletionItem;
					if (!(comItem.Entity is IProperty))
						yield return item;
				} else yield return item;
			}
		}
	}
}