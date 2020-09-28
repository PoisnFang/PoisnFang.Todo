using System.Collections.Generic;

namespace PoisnFang.Todo
{
    public class UrlParameters
    {
        public string[] Templates { get; set; }
        public string CurrentTemplate { get; set; }
        public Dictionary<string, string> Paramerters { get; set; }

        public int TemplatePos
        {
            get
            {
                var templatePos = -1;

                for (int i = 0; i < Templates.Length; i++)
                {
                    if (CurrentTemplate == Templates[i])
                    {
                        templatePos = i;
                    }
                }
                return templatePos;
            }
        }
    }
}