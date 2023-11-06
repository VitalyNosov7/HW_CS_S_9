namespace HW_9
{
    class Program
    {
        static void Main()
        {
            ConvertJsonToXml convertJsonToXml_1 = new ConvertJsonToXml("groupJson_1.json", "groupXml_1.xml");
            ConvertJsonToXml convertJsonToXml_2 = new ConvertJsonToXml("groupJson_2.json", "groupXml_2.xml");

            convertJsonToXml_1.ConversionFromJsonToXml();
            convertJsonToXml_2.ConversionFromJsonToXml();
        }
    }
}