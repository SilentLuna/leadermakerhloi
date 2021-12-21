using System;
using System.IO;
using System.Reflection;

namespace leadermakerhloi
{
    internal class Program
    {
        static string[] ideologies;
        static string[] traits = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"resrc\trait.txt"));
        static void Main(string[] args)
        {
            ideologies = GetIdeologies();
            if (args[0] == "b")
            {
                CreateFileBatch();
            }
            else
            {
                CreateFileNonBatch(args[0], args[1]);
            }
        }

        static string[] GetIdeologies()
        {
            string[] ideologies = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"resrc\ideo.txt"));
            return ideologies;
        }

        static void CreateFileNonBatch(string tag, string name)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), String.Format(@"output\{0}_leader.txt", tag));
            StreamWriter sw = new StreamWriter(path);
            sw.Write("characters = {\n\t");
            int i = 0;
            foreach(string ideo in ideologies)
            {
                string s = GetTitleThing(ideo);
                i++;
                sw.Write(String.Format("{0}_{1}_{2} = {{\n\t\t", tag, name, s));
                sw.Write(String.Format("name = {0}_{1}\n\t\t", tag, name));
                sw.Write("portraits = {\n\t\t\t");
                sw.Write("army = {\n\t\t\t\t");
                sw.Write(String.Format("small = \"gfx/leaders/{0}/danchou.dds\"\n\t\t\t", tag));
                sw.Write("}\n\t\t\t");
                sw.Write("army = {\n\t\t\t\t");
                sw.Write(String.Format("large = \"gfx/leaders/{0}/danchou.dds\"\n\t\t\t", tag));
                sw.Write("}\n\t\t");
                sw.Write("}\n\t\t");
                sw.Write("country_leader = {\n\t\t\t");
                sw.Write(String.Format("ideology = {0}\n\t\t\t", ideo));
                if (traits != null)
                {
                    sw.Write("traits = { ");
                    foreach(string var in traits)
                    {
                        sw.Write(var);
                        sw.Write(" ");
                    }
                    sw.Write("}\n\t\t\t");
                }
                else
                {
                    sw.Write("traits = { }\n\t\t\t");
                }
                sw.Write("expire=\"1965.1.1.1\"\n\t\t\t");
                sw.Write("id =\n\t\t");
                sw.Write("}\n\t");
                if (i < ideologies.Length)
                {
                    sw.Write("}\n\t");
                }
                else
                {
                    sw.Write("}\n");
                }
            }
            sw.Write("}");
            sw.Close();
        }

        static void CreateFileBatch()
        {
            string[] var0 = File.ReadAllLines(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"resrc\name_tag.txt"));
            foreach(string sVar0 in var0)
            {
                string[] saVar0 = sVar0.Split(',');
                CreateFileNonBatch(saVar0[0], saVar0[1]);
            }
        }

        static string GetTitleThing(string s)
        {
            if (s == "pro_yagoo_conservatism")
            {
                return "democratic";
            }
            else if (s == "pro_yagoo_progressivism")
            {
                return "progressive";
            }
            else if (s == "pro_yagoo_monarchism")
            {
                return "monarchy";
            }
            else if (s == "pro_yagoo_kleptocratic")
            {
                return "yagoo_kleptocratic";
            }
            else if (s == "pro_resistance_revolutionary")
            {
                return "revolutionary";
            }
            else if (s == "pro_resistance_nationalist")
            {
                return "nationalism";
            }
            else if (s == "enlightened_monarchy")
            {
                return "resistance_monarchy";
            }
            else if (s == "pro_resistance_kleptocratic")
            {
                return "resistance_kleptocracy";
            }
            else if (s == "kleptocratic_dictator")
            {
                return "neutral_kleptocratic";
            }
            else if (s == "reactionary_nationalist")
            {
                return "reactionary_nationalism";
            }
            else if (s == "holo_theocracy")
            {
                return "divine_mandate";
            }
            else if (s == "mixed_ideology_democrat")
            {
                return "neutral_democracy";
            }
            else if (s == "new_nationalism")
            {
                return "new_nationalist";
            }
            else if (s == "left_wing_revolutionaries")
            {
                return "left_wing_revolutionary";
            }
            else if (s == "reactionary_monarchist")
            {
                return "reactionary_monarchy";
            }
            else
            {
                return "democratic_anti";
            }
        }
    }
}
