using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ImportKml.Core;

namespace ImportKml.Tests
{
    [TestFixture]
    public class UnitTests
    {

        Kml kml;
        String kmlFileName;

        [TestFixtureSetUp]
        public void Init()
        {
            kmlFileName = @"C:\Documents and Settings\james\My Documents\Downloads\Testout.kml";
            kml = new Kml(kmlFileName);
        }


        [Test]
        public void NewCoordinateTest()
        {
            Coordinate coord = new Coordinate(147.123, -42.123);
            Assert.AreEqual(147.123, coord.Lon);
            Assert.AreEqual(-42.123, coord.Lat);
        }

        [Test]
        public void NewBoundingBoxTest()
        {
            Coordinate ne = new Coordinate(145.26861838, -38.23078764);
            Coordinate sw = new Coordinate(145.25965267, -38.23464633);

            BoundingBox bb = new BoundingBox(ne, sw);
            Assert.AreEqual(-38.23078764, bb.NorthEast.Lat);
            Assert.AreEqual(145.25965267, bb.SouthWest.Lon);
            Assert.AreEqual(-38.23464633, bb.SouthWest.Lat);
            Assert.AreEqual(145.26861838, bb.NorthEast.Lon);
        }

        

        [Test]
        public void LoadKmlTest()
        {
           
            Assert.AreEqual(kmlFileName, kml.KmlFileName);
        }

        [Test]
        public void GetKmlBoundingBoxTest()
        {
           
            BoundingBox bb = kml.BoundingBox();
            Assert.AreEqual(-38.23078764, bb.NorthEast.Lat);
            Assert.AreEqual(145.25965267, bb.SouthWest.Lon);
            Assert.AreEqual(-38.23464633, bb.SouthWest.Lat);
            Assert.AreEqual(145.26861838, bb.NorthEast.Lon);
        }

        [Test]
        public void GetImageTest()
        { 
           
            string name = "Testout.jpg";
            Assert.AreEqual(name, kml.ImageFileName());
            

        }

        [Test]
        public void GetCurrentDirectoryTest()
        {           
            string currentDirectory = @"C:\Documents and Settings\james\My Documents\Downloads";
            Assert.AreEqual(currentDirectory , kml.CurrentDirectory());
        }

        [Test]
        public void GetImageExtensionTest()
        {
            string jpg = @".jpg";
            Assert.AreEqual(jpg, kml.ImageFileType());
        }

        [Test]
        public void GetFullFileNameTest()
        {
            string fullName = @"C:\Documents and Settings\james\My Documents\Downloads\Testout.jpg";
            Assert.AreEqual(fullName, kml.ImageFileFullName());
        }

        
    }
}
