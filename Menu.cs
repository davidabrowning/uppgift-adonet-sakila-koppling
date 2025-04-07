using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADOnetSakilaKoppling
{
    internal class Menu
    {
        private readonly Input input;
        private readonly Output output;
        private readonly Repository repository;
        public Menu(Input input, Output output, Repository repository)
        {
            this.input = input;
            this.output = output;
            this.repository = repository;
        }
    }
}
