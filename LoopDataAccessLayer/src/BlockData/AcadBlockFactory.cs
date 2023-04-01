using DocumentFormat.OpenXml.Wordprocessing;

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
                "JB_3-TERM_SINGLE" => new JB_ANALOG_TERM_SINGLE(dataLoader, blockMap, tagMap),
                "JB_2-TERM_SINGLE" 
                or "JB_2-TERM_SINGLE_JOG"
                or "JB_4-TERM_SINGLE" => new JB_TERM_SINGLE(dataLoader, blockMap, tagMap),
                
                "PNL_2-TERM" => new PNL_2_TERM(dataLoader, blockMap, tagMap),
                "PNL_2-TERM_24VDC" => new PNL_2_TERM_24VDC(dataLoader, blockMap, tagMap),

                "PNL_2-TERM_EXT_PWR" => new PNL_2_TERM_EXT_PWR(dataLoader, blockMap, tagMap),
                
                "PNL_3-TERM_24VDC-1" => new PNL_2_TERM_24VDC(dataLoader, blockMap, tagMap),
                "PNL_3-TERM_24VDC-2" => new PNL_3_TERM_24VDC(dataLoader, blockMap, tagMap),
                "PNL_3-TERM" => new PNL_3_TERM(dataLoader, blockMap, tagMap),
                "PNL_4-TERM_24VDC" => new PNL_4_TERM_24VDC(dataLoader, blockMap, tagMap),
                
                "MOD_1-TERM_1-BPCS"
                or "MOD_1-TERM_1-PT_DYN"
                or "MOD_1-TERM_1-SIS" => new MOD_1_TERM_1(dataLoader, blockMap, tagMap),
                "MOD_2-TERM_1-BPCS" => new MOD_2_TERM_1(dataLoader, blockMap, tagMap),
                "MOD_2-TERM_1-SIS"
                or "MOD_2-TERM_1-BPCS_MIR" => new MOD_2_TERM_1_DISCRETE(dataLoader, blockMap, tagMap),
                "MOD_2-TERM_2-BPCS" => new MOD_2_TERM_2_BPCS(dataLoader, blockMap, tagMap),
                
                "RELAY_24VDC-2" => new RELAY_24VDC_2(dataLoader, blockMap, tagMap),
                
                "INST_AI_2W" => new INST_AI_2W(dataLoader, blockMap, tagMap),
                "INST_AO_2W" => new INST_AO_2W(dataLoader, blockMap, tagMap),
                "INST_DI_4W" => new INST_DI_4W(dataLoader, blockMap, tagMap),
                "INST_DI_4W_ZS" => new INST_DI_4W_ZS(dataLoader, blockMap, tagMap),
                "INST_DO_2W" => new INST_DO_2W(dataLoader, blockMap, tagMap),

                "VALVE_BODY" => new VALVE_BODY(dataLoader, blockMap, tagMap),
                "VALVE_2-SOL" => new VALVE_TWO_SOL(dataLoader, blockMap, tagMap),

                "SD_TABLE_10-ROW"
                or "SD_TABLE_20-ROW" => new SD_TABLE(dataLoader, blockMap, tagMap),
                "SD_TABLE_30-ROW" => new SD_TABLE_30(dataLoader, blockMap, tagMap),
                
                "STD B SIZE SHEET" => new TITLE_BLOCK(dataLoader, blockMap, tagMap),
                "MATERIAL_LIST_5-ROW" => new MATERIAL_LIST_5_ROW(dataLoader, blockMap, tagMap),
                
                _ => new EMPTY_BLOCK(dataLoader)
            };
        }
    }
}
