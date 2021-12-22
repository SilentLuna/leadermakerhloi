using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace leadermakerhloi
{
    internal class Program
    {
        static ConfigFile config = readConfig.ReadConfigFile();
        static void Main(string[] args)
        {
            CreateFileBatch();
        }
        static string ddsName;
        static List<String> traits = new List<String>();

        static void CreateFileNonBatch(string tag, string name)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), String.Format(@"output\{0}_leader.txt", tag));
            StreamWriter sw = new StreamWriter(path);
            sw.Write("characters = {\n\t");
            int i = 0;
            foreach(string ideo in config.ideologies)
            {
                string s = GetTitleThing(ideo);
                i++;
                sw.Write(String.Format("{0}_{1}_{2} = {{\n\t\t", tag, name, s));
                sw.Write(String.Format("name = {0}_{1}\n\t\t", tag, name));
                sw.Write("portraits = {\n\t\t\t");
                sw.Write("army = {\n\t\t\t\t");
                sw.Write(String.Format("small = \"gfx/leaders/{0}/{1}.dds\"\n\t\t\t", tag, ddsName));
                sw.Write("}\n\t\t\t");
                sw.Write("army = {\n\t\t\t\t");
                sw.Write(String.Format("large = \"gfx/leaders/{0}/{1}.dds\"\n\t\t\t", tag, ddsName));
                sw.Write("}\n\t\t");
                sw.Write("}\n\t\t");
                sw.Write("country_leader = {\n\t\t\t");
                sw.Write(String.Format("ideology = {0}\n\t\t\t", ideo));
                if (config.useTraits == true)
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
                sw.Write(String.Format("expire=\"{0}\"\n\t\t\t", config.expirationDate));
                sw.Write("id =\n\t\t");
                sw.Write("}\n\t");
                if (i < config.ideologies.Length)
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
            for (int i = 0; i < config.names.Length; i++)
            {
                traits.Clear();
                if (config.useMultipleDDSNames == true)
                {
                    ddsName = config.ddsNames[i];
                }
                else
                {
                    ddsName = config.ddsName;
                }
                if (config.useTraits == true)
                {
                    foreach(var Trait in config.traits)
                    {
                        foreach(string var in Trait.nationsWithTrait)
                        {
                            if (var == config.tags[i])
                            {
                                traits.Add(Trait.name);
                                break;
                            }
                        }
                    }
                }    
                CreateFileNonBatch(config.tags[i], config.names[i]);
            }
        }

        static string GetTitleThing(string s)
        {
            switch (s)
            {
                case "pro_yagoo_conservatism":
                    return "democratic";

                case "pro_yagoo_progressivism":
                    return "progressive";

                case "pro_yagoo_monarchism":
                    return "monarchy";

                case "pro_yagoo_kleptocratic":
                    return "yagoo_kleptocratic";

                case "pro_resistance_revolutionary":
                    return "revolutionary";

                case "pro_resistance_nationalist":
                    return "nationalism";

                case "enlightened_monarchy":
                    return "resistance_monarchy";

                case "pro_resistance_kleptocratic":
                    return "resistance_kleptocracy";

                case "kleptocratic_dictator":
                    return "neutral_kleptocratic";

                case "reactionary_nationalist":
                    return "reactionary_nationalism";

                case "holo_theocracy":
                    return "divine_mandate";

                case "mixed_ideology_democrat":
                    return "neutral_democracy";

                case "new_nationalism":
                    return "new_nationalist";

                case "left_wing_revolutionaries":
                    return "left_wing_revolutionary";

                case "reactionary_monarchist":
                    return "reactionary_monarchy";

                default:
                    return "democratic_anti";
            }
        }
    }
}
