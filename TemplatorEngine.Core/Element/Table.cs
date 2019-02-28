﻿

using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;

namespace TemplatorEngine.Core.Model.Element
{
    public class Table : TemplateElementBase
    {
        [XmlAttribute]
        public string DataField { get; set; }
    }
}