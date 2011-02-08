using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.IO;
using M = Manifold.Interop;


namespace ImportKml.Core
{


    public class Kml
    {
        public Kml(string kmlFileName){
            this.KmlFileName = kmlFileName;  
        }

        

        public String KmlFileName { get; set; }

        public void Import(M.Document document)
        {

            List<Int32> beforeIds = new List<Int32>();
            List<Int32> afterIds = new List<Int32>();
         
            
            

            foreach(M.Component comp in document.ComponentSet)
            {
                beforeIds.Add(comp.ID);
            }


            if (this.ImageFileType() == ".jpg")
            {


                M.ImportJpeg importer = (M.ImportJpeg)document.NewImport("JPEG");               
                importer.Import(ImageFileFullName(), M.ConvertPrompt.PromptNone, false);

                Int32 Id = 0;
                if (document.ComponentSet.Count == 1)
                {
                    Id = document.ComponentSet[0].ID;
                }
                else
                {


                    foreach (M.Component comp in document.ComponentSet)
                    {
                        afterIds.Add(comp.ID);
                    }

                   


                    beforeIds.Sort();
                    afterIds.Sort();

                    Int32 i = 0;
                    Int32 j = 0;
                    bool isnew = false;
                    bool matched = false;
                    Int32 LastNewID = -1;
                    i = document.ComponentSet.Count - 1;
                    isnew = false;
                    do
                    {
                        j = beforeIds.Count - 1;
                        matched = false;
                        do
                        {
                            if (afterIds[i] == beforeIds[j])
                                matched = true;
                            else
                                j = j - 1;

                        }
                        while (matched == false && j > 0);
                        if (matched == false)
                            isnew = true;
                        else
                            i = i - 1;

                    }
                    while (isnew == false && i != -1);

                    if (isnew)
                    {
                        LastNewID = afterIds[i];
                    }

                    Id = LastNewID;

                }
                M.Image img = (M.Image)document.ComponentSet[document.ComponentSet.ItemByID(Id)];
                M.CoordinateSystem coordinateSystem = document.Application.NewCoordinateSystem("Latitude / Longitude");
                M.Datum datum = document.Application.NewDatum("World Geodetic 1984 (WGS84) Auto");
                coordinateSystem.Datum = datum;
                
                coordinateSystem.Parameters["localOffsetX"].Value = BoundingBox().SouthWest.Lon ;
                coordinateSystem.Parameters["localOffsetY"].Value = BoundingBox().SouthWest.Lat;
                coordinateSystem.Parameters["localScaleX"].Value = Math.Abs((BoundingBox().SouthWest.Lon - BoundingBox().NorthEast.Lon) / img.Width);
                coordinateSystem.Parameters["localScaleY"].Value = Math.Abs ((BoundingBox().SouthWest.Lat - BoundingBox().NorthEast.Lat) / img.Height);
                img.CoordinateSystem = coordinateSystem;
                Console.WriteLine("Width " + img.Width.ToString());
                Console.WriteLine("Height " + img.Height.ToString());
                Console.WriteLine("X diff" + (this.BoundingBox().SouthWest.Lon - this.BoundingBox().NorthEast.Lon).ToString());
                Console.WriteLine("Y diff" + (this.BoundingBox().SouthWest.Lat - this.BoundingBox().NorthEast.Lat).ToString());

                //img.CoordinateSystem.Preset = "Latitude / Longitude";
                

            }
            else
            {
                throw new NotImplementedException("kml import only imports jpg image files at present");
            }
        }


        public string ImageFileFullName()
        {
            return CurrentDirectory() + @"\" + ImageFileName();
        }

        public string CurrentDirectory()
        {
            
            FileInfo file = new FileInfo(this.KmlFileName);
            DirectoryInfo dir = file.Directory;
            return dir.FullName;
        }

        public string ImageFileType()
        {
            FileInfo file = new FileInfo(this.ImageFileName());
            return file.Extension;

        }


        public String ImageFileName()
        {
            XDocument doc = XDocument.Load(this.KmlFileName);
            XNamespace ns = "http://earth.google.com/kml/2.2";

            IEnumerable<string> imageName = from placemark in doc.Descendants(ns + "Icon")
                                                select placemark.Element(ns + "href").Value;
            return imageName.First().ToString();
        }

        public BoundingBox BoundingBox()
        {
            
            XDocument doc = XDocument.Load(this.KmlFileName);
            XNamespace ns = "http://earth.google.com/kml/2.2";
            
            IEnumerable<string> northElements = from placemark in doc.Descendants(ns + "LatLonBox")
                                          select placemark.Element(ns + "north").Value;

            double north = Convert.ToDouble(northElements.First());
            
            

            var southElements = from placemark in doc.Descendants(ns + "LatLonBox")
                               select placemark.Element(ns + "south").Value;

            double south = Convert.ToDouble(southElements.First());
            
            var eastElement = from placemark in doc.Descendants(ns + "LatLonBox")
                               select placemark.Element(ns + "east").Value;

            double east = Convert.ToDouble(eastElement.First());

            var westElement = from placemark in doc.Descendants(ns + "LatLonBox")
                               select placemark.Element(ns + "west").Value;

            double west = Convert.ToDouble(westElement.First());
            
            Coordinate ne = new Coordinate(east, north);
            Coordinate sw = new Coordinate(west, south);
            BoundingBox bb = new BoundingBox(ne, sw);
            return bb;
        }
    }

   

    
}
