using System;
using Serilog;

namespace LoopDataAccessLayer
{
    public class AcadBlockFactory : IAcadBlockFactory
    {
        private readonly IDataLoader dataLoader;
        private readonly ILogger logger;

        public AcadBlockFactory(IDataLoader dataLoader, ILogger logger)
        {
            this.dataLoader = dataLoader;
            this.logger = logger;
        }

        public IMappableBlock GetBlock(BlockMapData blockMap, Dictionary<string, string> tagMap)
        {
            return blockMap.Name switch
            {
                "JB_11-TERM_I.S." => new JB_TERM_ISOLATOR(logger, dataLoader, blockMap, tagMap),

                "JB_3-TERM_SINGLE" or
                "JB_4-TERM_SINGLE_SHLD" => new JB_ANALOG_TERM_SINGLE(logger, dataLoader, blockMap, tagMap),
                "JB_3-TERM_DUAL" or
                "JB_4-TERM_DUAL_SHLD" => new JB_ANALOG_TERM_DUAL(logger, dataLoader, blockMap, tagMap),

                "JB_2-TERM_SINGLE" or
                "JB_2-TERM_SINGLE_JOG" or
                "JB_4-TERM_SINGLE" => new JB_TERM(logger, dataLoader, blockMap, tagMap),
                "JB_2-TERM_DUAL" or
                "JB_4-TERM_DUAL" => new JB_TERM_DUAL(logger, dataLoader, blockMap, tagMap),

                "PNL_2-TERM" => new PNL_2_TERM(logger, dataLoader, blockMap, tagMap),
                "PNL_2-TERM_24VDC" => new PNL_2_TERM_24VDC(logger, dataLoader, blockMap, tagMap),
                "PNL_2-TERM_EXT_PWR" or
                "PNL_2-TERM_EXT_PWR-2" => new PNL_2_TERM_EXT_PWR(logger, dataLoader, blockMap, tagMap),
                "PNL_3-TERM_24VDC-1" => new PNL_2_TERM_24VDC(logger, dataLoader, blockMap, tagMap),
                "PNL_3-TERM_24VDC-2" or
                "PNL_3-TERM_DYN" or
                "PNL_4-TERM_24VDC_SHLD" => new PNL_3_or_4_TERM_24VDC(logger, dataLoader, blockMap, tagMap),
                "PNL_3-TERM" => new PNL_3_TERM(logger, dataLoader, blockMap, tagMap),
                "PNL_4-TERM_24VDC" => new PNL_4_TERM_24VDC(logger, dataLoader, blockMap, tagMap),

                "MOD_1-TERM_1-BPCS" or
                "MOD_1-TERM_1-SIS" or
                "MOD_1-TERM_1-PT_DYN" => new MOD_1_TERM_1(logger, dataLoader, blockMap, tagMap),
                "MOD_2-TERM_1-PT_DYN" or
                "MOD_2-TERM_1-PT_DYN_MIR" => new MOD_2_TERM_1(logger, dataLoader, blockMap, tagMap),
                
                "MOD_2-TERM_1-SIS" or
                "MOD_2-TERM_1-BPCS_MIR" => new MOD_2_TERM_1_DISCRETE(logger, dataLoader, blockMap, tagMap),
                
                "MOD_2-TERM_2-BPCS" => new MOD_2_TERM_2_BPCS(logger, dataLoader, blockMap, tagMap),
                
                "RELAY_24VDC-2" or
                "RELAY_120VAC" => new RELAY(logger, dataLoader, blockMap, tagMap),
                "RELAY_120VAC_SERIES" => new RELAY_SERIES(logger, dataLoader, blockMap, tagMap),

                
                "INST_AI_2W" => new INST_AI_2W(logger, dataLoader, blockMap, tagMap),
                "INST_AI_3W" => new INST_AI_3W(logger, dataLoader, blockMap, tagMap),
                "INST_AO_2W" => new INST_AO_2W(logger, dataLoader, blockMap, tagMap),
                "INST_DI_4W" => new INST_DI_4W(logger, dataLoader, blockMap, tagMap),
                "INST_DI_4W_ZS" => new INST_DI_4W_ZS(logger, dataLoader, blockMap, tagMap),
                "INST_DO_2W" => new INST_DO_2W(logger, dataLoader, blockMap, tagMap),

                "TERM_DO_1W" or
                "TERM_DO_2W" => new TERM_DO_2W(logger, dataLoader, blockMap, tagMap),
                "TERM_DI_2W" => new TERM_DI_2W(logger, dataLoader, blockMap, tagMap),

                "VALVE_BODY" => new VALVE_BODY(logger, dataLoader, blockMap, tagMap),
                "VALVE_1-SOL" => new VALVE_ONE_SOL(logger, dataLoader, blockMap, tagMap),
                "VALVE_2-SOL" => new VALVE_TWO_SOL(logger, dataLoader, blockMap, tagMap),

                "SD_TABLE_0-ROW" or
                "SD_TABLE_10-ROW" or
                "SD_TABLE_20-ROW" => new SD_TABLE(logger, dataLoader, blockMap, tagMap),
                "SD_TABLE_30-ROW" => new SD_TABLE_30(logger, dataLoader, blockMap, tagMap),

                "ETHERNET_PORT" => new ETHERNET_OVERLOAD(logger, dataLoader, blockMap, tagMap),
                "MOD_2-BPCS_ETH" => new MOD_2_BPCS_ETH(logger, dataLoader, blockMap, tagMap),


                "STD B SIZE SHEET" => new TITLE_BLOCK(logger, dataLoader, blockMap, tagMap),
                "MATERIAL_LIST_5-ROW" => new MATERIAL_LIST_5_ROW(logger, dataLoader, blockMap, tagMap),
                
                _ => new EMPTY_BLOCK(logger, dataLoader)
            };
        }
    }
}
