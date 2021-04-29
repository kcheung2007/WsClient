using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace WsClient
{
    /// <summary>
    /// Handle user preferences for GUI control. Save the preferences in the XML file for initialization
    /// </summary>
    public partial class UserPreference
    {
        #region Initial combo box control Code
        /// <summary>
        /// Save the list of combo box items by calling other method: WriteComboBoxEntries()
        /// The Write Order is important...
        /// </summary>
        private void SaveComboBoxItem()
        {
            Debug.WriteLine("UcSimpleTest.cs - SaveComboBoxItem");
            XmlTextWriter tw = null;
            try
            {
                string cboPath = Application.StartupPath + "\\InitFile.xml";
                if (!File.Exists(cboPath))
                {
                    File.CreateText(cboPath);
                }

                // Save the combox
                tw = new XmlTextWriter(Application.StartupPath + "\\InitFile.xml", System.Text.Encoding.UTF8);

                Debug.WriteLine("\t ComboBox Item file" + Application.StartupPath + "\\InitFile.xml");

                tw.WriteStartDocument();
                tw.WriteStartElement("InitData");

                // TO DO : Re-work on UserPreference
                //The order is important
                //WriteComboBoxEntries(cboDomainName, "cboDomainName", cboDomainName.Text, tw);
                //WriteComboBoxEntries(cboMailFrom, "cboMailFrom", cboMailFrom.Text, tw);
                //WriteComboBoxEntries(cboRcptTo, "cboRcptTo", cboRcptTo.Text, tw);

                tw.WriteEndElement();
            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine(msg, "SaveComboBoxItem()");
            }//end of catch
            finally
            {
                tw.Flush();
                tw.Close();
            }

            LoadComboBoxes();
        }//end of SaveComboBoxItem

        /// <summary>
        /// Write a list of combox box entries into an xml file
        /// </summary>
        /// <param name="cboBox">ComboBox control</param>
        /// <param name="cboBoxName">Name of the control in XML</param>
        /// <param name="cboBoxText">The input text in combo box</param>
        /// <param name="tw">XmlTextWriter</param>
        private void WriteComboBoxEntries(ComboBox cboBox, string cboBoxName, string txtBoxText, XmlTextWriter tw)
        {
            Debug.WriteLine("UcSimpleTest.cs - WriteComboBoxEntries");
            int maxEntriesToStore = 10;

            tw.WriteStartElement("combobox");
            tw.WriteStartAttribute("name", string.Empty);
            tw.WriteString(cboBoxName);
            tw.WriteEndAttribute();

            // Write the item from the text box first.
            if (txtBoxText.Length != 0)
            {
                tw.WriteStartElement("entry");
                tw.WriteString(txtBoxText);
                tw.WriteEndElement();
                maxEntriesToStore -= 1;
            }//end of if

            // Write the rest of the entries (up to 10).
            for (int i = 0; i < cboBox.Items.Count && i < maxEntriesToStore; ++i)
            {
                if (txtBoxText != cboBox.Items[i].ToString())
                {
                    tw.WriteStartElement("entry");
                    tw.WriteString(cboBox.Items[i].ToString());
                    tw.WriteEndElement();
                }
            }//end of for
            tw.WriteEndElement();
        }//end of WriteComboBoxEntries

        /// <summary>
        /// Load the text value into combo boxes. (OK... hardcode)
        /// </summary>
        private void LoadComboBoxes()
        {
            Debug.WriteLine("UcSimpleTest.cs - LoadComboBoxes");
            try
            {
                // Re-work on the user preference
                //cboDomainName.Items.Clear();
                //cboMailFrom.Items.Clear();
                //cboRcptTo.Items.Clear();

                XmlDocument xdoc = new XmlDocument();
                string cboPath = Application.StartupPath + "\\InitFile.xml";
                if (!File.Exists(cboPath))
                {
                    //File.CreateText(cboPath);
                    SaveComboBoxItem();
                    return;
                }//end of if - full path file doesn't exist

                xdoc.Load(cboPath);
                XmlElement root = xdoc.DocumentElement;
                XmlNodeList nodeList = root.ChildNodes;

                int numComboBox = nodeList.Count;
                int x;
                for (int i = 0; i < numComboBox; i++)
                {
                    switch (nodeList.Item(i).Attributes.GetNamedItem("name").InnerText)
                    {
                        case "cboDomainName":
                            for (x = 0; x < nodeList.Item(0).ChildNodes.Count; ++x)
                            {
                                // Re-work on the user preference
                                //cboDomainName.Items.Add(nodeList.Item(0).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboMailFrom":
                            for (x = 0; x < nodeList.Item(1).ChildNodes.Count; ++x)
                            {
                                // Re-work on the user preference
                                //cboMailFrom.Items.Add(nodeList.Item(1).ChildNodes.Item(x).InnerText);
                            }
                            break;
                        case "cboRcptTo":
                            for (x = 0; x < nodeList.Item(2).ChildNodes.Count; ++x)
                            {
                                // Re-work on the user preference
                                //cboRcptTo.Items.Add(nodeList.Item(2).ChildNodes.Item(x).InnerText);
                            }
                            break;
                    }//end of switch
                }//end of for

                // Re-work on the user preference
                //cboDomainName.Text = cboDomainName.Items[0].ToString();
                //cboMailFrom.Text = cboMailFrom.Items[0].ToString();
                //cboRcptTo.Text = cboRcptTo.Items[0].ToString();

            }//end of try
            catch (Exception ex)
            {
                string msg = ex.Message + "\n" + ex.GetType().ToString() + ex.StackTrace;
                Debug.WriteLine(msg, "Exception");
                MessageBox.Show( msg, "LoadComboBoxes()", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly );
            }//end of catch
        }//end of LoadComboBoxes
        #endregion

    }
}
