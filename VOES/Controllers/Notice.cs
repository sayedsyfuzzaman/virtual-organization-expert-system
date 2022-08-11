using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOES.Controllers
{
    class Notice
    {
        private String serial, subject, body, postedby, postdate;

        public String Serial
        {
            get { return serial; }
            set { serial = value; }
        }

        public String Postdate
        {
            get { return postdate; }
            set { postdate = value; }
        }

        public String Postedby
        {
            get { return postedby; }
            set { postedby = value; }
        }

        public String Body
        {
            get { return body; }
            set { body = value; }
        }

        public String Subject
        {
            get { return subject; }
            set { subject = value; }
        }

    }
}
