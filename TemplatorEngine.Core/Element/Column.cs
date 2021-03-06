using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using TemplatorEngine.Core.Abstract;
using TemplatorEngine.Core.Model;

namespace TemplatorEngine.Core.Element
{
    
    public class Column : TemplateElementBase
    {
        
        [XmlElement(Type = typeof(Field)),
        XmlElement(Type = typeof(Line)),
        XmlElement(Type = typeof(Space)),
        XmlElement(Type = typeof(Label)),
        XmlElement(Type = typeof(Value)),
        XmlElement(Type = typeof(Image)),
        XmlElement(Type = typeof(Barcode)),
        XmlElement(Type = typeof(Iterator)),
        XmlElement(Type = typeof(Row)),
        XmlElement(Type = typeof(NewPage)),
        XmlElement(Type = typeof(Column))]
        public List<TemplateElementBase> Items { get; set; }
        
        public override void Initialize(double? maxWidth, double? maxHeight, RenderContext context, IList<PropertyData> data)
        {
            if (this.Width == null)
            {
                this.Width = maxWidth ?? context.MaxPageWidth;
            }

            if (this.Height != null)
            {
                throw new Exception("Height of element Column is calculated automatically - cannot be set in XML");
            }

            this.Height = 0;

            var currentX = context.CurrentX;
            
            foreach (var item in this.Items)
            {
                context.CurrentX = currentX;
                
                item.Initialize(this.Width, item.Height-context.PageSettings.Spacing, context,  data);

                if (context.NewPageCreated)
                {
                    context.NewPageCreated = false;
                    this.Height = -context.PageSettings.Spacing.Value;
                }

                context.CurrentY += item.Height.Value + context.PageSettings.Spacing.Value;
                

                this.Height += item.Height+ context.PageSettings.Spacing;
            }

            this.Height -= context.PageSettings.Spacing;
            context.CurrentX = currentX;
            
        }
    }
}