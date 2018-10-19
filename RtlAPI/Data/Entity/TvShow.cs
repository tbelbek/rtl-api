using System.Collections.Generic;
using RtlAPI.Data.Entity.Base;

namespace RtlAPI.Data.Entity
{

    public class Schedule : BaseEntity<int>
    {
        public string Time { get; set; }
        public virtual List<string> Days { get; set; }
    }

    public class Rating : BaseEntity<int>
    {
        public double? Average { get; set; }
    }

    public class Country : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Timezone { get; set; }
    }

    public class Network : BaseEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Country Country { get; set; }
    }

    public class Country2 : BaseEntity<int>
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Timezone { get; set; }
    }

    public class WebChannel : BaseEntity<int>
    {
        public string Name { get; set; }
        public virtual Country2 Country { get; set; }
    }

    public class Externals : BaseEntity<int>
    {
        public int Tvrage { get; set; }
        public int Thetvdb { get; set; }
        public string Imdb { get; set; }
    }

    public class Image : BaseEntity<int>
    {
        public string Medium { get; set; }
        public string Original { get; set; }
    }

    public class Self : BaseEntity<int>
    {
        public string Href { get; set; }
    }

    public class Previousepisode : BaseEntity<int>
    {
        public string Href { get; set; }
    }

    public class Nextepisode : BaseEntity<int>
    {
        public string Href { get; set; }
    }

    public class Links : BaseEntity<int>
    {
        public virtual Self Self { get; set; }
        public virtual Previousepisode Previousepisode { get; set; }
        public virtual Nextepisode Nextepisode { get; set; }
    }

    public class TvShow : BaseEntity<int>
    {
        public int TvMazeId { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Language { get; set; }
        public virtual List<object> Genres { get; set; }
        public string Status { get; set; }
        public int Runtime { get; set; }
        public string Premiered { get; set; }
        public string OfficialSite { get; set; }
        public virtual Schedule Schedule { get; set; }
        public virtual Rating Rating { get; set; }
        public int Weight { get; set; }
        public virtual Network Network { get; set; }
        public virtual WebChannel WebChannel { get; set; }
        public virtual Externals Externals { get; set; }
        public virtual Image Image { get; set; }
        public virtual string Summary { get; set; }
        public virtual int Updated { get; set; }
        public virtual Links Links { get; set; }
        public virtual List<CastPerson> Crew { get; set; }

    }

    public class Person : BaseEntity<int>
    {
        public string Url { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public string Birthday { get; set; }
        public object Deathday { get; set; }
        public string Gender { get; set; }
        public Image Image { get; set; }
        public Links Links { get; set; }
    }

    public class Image2 : BaseEntity<int>
    {
        public string Medium { get; set; }
        public string Original { get; set; }
    }

    public class Self2 : BaseEntity<int>
    {
        public string Href { get; set; }
    }

    public class Links2 : BaseEntity<int>
    {
        public Self2 Self { get; set; }
    }

    public class Character : BaseEntity<int>
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public Image2 Image { get; set; }
        public Links2 Links { get; set; }
    }

    public class CastPerson : BaseEntity<int>
    {
        public Person Person { get; set; }
        public Character Character { get; set; }
        public int PersonId { get; set; }
        public int ShowId { get; set; }
        public bool Self { get; set; }
        public bool Voice { get; set; }
    }
}