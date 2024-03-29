﻿using System.Text;
using System.Text.Json;

namespace ChatLibrary
{
    public class MSG
    {
        internal string sender_name;
        internal string text;
        internal DateTime timeOfGetting;
        public MSG(string sender_name, string text, DateTime timeOfGetting)
        {
            this.sender_name = sender_name;
            this.text = text;
            this.timeOfGetting = timeOfGetting;
        }
        public static explicit operator MSG(MSG_Serialization ms)
        {
            return new MSG(ms.senderName, ms.text, ms.timeOfGetting);
        }
        public override string ToString()
        {
            return $"{timeOfGetting.ToString()} {sender_name}: {text}";
        }
        public static List<MSG> GetAllMSGs(string jsonFileName)
        {
            List<MSG> msgs = new List<MSG>();
            string s;
            using(var reader=new StreamReader(jsonFileName))
            {
                while ((s = reader.ReadLine()) != null)
                {
                    if (s == "" || s == "{}") return msgs;
                    msgs.Add((MSG)JsonSerializer.Deserialize<MSG_Serialization>(s));
                }
            }
            return msgs;
        }
        public void WriteMsgToFile(string jsonFileName)
        {
            using(var writer=new StreamWriter(jsonFileName,true))
            {
                writer.WriteLine(JsonSerializer.Serialize<MSG_Serialization>((MSG_Serialization)this));
            }
        }
        public static MSG GetMSG(StringBuilder builder)
        {
            string s = builder.ToString();
            string[] tmp = builder.ToString().Split(' ');
            builder.Clear();
            for (int i = 3; i < tmp.Length; i++)
                builder.Append($"{tmp[i]} ");
                return new MSG(tmp[2]/*Sender*/, builder.ToString()/*Text*/, DateTime.Parse($"{tmp[0]} {tmp[1]}"));
        }
    }
}