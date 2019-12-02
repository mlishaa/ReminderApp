using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReminderApp
{
   public class Reminder
    {

        public string ShortDes { get; set; }
        public string LongDes { get; set; }
        public DateTime DueDate { get; set; }


        public Reminder(string _shortDes,string _longDes,DateTime _dueDate)
        {
            _shortDes = ShortDes;
            _longDes = LongDes;
            _dueDate = DueDate;
        }
        public Reminder()
        {

        }
    }
}
