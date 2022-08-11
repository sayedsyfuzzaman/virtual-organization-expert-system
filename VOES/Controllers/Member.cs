using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VOES.Controllers
{
    public class Member
    {
        private String id, name, phone, email, designation, address, dob, bloodgroup, usertype, username, pass, addedby, additiondate;
        private byte[] image;


        public String Additiondate
        {
            get { return additiondate; }
            set { additiondate = value; }
        }

        public String Addedby
        {
            get { return addedby; }
            set { addedby = value; }
        }

        public String Pass
        {
            get { return pass; }
            set { pass = value; }
        }

        public String Username
        {
            get { return username; }
            set { username = value; }
        }

        public String Usertype
        {
            get { return usertype; }
            set { usertype = value; }
        }

        public String Bloodgroup
        {
            get { return bloodgroup; }
            set { bloodgroup = value; }
        }

        public String Dob
        {
            get { return dob; }
            set { dob = value; }
        }

        public String Address
        {
            get { return address; }
            set { address = value; }
        }

        public String Designation
        {
            get { return designation; }
            set { designation = value; }
        }

        public String Email
        {
            get { return email; }
            set { email = value; }
        }

        public String Phone
        {
            get { return phone; }
            set { phone = value; }
        }

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String Id
        {
            get { return id; }
            set { id = value; }
        }

        public byte[] Image
        {
            get { return image; }
            set { image = value; }
        }

    }
}
