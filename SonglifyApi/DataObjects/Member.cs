using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Mobile.Service;

namespace SonglifyApi.DataObjects
{
    public class Member : EntityData
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string SessionId { get; set; }

        public virtual ICollection<Recommendation> Favorites { get; set; }

    }
}
