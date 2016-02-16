using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail
{
    public class Email
    {
        public string Raw { get; private set; }
        public string Body { get; private set; }
        public Dictionary<string, List<string>> Header { get; private set; }

        public Email(string raw)
        {
            Body = "";
            Raw = raw.Replace("\r","");
            Header = new Dictionary<string, List<string>>();
            parse();
        }

        // Return a header as list of string
        public List<string> GetHeaders(string key)
        {
            key = key.ToLower();

            if (Header.ContainsKey(key))
            {
                return Header[key];
            }
            return new List<string>();
        }

        /// <summary>
        ///  Returns single header
        /// </summary>
        /// <param name="key">header key</param>
        /// <returns>header value</returns>
        public string GetHeader(string key)
        {
            List<string> headers = GetHeaders(key);
            if (headers.Count > 0)
            {
                return headers.First();
            }
            return "";
        }


        // parse an email -> string Body and Dictionary Header
        private void parse()
        {
            string rawHeader = "";
            int l = this.Raw.Length;

            /*if (l == 0)
            {
                throw new System.Exception("you must call Read() first with a valid email. Email.Raw is empty");
            }*/

            char prev = (char)0;
            int i;

            for (i = 0; i < l; i++)
            {
                if (prev == 10 && Raw[i] == 10)
                {
                    rawHeader = rawHeader.Substring(0, rawHeader.Length - 1);
                    break;
                }
                prev = Raw[i];
                rawHeader += prev;
            }

            if (i < l)
            {
                Body = Raw.Substring(i + 1);
            }
        
             // Parse headers
             List<string> hLines = new List<string>();
             l = rawHeader.Length;
             string currentHeaderLine = "";

             for (i = 0; i < l; i++)
             {
                 // split or end of line ?
                 if (rawHeader[i] == 10)
                 {
                     // split
                     if (l > i + 2 && (rawHeader[i + 1] == 9 || rawHeader[i + 1]==32))
                     {
                        i=i+2;
                        continue;   
                     }
                     // New Header
                     else
                     {
                         hLines.Add(currentHeaderLine);
                         currentHeaderLine = "";
                     }
                 }
                 else
                 {
                     currentHeaderLine += rawHeader[i];
                 }
             }

             // we got hLines
             string[] kv;
             string headerKey;
             foreach (string line in hLines)
             {
                 kv = line.Split(new char[] { ':' }, 2);
                 if (kv.Length != 2)
                 {
                     throw new System.Exception("Found bad header line: " + line);
                 }
                headerKey = kv[0].ToLower();
                 // add entry to Header
                 if (!Header.ContainsKey(headerKey))
                 {
                     Header[headerKey] = new List<string>();
                 }
                 Header[headerKey].Add(kv[1]);
             }
        }
    }
}

