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
        private int budget;
        private string status;

        public string ID
        {
            get { return this.id; }
            set { this.id = value; }
        } 
            
        public string Name
        {
            get { return this.name; }
            set { this.name = value; }
        }
        public DateTime Start
        {
            get { return this.start; }
            set { this.start = value; }
        }
        public DateTime End
        {
            get { return this.end; }
            set { this.end = value; }
        }
        public int Budget
        {
            get { return this.budget; }
            set { this.budget = value; }
        }
        public string Status
        {
            get { return this.status; }
            set { this.status = value; }
        }

        public Project() { }
        public Project(string id, string name, DateTime start, DateTime end, int budget, string status)
        {
            this.id = id;
            this.name = name;
            this.start = start;
            this.end = end;
            this.budget = budget;
            this.status = status;
        }
        ~Project() { }
    }
}
