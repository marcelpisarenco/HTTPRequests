using System;
using System.Collections.Generic;
using System.Text;

namespace Lab3
{
    class Account
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Adress { get; set; }
        public int Port { get; set; }

        public Account(string Adress, int Port)
        {
            this.Login = "Selmarcelpisarenco";
            this.Password = "H4q3FbY";
            this.Adress = Adress;
            this.Port = Port;
        }

    }
}
