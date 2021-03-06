using System;
using System.Data;
using System.IO;

namespace DAL
{
    public static partial class XmlConverter
    {
        #region Methods
        
            /// <summary>
            /// Creates a DataSet from a Xml string
            /// </summary>
            /// <param name="Xml_doc">string containing Xml source data</param>
            /// <returns>DataSet containing data</returns>
            public static DataSet XmlStringToDataSet(string Xml_doc)
            {
                if (string.IsNullOrEmpty(Xml_doc))
                    throw new ArgumentException("Cannot serialize a null object");

                DataSet ds = new DataSet();

                StringReader string_reader = new StringReader(Xml_doc);
                ds.ReadXml(string_reader);

                return ds;
             }

            /// <summary>
            /// Creates an Xml string from a DataSet
            /// </summary>
            /// <param name="dt">DataSet containing source data</param>
            /// <returns>string containing Xml doc</returns>
            public static string DataSetToXmlString(DataSet data_set)
            {
                return DataSetToXmlString(data_set, string.Empty);
            }

            /// <summary>
            /// Creates an Xml string from a DataSet
            /// </summary>
            /// <param name="dt">DataSet containing source data</param>
            /// <param name="xml_namespace">string Xml namespace for Xml doc</param>
            /// <returns>string containing Xml doc</returns>
            public static string DataSetToXmlString(DataSet data_set, string xml_namespace)
            {
                data_set.Namespace = xml_namespace;

                return data_set.GetXml();
            }

            /// <summary>
            /// Creates an Xml string from a DataTable
            /// </summary>
            /// <param name="dt">DataTable containing source data</param>
            /// <returns>string containing Xml doc</returns>
            public static string DataTableToXmlString(DataTable data_table)
            {
                return DataTableToXmlString(data_table, string.Empty);
            }

            /// <summary>
            /// Creates an Xml string from a DataTable
            /// </summary>
            /// <param name="dt">DataTable containing source data</param>
            /// <param name="xml_namespace">string Xml namespace for Xml doc</param>
            /// <returns>string containing Xml doc</returns>
            public static string DataTableToXmlString(DataTable data_table, string xml_namespace)
            {
                DataSet data_set = new DataSet();
                data_set.Tables.Add(data_table);

                // parent node for table is created with name of DataSet
                data_set.DataSetName = "TableData";

                return DataSetToXmlString(data_set, xml_namespace);
            }
        
            /// <summary>
            /// Creates a Xml node.
            /// </summary>
            /// <param name="node_name">string name of node</param>
            /// <param name="node_data">string data contained by node</param>
            /// <returns></returns>
            public static string CreateXmlNode(string node_name, string node_data)
            {
                // Fix characters in node_name
                if (node_name.Contains(" "))    node_name = node_name.Replace(" ", string.Empty);  // remove whitespace
                if (node_name.Contains("\\"))   node_name = node_name.Replace("\\", string.Empty); // remove \
                if (node_name.Contains("/"))    node_name = node_name.Replace("/", string.Empty);  // remove /
                if (node_name.Contains("'"))    node_name = node_name.Replace("'", string.Empty);  // remove '
                if (node_name.Contains("\""))   node_name = node_name.Replace("\"", string.Empty); // remove "
                if (node_name.Contains("["))    node_name = node_name.Replace("[", string.Empty);  // remove [
                if (node_name.Contains("]"))    node_name = node_name.Replace("]", string.Empty);  // remove ]
                if (node_name.Contains("&"))    node_name = node_name.Replace("&", string.Empty);  // remove &
                if (node_name.Contains("<"))    node_name = node_name.Replace("<", string.Empty);  // remove <
                if (node_name.Contains(">"))    node_name = node_name.Replace(">", string.Empty);  // remove >

                //if (node_data.Contains("&"))    node_data = node_data.Replace(">","&amp;");     // replace &
                //if (node_data.Contains("<"))    node_data = node_data.Replace("<","&lt;");      // replace <
                //if (node_data.Contains(">"))    node_data = node_data.Replace(">","&gt;");      // replace >
                //if (node_data.Contains("\""))   node_data = node_data.Replace("\"","&quot;");   // replace "
                //if (node_data.Contains("'"))    node_data = node_data.Replace("'","&apos;");    // replace '

                return String.Format("<{0}>{1}</{0}>", node_name, node_data);
            }

        #endregion
    }
}
