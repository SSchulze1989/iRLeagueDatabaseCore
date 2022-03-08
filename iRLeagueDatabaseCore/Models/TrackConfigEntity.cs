using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iRLeagueDatabaseCore.Models
{
    public class TrackConfigEntity
    {
        public long TrackId { get; set; }
        public long TrackGroupId { get; set; }
        public virtual TrackGroupEntity TrackGroup { get; set; }
        public string ConfigName { get; set; }
        public double LengthKm { get; set; }
        public int Turns { get; set; }
        public ConfigType ConfigType { get; set; }
        public bool HasNigtLigthing { get; set; }
        public string MapImageSrc { get; set; }

    }

    public enum ConfigType
    {
        ShortTrack,
        Speedway,
        Rallycross,
        RoadCourse,
        DirtOval,
        Unknown
    }
}
