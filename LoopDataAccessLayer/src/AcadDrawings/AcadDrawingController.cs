using LoopDataAdapterLayer;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace LoopDataAccessLayer
{
    public class AcadDrawingController
    {
        
        private readonly LoopDataConfig loopConfig;
        private readonly IDataLoader dataLoader;

        public List<AcadDrawingDataMappable> Drawings { get; set; }

        public AcadDrawingController(IDataLoader dataLoader, LoopDataConfig loopConfig)
        {
            this.dataLoader = dataLoader;
            this.loopConfig = loopConfig;
            var titleBlock = dataLoader.GetTitleBlockData();
            loopConfig.SiteID = titleBlock.SiteNumber;
            Drawings = new List<AcadDrawingDataMappable>();
        }

        public AcadDrawingController(
            string excelFileName,
            string configFileName,
            string templatePath,
            string outputDrawingPath)
        {
            // figure out the rigth exception when the excel is open
            ExcelDataLoader excelLoader;
            try
            {
                excelLoader = new(excelFileName);
            }
            catch (IOException)
            {
                throw new IOException("Excel must be open - please close it.");
            }

            // test the exception for this if can't connect to internet
            DBDataLoader dbLoader = new();

            // shouldn't be any exceptions here
            dataLoader = new DataLoader(excelLoader, dbLoader);
            
            // setup the loop config
            try
            {
                loopConfig = new(configFileName);
                loopConfig.LoadConfig();
                loopConfig.TemplateDrawingPath = templatePath;
                loopConfig.OutputDrawingPath = outputDrawingPath;
                var titleBlock = dataLoader.GetTitleBlockData();
                loopConfig.SiteID = titleBlock.SiteNumber;
            }
            catch (FileNotFoundException)
            {
                string msg = "Unable to find"
                    + Environment.NewLine
                    + configFileName
                    + Environment.NewLine
                    + "Please relocate the file and try again.";
                throw new FileNotFoundException(msg);
            }

            

            Drawings = new List<AcadDrawingDataMappable>();
        }


        public void BuildDrawings()
        {
            AcadBlockFactory blockFactory = new(dataLoader);
            TemplatePicker templatePicker = new(dataLoader, loopConfig);
            AcadDrawingBuilder drawingBuilder = new(dataLoader, loopConfig, templatePicker, blockFactory);
            IEnumerable<LoopNoTemplatePair> loops = dataLoader.GetLoops();
            foreach (LoopNoTemplatePair loop in loops)
            {
                IEnumerable<AcadDrawingDataMappable> drawings = drawingBuilder.BuildDrawings(loop);
                foreach(AcadDrawingDataMappable drawing in drawings)
                {
                    Drawings.Add(drawing);
                }
            }
        }

        public void SaveDrawingsToFile(string fileName)
        {
            var json = JsonConvert.SerializeObject(Drawings, Formatting.Indented);
            File.WriteAllText(fileName, json);
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
