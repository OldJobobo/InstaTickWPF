using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InstaTickWPF
{
    public static class Priority
    {
        public static string Low => "Low";
        public static string Medium => "Medium";
        public static string High => "High";
        public static string Urgent => "Urgent";
    }

    [Serializable]
    public class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsComplete { get; set; }
        public string Priority { get; set; }
    }


}
