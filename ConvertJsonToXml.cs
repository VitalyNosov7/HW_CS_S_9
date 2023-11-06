using System.Text.Json;
using System.Xml;

namespace HW_9
{
    public class ConvertJsonToXml
    {
        private string PathToFileJson = string.Empty;
        private string PathToFileXml = string.Empty;

        public ConvertJsonToXml(string pathToFileJson, string pathToFileXml)
        {
            PathToFileJson = pathToFileJson;
            PathToFileXml = pathToFileXml;
        }

        public string GetPathToFileJson()
        {
            return PathToFileJson;
        }

        public string GetPathToFileXml()
        {
            return PathToFileXml;
        }
        public void ConversionFromJsonToXml()
        {
            ConvertToXml();
            ReadTheCreatedXmlFile();
        }

        public bool CheckForFileExistence(string pathToFile)
        {
            if (!File.Exists(pathToFile))
            {
                return false;
            }
            return true;
        }

        private void ConvertToXml()
        {
            try
            {
                if (!CheckForFileExistence(PathToFileJson))
                {
                    throw new FileNotFoundException($"Файл \"{PathToFileJson}\" не найден!");
                }
                else
                {
                    Console.WriteLine($"Файл формата JSON \"{PathToFileJson}\"" +
                                        $" будет конвертирован и записан в файл формата XML  \"{PathToFileXml}\".");

                    using (StreamReader reader = new StreamReader(PathToFileJson))
                    {
                        PathToFileJson = reader.ReadToEnd();
                    }

                    using (JsonDocument document = JsonDocument.Parse(PathToFileJson))
                    {

                        XmlDocument xmlDoc = new XmlDocument();
                        XmlDeclaration xmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
                        XmlElement rootElement = xmlDoc.CreateElement("Students");
                        xmlDoc.InsertBefore(xmlDeclaration, xmlDoc.DocumentElement);

                        foreach (JsonElement element in document.RootElement.EnumerateArray())
                        {

                            XmlElement peopleElement = xmlDoc.CreateElement("Person");

                            string? lastName = element.GetProperty("LastName").GetString();
                            string? firstName = element.GetProperty("FirstName").GetString();
                            string? fatherName = element.GetProperty("FatherName").GetString();
                            int age = element.GetProperty("Age").GetInt32();
                            int group = element.GetProperty("Group").GetInt32();

                            XmlElement lastNameElement = xmlDoc.CreateElement("LastName");
                            lastNameElement.InnerText = lastName;
                            peopleElement.AppendChild(lastNameElement);

                            XmlElement firstNameElement = xmlDoc.CreateElement("FirstName");
                            firstNameElement.InnerText = firstName;
                            peopleElement.AppendChild(firstNameElement);

                            XmlElement fatherNameElement = xmlDoc.CreateElement("FatherName");
                            fatherNameElement.InnerText = fatherName;
                            peopleElement.AppendChild(fatherNameElement);

                            XmlElement ageElement = xmlDoc.CreateElement("Age");
                            ageElement.InnerText = age.ToString();
                            peopleElement.AppendChild(ageElement);

                            XmlElement groupElement = xmlDoc.CreateElement("Group");
                            groupElement.InnerText = group.ToString();
                            peopleElement.AppendChild(groupElement);

                            rootElement.AppendChild(peopleElement);
                        }

                        xmlDoc.AppendChild(rootElement);

                        xmlDoc.Save(PathToFileXml);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"Ошибка: {ex.Message}");
                Console.ResetColor();
            }
        }

        private void ReadTheCreatedXmlFile()
        {
            try
            {
                if (!CheckForFileExistence(PathToFileXml))
                {
                    throw new FileNotFoundException($"Файл \"{PathToFileXml}\" не найден!");
                }
                else
                {
                    Console.WriteLine($"Файл \"{PathToFileXml}\" содержит следубщие данные:");

                    string[] lines = File.ReadAllLines(PathToFileXml);
                    foreach (string line in lines)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Error.WriteLine($"Ошибка: {ex.Message}");
                Console.ResetColor();
            }
        }
    }
}