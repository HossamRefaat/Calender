using System;
using System.IO;
using System.Collections.Generic;

namespace ooo
{
    class Note
    {
        public string date;
        public string text;
        public Note(string date = null, string text = null)
        {
            this.date = date;
            this.text = text;
        }
    }
    class Database
    {
        const string FILE_PATH = "./db/db.txt";
        public Note[] Read()
        {
            List<Note> notes = new List<Note>();
            string[] lines = File.ReadAllLines(FILE_PATH);
            foreach (string line in lines)
            {
                if (line.Length > 0)
                {

                    if (line[0] == '#')
                    {
                        Note newNote = new Note();
                        newNote.date = line.Substring(2);
                        notes.Add(newNote);
                    }
                    else
                    {
                        notes[notes.Count - 1].text += line;
                    }
                }
            }

            return notes.ToArray();
        }
        public Note GetNoteByDate(string date)
        {
            Note[] notes = Read();

            foreach (Note note in notes)
            {
                if (note.date == date)
                    return note;
            }

            return null;
        }
        public void Add(Note note)
        {
            Console.WriteLine("ADDED");
            using (StreamWriter sw = File.AppendText(FILE_PATH))
            {
                sw.WriteLine("\n# " + note.date);
                sw.WriteLine(note.text);
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

            Add(new Note(date, newText));
        }
    }
    class Program
    {
        static public DateTime[] GetDatesInMonth(int year, int month)
        {
            int count = DateTime.DaysInMonth(year, month);

            DateTime dt = DateTime.Now;
            DateTime[] days = new DateTime[count];

            for (int i = 0; i < count; i++)
            {
                DateTime d = new DateTime(year, month, i + 1);
                days[i] = d;
            }

            return days;
        }
    }
}