using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Manifold.Test;
using Manifold.Interop;
using ImportKml.Core;



namespace ImportKml.Tests
{
    [TestFixture]
    class ManifoldIntegrationTests
    {

       
        String kmlFileName;
        Kml kml;


        [TestFixtureSetUp]
        public void Init() {
            kmlFileName = @"C:\Documents and Settings\james\My Documents\Downloads\Testout.kml";
            kml = new Kml(kmlFileName);
            
        }

        [Test]
        public void mapCreated()
        {
            Generator  g = new Generator();
            Assert.IsNotNull(g.Document);
            Assert.GreaterOrEqual(g.Document.ComponentSet.Count,0);
        }

        [Test]
        public void ImportKmlImageTest()
        {
            Generator g = new Generator();
            Int32 counterBefore = g.Document.ComponentSet.Count;
            Manifold.Interop.Document doc = g.Document;
            kml.Import(doc);
            Int32 counterAfter = g.Document.ComponentSet.Count;
            Assert.AreEqual(counterBefore + 1, counterAfter);
            
        }

        [Test]
        public void ImportKmlImageTestSoWeCanFindIt()
        {
            Generator g = new Generator(@"C:\temp\TestMap.map");
            Int32 counterBefore = g.Document.ComponentSet.Count;
            Manifold.Interop.Document doc = g.Document;
            kml.Import(doc);
            Int32 counterAfter = g.Document.ComponentSet.Count;
            Assert.AreEqual(counterBefore + 1, counterAfter);
            doc.Save();

        }



    }
}
