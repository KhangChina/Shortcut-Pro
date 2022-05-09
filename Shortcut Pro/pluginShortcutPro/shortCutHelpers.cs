using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace pluginShortcutPro
{
    public class shortCutHelpers
    {
        public string pathData = Application.StartupPath + "/data.json";
       public string pathDataResource = Application.StartupPath + "/Excute";
        public JArray getData()
        {
            var json = File.ReadAllText(pathData);
            var jObject = JObject.Parse(json);
            if (jObject != null)
            {
                JArray experiencesArrary = (JArray)jObject["shortcuts"];
                return experiencesArrary;
            }
            return null;
        }
        public string AddShortCut(string name, string path, string note)
        {
            var json = File.ReadAllText(pathData);
            var jsonObj = JObject.Parse(json);
            var newShortCut = "{ 'name':'" + name + "', 'path': '" + path + "','note': '" + note  + "'}";
            var experienceArrary = jsonObj.GetValue("shortcuts") as JArray;
            var newShort = JObject.Parse(newShortCut);
            experienceArrary.Add(newShort);
            jsonObj["shortcuts"] = experienceArrary;
            string newJsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(pathData, newJsonResult);
            return newJsonResult;
        }
        public string EditShortCut(string note,string path)
        {
            string json = File.ReadAllText(pathData);
            var jObject = JObject.Parse(json);
            JArray shortcutsArrary = (JArray)jObject["shortcuts"];
            foreach (var shortCuts in shortcutsArrary.Where(obj => obj["path"].Value<string>() == path))
            {
                //shortCuts["name"] = !string.IsNullOrEmpty(name) ? name : "";
                //shortCuts["path"] = !string.IsNullOrEmpty(path) ? path : "";
                shortCuts["note"] = !string.IsNullOrEmpty(note) ? note : "";
            }
            jObject["shortcuts"] = shortcutsArrary;
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(pathData, output);
            return output;
        }
        public string deleteShortCut(string name)
        {
            var json = File.ReadAllText(pathData);
            var jObject = JObject.Parse(json);
            JArray experiencesArrary = (JArray)jObject["shortcuts"];
            var companyToDeleted = experiencesArrary.FirstOrDefault(obj => obj["name"].Value<string>() == name);
            experiencesArrary.Remove(companyToDeleted);
            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jObject, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(pathData, output);
            return output;
        }
        public void RunShortcuts(string path)
        {

            ProcessStartInfo start = new ProcessStartInfo();
            start.FileName = path;
            start.WorkingDirectory = Path.GetDirectoryName(path);
            ThreadStart thr = new ThreadStart(()=> Process.Start(start));
            Thread th = new Thread(thr);
            th.Start();
           

        }
        public Icon ExtractIconFromFilePath(string executablePath)
        {
            Icon result = (Icon)null;

            try
            {
                result = Icon.ExtractAssociatedIcon(executablePath);
            }
            catch (Exception)
            {
                MessageBox.Show("Unable to extract the icon from the binary");
            }

            return result;
        }
    }
}
