using System;
using System.Xml;

namespace RoutinesLibrary.Core.Data
{
    public class XmlHelper
    {
        #region Create document

        public static XmlDocument CreateXmlDocument()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlDeclaration xmlDeclaration = default(XmlDeclaration);
            xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
            xmlDoc.AppendChild(xmlDeclaration);

            return xmlDoc;
        }

        #endregion

        #region Convertions

        public static string StringToXml(string text)
        {
            text = text.Replace("&", "&amp;");
            text = text.Replace("<", "&lt;");
            text = text.Replace(">", "&gt;");
            text = text.Replace("\"", "&quot;");
            text = text.Replace("'", "&apos;");

            return text;
        }

        public static string XmlToString(string text)
        {
            text = text.Replace("&amp;", "&");
            text = text.Replace("&lt;", "<");
            text = text.Replace("&gt;", ">");
            text = text.Replace("&quot;", "\"");
            text = text.Replace("&apos;", "'");

            return text;
        }

        #endregion

        #region Load / Save

        public static XmlDocument LoadFromFile(string fileName)
        {
            XmlReaderSettings xmlSet = new XmlReaderSettings();
            xmlSet.CheckCharacters = false;
            xmlSet.DtdProcessing = DtdProcessing.Ignore;
            xmlSet.ValidationType = ValidationType.None;
            xmlSet.XmlResolver = null;

            XmlReader xmlr = XmlTextReader.Create(fileName, xmlSet);
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlr);
            xmlr.Close();

            return xmlDoc;
        }

        public static void SaveToFile(XmlDocument xmlDoc, string fileName)
        {
            if (xmlDoc != null && fileName != "")
            {
                xmlDoc.Save(fileName);
            }
        }

        #endregion

        #region Add / Remove Nodes

        public static XmlNode AddNode(XmlDocument parentNode, string nodeName, string nodeValue = "")
        {
            XmlElement xmlNewElem = parentNode.CreateElement(nodeName);
            xmlNewElem.InnerText = nodeValue;
            parentNode.AppendChild(xmlNewElem);

            return xmlNewElem;
        }

        public static XmlNode AddNode(XmlNode parentNode, string nodeName, string nodeValue = "")
        {
            return AddNode(parentNode.OwnerDocument, nodeName, nodeValue);
        }

        public static XmlNode SetNode(XmlDocument parentNode, string nodeName, string nodeValue = "")
        {
            XmlNode xmlNewElem = GetFirstChild(parentNode, nodeName);

            if (xmlNewElem == null)
            {
                xmlNewElem = parentNode.CreateElement(nodeName);
                parentNode.AppendChild(xmlNewElem);
            }
            xmlNewElem.InnerText = nodeValue;

            return xmlNewElem;
        }

        public static XmlNode SetNode(XmlNode parentNode, string nodeName, string nodeValue = "")
        {
            return SetNode(parentNode.OwnerDocument, nodeName, nodeValue);
        }

        #endregion

        #region Nodes attributes

        public static XmlAttribute AddAttribute(XmlNode node, string nodeAttrib, string nodeAttribValue)
        {
            XmlDocument xmlDoc = node.OwnerDocument;
            XmlAttribute xmlAttr = xmlDoc.CreateAttribute(nodeAttrib);
            xmlAttr.Value = nodeAttribValue;
            node.Attributes.Append(xmlAttr);

            return xmlAttr;
        }

        public static XmlAttribute SetAttribute(XmlNode node, string nodeAttrib, string nodeAttribValue)
        {
            XmlAttribute xmlAttr = node.Attributes[nodeAttrib];

            if (ReferenceEquals(xmlAttr, null))
            {
                XmlDocument xmlDoc = node.OwnerDocument;
                xmlAttr = xmlDoc.CreateAttribute(nodeAttrib);
                xmlAttr.Value = nodeAttribValue;
                node.Attributes.Append(xmlAttr);
            }
            else
            {
                xmlAttr.Value = nodeAttribValue;
            }

            return xmlAttr;
        }

        #endregion

        #region Search Node

        public static XmlNode GetFirstChild(XmlDocument xmlParent, string sChildName)
        {
            XmlNodeList nodes = xmlParent.ChildNodes;

            foreach (XmlNode node in nodes)
            {
                if (node.Name == sChildName)
                {
                    return node;
                }
            }

            return null;
        }

        public static XmlNode GetFirstChild(XmlNode xmlParent, string sChildName)
        {
            XmlNodeList nodes = xmlParent.ChildNodes;

            foreach (XmlNode node in nodes)
            {
                if (node.Name == sChildName)
                {
                    return node;
                }
            }

            return null;
        }

        public static string GetAbsolutePathFromNode(XmlNode xmlNode)
        {
            string path = "";
            do
            {
                path = "/" + xmlNode.Name + path;
                xmlNode = xmlNode.ParentNode;
            } while (xmlNode != null || xmlNode.NodeType == XmlNodeType.Document);

            return path;
        }

        #endregion
    }
}