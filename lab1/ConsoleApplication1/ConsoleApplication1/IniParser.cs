using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace ConsoleApplication1
{
    public class IniParser
    {
        public Dictionary<string, Dictionary<string, string>> _data =
            new Dictionary<string, Dictionary<string, string>>();
        public IniParser (string filename)
        {
            var f = new StreamReader(filename);
            try
            {
                if (!File.Exists(filename))
                    throw new IniParserException("File doesn't exist");
                string section_name = "", line;
                while ((line = f.ReadLine()) != null)
                {
                    var pos = line.IndexOf(';');
                    if (pos >= 0)
                        line = line.Substring(0, pos);
                    line = line.Replace(" = ", "=");
                    line = line.TrimEnd(' ');
                    if (line == "")
                        continue;
                    pos = line.IndexOf(' ');
                    if (pos >= 0)
                        throw new IniParserException("Incorrect file format");
                    if (line[0] == '[')
                    {
                        if (line[line.Length - 1] != ']')
                            throw new IniParserException("Incorrect file format");
                        section_name = line.Substring(1, line.IndexOf(']') - 1);
                        _data[section_name] = new Dictionary<string, string>();
                    }
                    else
                    {
                        var check = line.Split('=');
                        if ((check.Length != 2) || (section_name == ""))
                            throw new IniParserException("Incorrect file format");
                        _data[section_name][check[0]] = check[1];
                    }
                }
            }
            catch (IniParserException e)
            {
                Console.WriteLine(e);
                Environment.Exit(1);
            }
            finally
            {
                if (f != null)
                {
                    f.Close();
                }
            }
        }
        
        public T TryGet<T>(string section_name, string key)
        {
            T tmp = default;
            try
            {
                if (!_data.ContainsKey(section_name))
                    throw new IniParserException("Incorrect section name");
                if (!_data[section_name].ContainsKey(key))
                    throw new IniParserException("Incorrect parametr name");
                tmp = (T) Convert.ChangeType(_data[section_name][key], typeof(T));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Environment.Exit(1);
            }
            return tmp;
        }

        public int TryGetInt(string section_name, string key)
        {
            return TryGet<int>(section_name, key);
        }
        public void TryGetDouble(string section_name, string key)
        {
            TryGet<double>(section_name, key);
        }
        public void TryGetString(string section_name, string key)
        {
            TryGet<string>(section_name, key);
        }
    }
}