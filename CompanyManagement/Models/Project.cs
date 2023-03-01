using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyManagement
{
    public class Project
    {
        private string id;
        private string name;
        private DateTime start;
        private DateTime end;
        private int budject;
        private string status;

        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int Budget { get; set; }
        public string Status { get; set; }
    }
}
