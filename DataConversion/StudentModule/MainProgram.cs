using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;

namespace DataConversion
{
    class MainProgram
    {
        static void Main(string[] args)
        {
            string csvFilePath = @".\Resources\StudentMarks.csv";
            string xmlFilePath = @".\Resources\StudentMarks.xml";

            //Converting CSV to XML, comment out after running once.
            convertCsvToXml(csvFilePath, xmlFilePath);

            //Converting XML to C# List
            var studentsMarks = convertXMLtoList(xmlFilePath);

            foreach (var student in studentsMarks)
            {
                Console.Out.WriteLine(student.Physics);
            }
        }



        public static void convertCsvToXml(string csvFilePath, string xmlFilePath)
        {
            var lines = File.ReadAllLines(csvFilePath);
            string[] headers = lines[0].Split(',').Select(x => x).ToArray(); //Getting header values from first line  
            var xml = new XElement("StudentMarks",
              lines.Where((line, index) => index > 0)
              .Select((line, i) => new XElement("Student",
                 line.Split(',').Select((column, index) => new XElement(headers[index], column)))));
            xml.Save(xmlFilePath);
        }

        public static List<Student> convertXMLtoList(string filePath)
        {
            XDocument doc = XDocument.Load(filePath);
            List<Student> studentsMarks = doc.Descendants("Student").Select(x => new Student()
            {
                RollNo = int.Parse(x.Element("roll_no").Value),
                Name = x.Element("name").Value,
                Physics = short.Parse(x.Element("physics").Value),
                Chemistry = short.Parse(x.Element("chemistry").Value),
                Maths = short.Parse(x.Element("maths").Value),
                Biology = short.Parse(x.Element("biology").Value),
                English = short.Parse(x.Element("english").Value)
            }).ToList();

            return studentsMarks;
        }

    }
}
