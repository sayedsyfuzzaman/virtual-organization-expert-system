using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Data.SqlClient;

namespace VOES.Controllers
{
    class Event
    {
        String eventName, startDate, endDate, description, location, status, addedby, additionDate;
        private byte[] image;
        public String AdditionDate
        {
            get { return additionDate; }
            set { additionDate = value; }
        }

        public String Addedby
        {
            get { return addedby; }
            set { addedby = value; }
        }
        

        public byte[] Image
        {
            get { return image; }
            set { image = value; }
        }

        public String Status
        {
            get { return status; }
            set { status = value; }
        }

        public String Location
        {
            get { return location; }
            set { location = value; }
        }

        public String Description
        {
            get { return description; }
            set { description = value; }
        }

        public String EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public String StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public String EventName
        {
            get { return eventName; }
            set { eventName = value; }
        }
        
    }
}
