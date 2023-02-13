namespace LoopDataAccessLayer
{
    public class AcadBlockFactory : IAcadBlockFactory
    {
        private readonly IDataLoader dataLoader;

        public AcadBlockFactory(IDataLoader dataLoader)
        {
            this.dataLoader = dataLoader;
        }

        public BlockDataMappable GetBlock(
            BlockMapData blockMap, Dictionary<string, string> tagMap)
        {
            switch (blockMap.Name)
            {
                case "JB_3-TERM_SINGLE":
                    return new JB_3_TERM_SINGLE(dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] };
                
                case "PNL_3-TERM_24VDC":
                    return new PNL_3_TERM_24VDC(dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] };
                
                case "PNL_3-TERM":
                    return new PNL_3_TERM(dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] };
                
                case "MOD_1-TERM_1-BPCS":
                    return new MOD_1_TERM_1_BPCS(dataLoader)
                    {
                        Name = blockMap.Name,
                        Tag = tagMap[blockMap.Tags[0]],
                        ControllerTag = tagMap[blockMap.Tags[1]],
                    };
                
                case "MOD_2-TERM_1-BPCS":
                    return new MOD_2_TERM_1_BPCS(dataLoader)
                    {
                        Name = blockMap.Name,
                        Tag = tagMap[blockMap.Tags[0]],
                        ControllerTag = tagMap[blockMap.Tags[1]],
                        AITag = tagMap[blockMap.Tags[2]],
                    };
                
                case "INST_AI_2W":
                    return new INST_AI_2W(dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] };
                
                case "INST_AO_2W":
                    return new INST_AO_2W(dataLoader)
                    {
                        Name = blockMap.Name,
                        Tag = tagMap[blockMap.Tags[0]],
                        ValveTag = tagMap[blockMap.Tags[1]]
                    };

                case "BUTTERFLY_DIAPHRAGM":
                    return new BUTTERFLY_DIAPHRAGM(dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] };

                case "STD B SIZE SHEET":
                    //return new TITLE_BLOCK(dataLoader)
                    //{
                    //    Name = blockMap.Name,
                    //    Tag = "TitleBlock",
                    //    TitleBlockData = titleBlock,
                    //};
                    return new EMPTY_BLOCK(dataLoader);

                default:
                    return new EMPTY_BLOCK(dataLoader);
            }
        }
    }
}
