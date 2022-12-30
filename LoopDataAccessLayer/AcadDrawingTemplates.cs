using LoopDataAdapterLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class AcadDrawingTemplate
    {
        private TemplateConfig template;
        public AcadDrawingTemplate(TemplateConfig template)
        {
            this.template = template;
        }
    }

    public class AI_SINGLE : AcadDrawingTemplate
    {
        public Dictionary<string, string> BlockMap { get; set; } = new Dictionary<string, string>();
        
        public AI_SINGLE(TemplateConfig template) : base(template) { }

        private void Map()
        {

        }

    }

    public class AI_AO_BUTTERFLY
    {
        public string AI_Tag { get; }
        public string AO_Tag { get; }
        public string Butterfly_Tag { get; }

        public AI_AO_BUTTERFLY(string AI_Tag, string AO_Tag, string Butterfly_Tag)
        {
            this.AI_Tag = AI_Tag;
            this.AO_Tag = AO_Tag;
            this.Butterfly_Tag = Butterfly_Tag;
        }
    }
}
