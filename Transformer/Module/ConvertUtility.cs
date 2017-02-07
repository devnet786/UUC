using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using Transformer.Common;

namespace Transformer.Module
{
    class ConvertUtility
    {
        private String resultDoc = "result.xml";

        public string utility(string SourceXMl)
        {

            string sourceDoc = SourceXMl + ".xml";
            string xsltDoc = "TotalXMLTemplate.xml";
            XPathDocument myXPathDocument = new XPathDocument(sourceDoc);
            XslTransform myXslTransform = new XslTransform();
            XmlTextWriter writer = new XmlTextWriter(resultDoc, null);
            myXslTransform.Load(xsltDoc);
            myXslTransform.Transform(myXPathDocument, null, writer);
            writer.Close();
            StreamReader stream = new StreamReader(resultDoc);
            return stream.ReadToEnd();

        }	
    }
}
