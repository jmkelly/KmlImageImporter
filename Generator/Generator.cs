using System;
using m = Manifold.Interop;
using System.Text;

namespace Manifold.Test
{
    public class Generator
    {

        private m.Application app;
        private m.Document doc;
        private m.Drawing drw;

        public Generator()
        {
            app = new m.Application();
            doc = app.NewDocument(null, false);
            drw = doc.NewDrawing(Guid.NewGuid().ToString(), app.DefaultCoordinateSystem, false);

        }

        public Generator(String MapFile, String Drawing)
        {
            app = new m.Application();
            doc = app.NewDocument(MapFile, false);
            drw = (m.Drawing)doc.ComponentSet[Drawing];
        }

        public Generator(String MapFile)
        {
            app = new m.Application();
            doc = app.NewDocument(MapFile, false);

        }

        public m.Drawing Drawing
        {
            get
            {
                return drw;
            }
        }

        public m.Document Document { get { return doc; } }


        public void Close()
        {
            doc.Close(false);
            app.Quit();
            doc = null;
            drw = null;
            app = null;
        }

    }
}

