using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebScraper
{
    
    public class SolverModel
    {
        public Result[] results { get; set; }
    }

    public class Result
    {
        public Status status { get; set; }
        public string name { get; set; }
        public string md5 { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public Entity[] entities { get; set; }
    }

    public class Status
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class Entity
    {
        public string kind { get; set; }
        public string name { get; set; }
        public Object[] objects { get; set; }
    }

    public class Object
    {
        public float[] box { get; set; }
        public Entity1[] entities { get; set; }
    }

    public class Entity1
    {
        public string kind { get; set; }
        public string name { get; set; }
        public string text { get; set; }
    }

}
