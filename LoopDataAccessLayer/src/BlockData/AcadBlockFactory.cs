namespace LoopDataAccessLayer
{
    public class AcadBlockFactory : IAcadBlockFactory
    {
        private readonly IDataLoader dataLoader;

        public AcadBlockFactory(IDataLoader dataLoader)
        {
            this.dataLoader = dataLoader;
        }

        public IMappableBlock GetBlock(BlockMapData blockMap, Dictionary<string, string> tagMap)
        {
            return blockMap.Name switch
            {
                "JB_3-TERM_SINGLE" =>new JB_3_TERM_SINGLE(dataLoader)
                {
                    Name = blockMap.Name,
                    Tag = tagMap[blockMap.Tags[0]]
                },

                "PNL_3-TERM_24VDC-2" => new PNL_3_TERM_24VDC(dataLoader)
                {
                    Name = blockMap.Name,
                    Tag = tagMap[blockMap.Tags[0]]
                },

                "PNL_3-TERM" => new PNL_3_TERM(dataLoader)
                {
                    Name = blockMap.Name,
                    Tag = tagMap[blockMap.Tags[0]]
                },

                "MOD_1-TERM_1-BPCS" => new MOD_1_TERM_1_BPCS(dataLoader)
                {
                    Name = blockMap.Name,
                    Tag = tagMap[blockMap.Tags[0]],
                    ControllerTag = tagMap[blockMap.Tags[1]],
                },

                "MOD_2-TERM_1-BPCS" => new MOD_2_TERM_1_BPCS(dataLoader)
                {
                    Name = blockMap.Name,
                    Tag = tagMap[blockMap.Tags[0]],
                    ControllerTag = tagMap[blockMap.Tags[1]],
                    AITag = tagMap[blockMap.Tags[2]],
                },

                "INST_AI_2W" => new INST_AI_2W(dataLoader)
                {
                    Name = blockMap.Name,
                    Tag = tagMap[blockMap.Tags[0]]
                },

                "INST_AO_2W" => new INST_AO_2W(dataLoader)
                {
                    Name = blockMap.Name,
                    Tag = tagMap[blockMap.Tags[0]],
                    ValveTag = tagMap[blockMap.Tags[1]]
                },

                "VALVE_BODY" =>
                    //new VALVE_BODY(dataLoader) { Name = blockMap.Name, Tag = tagMap[blockMap.Tags[0]] },
                    // until I get the dynamic block working this mapping doesn't work - Autocad finds a block reference
                    // but that is the wrong type and it throws an error when trying to cast to that type
                    // FIX AUTOCAD then re-enable this.
                    new EMPTY_BLOCK(dataLoader),

                "STD B SIZE SHEET" => new TITLE_BLOCK(dataLoader)
                {
                    Name = blockMap.Name,
                    Tag = tagMap[blockMap.Tags[0]],
                    DescriptionTag = tagMap[blockMap.Tags[1]]
                },

                _ => new EMPTY_BLOCK(dataLoader)
            };
        }
    }
}
