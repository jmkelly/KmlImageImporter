using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using ImportKml.Core;
using Manifold.Interop;
using Manifold.Interop.Scripts;




namespace ImportKml
{
    public partial class Addin : UserControl, IEventsConnection 
    {

        Manifold.Interop.Application app;
        Manifold.Interop.Document doc;
        Manifold.Interop.ComponentSet comps;


        public Addin()
        {
            InitializeComponent();
        }

        

       

        private void btnFileSelect_Click(object sender, System.EventArgs e)
        {
            openFileDialog1.ShowDialog();

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

            string[] files = openFileDialog1.FileNames;

            Manifold.Interop.History logger = doc.Application.History;




            foreach (string str in files)
            {
                if (KmlFile(str))
                {
                    Kml kml = new Kml(str);
                    try
                    {
                        kml.Import(doc);
                    }
                    catch (Exception ex)
                    {
                        
                        logger.Log(ex.Message + "\n", null);

                    }



                }
            }
        }

        

            private bool KmlFile(string fileName)
            {
                FileInfo file = new FileInfo(fileName);
                if (file.Extension.ToLower()  == ".kml")
                    return true;
                else
                    return false;
            }

           

            public void ConnectEvents(Events ev)
            {
                ev.AddinLoaded += new Events.AddinLoadedEventHandler(ev_AddinLoaded);
                ev.ComponentDataChanged += new Events.ComponentDataChangedEventHandler(ev_ComponentDataChanged);
                ev.ComponentNameChanged += new Events.ComponentNameChangedEventHandler(ev_ComponentNameChanged);
                ev.ComponentProjectionChanged += new Events.ComponentProjectionChangedEventHandler(ev_ComponentProjectionChanged);
                ev.ComponentsAdded += new Events.ComponentsAddedEventHandler(ev_ComponentsAdded);
                ev.ComponentSelectionChanged += new Events.ComponentSelectionChangedEventHandler(ev_ComponentSelectionChanged);
                ev.ComponentsRemoved += new Events.ComponentsRemovedEventHandler(ev_ComponentsRemoved);
                ev.ComponentStateChanged += new Events.ComponentStateChangedEventHandler(ev_ComponentStateChanged);
                ev.DocumentClosed += new Events.DocumentClosedEventHandler(ev_DocumentClosed);
                ev.DocumentCreated += new Events.DocumentCreatedEventHandler(ev_DocumentCreated);
                ev.DocumentOpened += new Events.DocumentOpenedEventHandler(ev_DocumentOpened);
                ev.DocumentSaved += new Events.DocumentSavedEventHandler(ev_DocumentSaved);
                ev.WindowActivated += new Events.WindowActivatedEventHandler(ev_WindowActivated);
            }

            void ev_WindowActivated(object sender, WindowEventArgs Args)
            {
                throw new NotImplementedException();
            }

            void ev_DocumentSaved(object sender, DocumentEventArgs Args)
            {
                throw new NotImplementedException();
            }

            void ev_DocumentOpened(object sender, DocumentEventArgs Args)
            {
                throw new NotImplementedException();
            }

            void ev_DocumentCreated(object sender, DocumentEventArgs Args)
            {
                throw new NotImplementedException();
            }

            void ev_DocumentClosed(object sender, DocumentEventArgs Args)
            {
                throw new NotImplementedException();
            }

            void ev_ComponentStateChanged(object sender, ComponentEventArgs Args)
            {
                throw new NotImplementedException();
            }

            void ev_ComponentsRemoved(object sender, DocumentEventArgs Args)
            {
                throw new NotImplementedException();
            }

            void ev_ComponentSelectionChanged(object sender, ComponentEventArgs Args)
            {
                throw new NotImplementedException();
                
            }

            void ev_ComponentsAdded(object sender, DocumentEventArgs Args)
            {
                throw new NotImplementedException();

            }

            void ev_ComponentProjectionChanged(object sender, ComponentEventArgs Args)
            {
                throw new NotImplementedException();
            }

            void ev_ComponentNameChanged(object sender, ComponentEventArgs Args)
            {
                throw new NotImplementedException();
            }

            void ev_ComponentDataChanged(object sender, ComponentEventArgs Args)
            {
                throw new NotImplementedException();
            }

            void ev_AddinLoaded(object sender, DocumentEventArgs Args)
            {
               
                app = Args.Document.Application;
                doc = Args.Document;
                comps = doc.ComponentSet;
               
            }

            
    }
}
