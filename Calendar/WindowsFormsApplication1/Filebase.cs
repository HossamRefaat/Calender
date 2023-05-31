using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WindowsFormsApplication1
{
    class NoteClass
    {
        public string date;
        public string text;

        public NoteClass(string date = null, string text = null)
        {
            this.date = date;
            this.text = text;
        }
    }
    class Filebase
    {
        const string FILE_PATH = "./db/db.txt";

        public NoteClass[] Read()
        {
            List<NoteClass> notes = new List<NoteClass>();
            string[] lines = File.ReadAllLines(FILE_PATH);
            foreach (string line in lines)
            {
                if (line.Length > 0)
                {
                    if (line[0] == '#')
                    {
                        NoteClass newNoteClass = new NoteClass();
                        newNoteClass.date = line.Substring(2);
                        notes.Add(newNoteClass);
                    }
                    else
                    {
                        notes[notes.Count - 1].text += line;
                    }
                }
            }
            return notes.ToArray();
        }

        public string[] ReadLinesByDate(string date)
        {
            string[] lines = File.ReadAllLines(FILE_PATH);
            int len = lines.Length, sindex = -1, eindex = lines.Length;

            for (int i = 0; i < len; i++)
            {
                if (lines[i].Length > 0 && lines[i][0] == '#' && lines[i].Substring(2) == date)
                {
                    sindex = i;
                    break;
                }
            }

            if (sindex != -1)
                for (int i = sindex + 1; i < len; i++)
                {
                    if (lines[i].Length > 0 && lines[i][0] == '#')
                    {
                        eindex = i - 1;
                        break;
                    }
                }


            if (sindex == -1 && eindex == lines.Length) return null;

            List<string> newLines = new List<string>();
            for (int i = 0; i < len; i++)
            {
                if (!(i < sindex || i > eindex) && lines[i].Length > 0)
                    newLines.Add(lines[i]);
            }

            newLines.RemoveAt(0);

            return newLines.Count == 0 ? null : newLines.ToArray();
        }

        public NoteClass GetNoteClassByDate(string date)
        {
            NoteClass[] NoteClasss = Read();

            foreach (NoteClass NoteClass in NoteClasss)
            {
                if (NoteClass.date == date)
                    return NoteClass;
            }

            return null;
        }

        public void Add(NoteClass NoteClass)
        {
            Console.WriteLine("ADDED");
            using (StreamWriter sw = File.AppendText(FILE_PATH))
            {
                sw.WriteLine("\n# " + NoteClass.date);
                sw.WriteLine(NoteClass.text);
                sw.Close();
            }
        }
        public void Delete(string date)
        {
            string[] lines = File.ReadAllLines(FILE_PATH);
            int len = lines.Length, sindex = -1, eindex = lines.Length;

            for (int i = 0; i < len; i++)
            {
                if (lines[i].Length > 0 && lines[i][0] == '#' && lines[i].Substring(2) == date)
                {
                    sindex = i;
                    break;
                }
            }
            if (sindex != -1)
                for (int i = sindex + 1; i < len; i++)
                {
                    if (lines[i].Length > 0 && lines[i][0] == '#')
                    {
                        eindex = i - 1;
                        break;
                    }
                }
            if (sindex == -1 && eindex == lines.Length) return;
            List<string> newLines = new List<string>();
            for (int i = 0; i < len; i++)
            {
                if ((i < sindex || i > eindex) && lines[i].Length > 0)
                    newLines.Add(lines[i]);
            }
            Console.WriteLine("DELETING");
            File.WriteAllLines(FILE_PATH, newLines.ToArray());
        }
        public void Update(string date, string newText)
        {
            Delete(date);
            Add(new NoteClass(date, newText));
        }
    }
}
