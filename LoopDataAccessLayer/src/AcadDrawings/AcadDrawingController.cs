using LoopDataAdapterLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Serilog;
using System.Runtime.Serialization;

namespace LoopDataAccessLayer
{
    public class AcadDrawingController
    {
        
        private readonly LoopDataConfig loopConfig;
        private readonly IDataLoader dataLoader;
        private readonly ILogger logger;
        public bool ErrorsDetected { get; private set; } = false;


        public List<AcadDrawingDataMappable> Drawings { get; set; }

        public AcadDrawingController(IDataLoader dataLoader, LoopDataConfig loopConfig, ILogger logger)
        {
            this.dataLoader = dataLoader;
            this.loopConfig = loopConfig;
            IExcelTitleBlockData<string> titleBlock = dataLoader.GetTitleBlockData();
            loopConfig.SiteID = titleBlock.SiteNumber;
            Drawings = new List<AcadDrawingDataMappable>();
            this.logger = logger;
        }

        public AcadDrawingController(
            string excelFileName,
            string configDirectoryName,
            string templatePath,
            string outputDrawingPath,
            ILogger logger)
            : this(CreateDataLoader(excelFileName), new LoopDataConfig(logger, configDirectoryName), logger)
        {
            loopConfig.TemplateDrawingPath = templatePath;
            loopConfig.OutputDrawingPath = outputDrawingPath;

            try
            {
                loopConfig.LoadConfig();
            }
            catch (LoopDataException ex)
            {
                //string msg = "Unable to find"
                //    + Environment.NewLine
                //    + configDirectoryName
                //    + Environment.NewLine
                //    + "Please relocate the file and try again.";

                //throw new FileNotFoundException(msg);
                throw;
            }
        }

        private static IDataLoader CreateDataLoader(string excelFileName)
        {
            ExcelDataLoader excelDL;
            try
            {
                excelDL = new ExcelDataLoader(excelFileName);
            }
            catch (FileNotFoundException)
            {
                string msg = "Unable to find"
                    + Environment.NewLine
                    + excelFileName
                    + Environment.NewLine
                    + "Please relocate the file and try again.";
                throw new FileNotFoundException(msg);
            }
            catch (IOException)
            {
                throw new IOException("Excel must be open - please close it.");
            }

            // any error handlign here?
            DBDataLoader dbDL = new();

            return new DataLoader(excelDL, dbDL);
        }

        public void BuildDrawings()
        {
            ErrorsDetected = false;
            var blockFactory = new AcadBlockFactory(dataLoader, logger);
            var templatePicker = new TemplatePicker(dataLoader, loopConfig, logger);
            var drawingBuilder = new AcadDrawingBuilder(dataLoader, loopConfig, templatePicker, blockFactory, new LoopTagMapper(), logger);
            
            IEnumerable<LoopNoTemplatePair> loops = dataLoader.GetLoops();
            logger.Information($"{loops.Count()} loops found");

            List<string> loopsWithProblems = new();
            foreach (LoopNoTemplatePair loop in loops)
            {
                try
                {
                    logger.Information("Creating drawing(s) for " + loop.LoopNo);
                    IEnumerable<AcadDrawingDataMappable> drawings = drawingBuilder.BuildDrawings(loop);
                    foreach (AcadDrawingDataMappable drawing in drawings)
                    {
                        Drawings.Add(drawing);
                    }
                }
                catch (TemplateNumberOfJbsException ex)
                {
                    logger.Error(ex.Message); 
                    loopsWithProblems.Add(loop.LoopNo);
                }
                catch (TemplateTagTypeNotFoundException ex)
                {
                    logger.Error(ex.Message);
                    loopsWithProblems.Add(loop.LoopNo);
                }
            }
            if(loopsWithProblems.Count > 0)
            {
                ErrorsDetected = true;
                logger.Warning("Loops with problems:");
                logger.Warning(string.Join(",", loopsWithProblems));
            }
        }

        public void SaveDrawingsToFile(string fileName)
        {
            try
            {
                logger.Information("Writing output to JSON file " + fileName);
                var json = JsonConvert.SerializeObject(Drawings, Formatting.Indented);
                File.WriteAllText(fileName, json);
            }
            catch (JsonSerializationException ex)
            {
                logger.Error(ex, "Failed to serialize drawings to JSON");
                throw new SerializationException("Failed to serialize drawings to JSON", ex);
            }
            catch (PathTooLongException ex)
            {
                logger.Error(ex, "File path is too long");
                throw new IOException("File path is too long", ex);
            }
            catch (IOException ex)
            {
                logger.Error(ex, "Failed to write drawings to file");
                throw new IOException("Failed to write drawings to file", ex);
            }
            catch (NotSupportedException ex)
            {
                logger.Error(ex, "File path is not in a valid format");
                throw new IOException("File path is not in a valid format", ex);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Failed to save drawings to file");
                throw;
            }
        }

    }

    public class AcadDrawingControllerException : Exception
    {
        public AcadDrawingControllerException(string? message) : base(message)
        {
        }
        public AcadDrawingControllerException(string? message, Exception? innerException) : base(message, innerException)
        {
        }
    }
}
