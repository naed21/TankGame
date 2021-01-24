using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace TankGameLibrary
{
	public class TextureAtlas
	{
		[XmlElement("SubTexture")]
		public SubTexture[] SubTextures {get;set;}
	}
	
	public class SubTexture
	{
		[XmlAttribute("name")]
		public string Name { get; set; }
		[XmlAttribute("x")]
		public string Xpos { get; set; }
		[XmlAttribute("y")]
		public string Ypos { get; set; }
		[XmlAttribute("width")]
		public string Width { get; set; }
		[XmlAttribute("height")]
		public string Height { get; set; }
	}
}
