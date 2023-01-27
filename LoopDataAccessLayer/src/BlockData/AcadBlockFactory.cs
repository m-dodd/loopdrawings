namespace LoopDataAccessLayer
{
    public class AcadBlockFactory
    {
        private readonly DataLoader dataLoader;

        public AcadBlockFactory(DataLoader dataLoader)
        {
            this.dataLoader = dataLoader;
        }

        public BlockDataMappable GetBlock(
            BlockMapData blockMap, Dictionary<string, string> tagMap, TitleBlockData titleBlock )
        {
            switch (blockMap.Name)
            {
                case "JB_3-TERM_SINGLE":
                    return new JB_3_TERM_SINGLE(this.dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] };
                
                case "PNL_3-TERM_24VDC":
                    return new PNL_3_TERM_24VDC(this.dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] };
                
                case "PNL_3-TERM":
                    return new PNL_3_TERM(this.dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] };
                
                case "MOD_1-TERM":
                    return new MOD_1_TERM(this.dataLoader)
                    {
                        Name = blockMap.Name,
                        Tag = tagMap[blockMap.Tags[0]],
                        ControllerTag = tagMap[blockMap.Tags[1]],
                    };
                
                case "MOD_2-TERM":
                    return new MOD_2_TERM(this.dataLoader)
                    {
                        Name = blockMap.Name,
                        Tag = tagMap[blockMap.Tags[0]],
                        ControllerTag = tagMap[blockMap.Tags[1]],
                    };
                
                case "INST_AI_2W":
                    return new INST_AI_2W(this.dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] };
                
                case "INST_AO_2W":
                    return new INST_AO_2W(this.dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] };

                case "BUTTERFLY_DIAPHRAGM":
                    return new BUTTERFLY_DIAPHRAGM(this.dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] };

                case "STD B SIZE SHEET":
                    //return new TITLE_BLOCK(this.dataLoader)
                    //{
                    //    Name = blockMap.Name,
                    //    Tag = "TitleBlock",
                    //    TitleBlockData = titleBlock,
                    //};
                    return new EMPTY_BLOCK(this.dataLoader);

                default:
                    return new EMPTY_BLOCK(this.dataLoader);
            }
        }
    }
}
