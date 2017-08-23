using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstEvent.Model
{
    public class Insurance
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public int OfficeId { set; get; }
    }

    public class InsuranceView
    {
        public int Id { set; get; }

        public string Name { set; get; }

        public string Office { set; get; }
    }
}
