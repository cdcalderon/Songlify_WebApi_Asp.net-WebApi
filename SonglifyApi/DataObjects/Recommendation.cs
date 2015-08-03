using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Mobile.Service;

namespace SonglifyApi.DataObjects
{
    public class Recommendation : EntityData
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string PreviewUrl { get; set; }
        public string ImageSmall { get; set; }
        public string ImageMedium { get; set; }
        public string ImageLarge { get; set; }
        public string OpenUrl { get; set; }
        public string SongId { get; set; }

        public string MemberIdentifier { get; set; }
    }
}
