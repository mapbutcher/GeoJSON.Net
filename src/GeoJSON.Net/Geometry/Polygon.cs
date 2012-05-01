// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Polygon.cs" company="Jörg Battermann">
//   Copyright © Jörg Battermann 2011
// </copyright>
// <summary>
//   Defines the <see cref="http://geojson.org/geojson-spec.html#polygon">Polygon</see> type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace GeoJSON.Net.Geometry
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Newtonsoft.Json;

    /// <summary>
    /// Defines the <see cref="http://geojson.org/geojson-spec.html#polygon">Polygon</see> type.
    /// Coordinates of a Polygon are a list of <see cref="http://geojson.org/geojson-spec.html#linestring">linear rings</see>
    /// coordinate arrays. The first element in the array represents the exterior ring. Any subsequent elements
    /// represent interior rings (or holes).
    /// </summary>
    /// <seealso cref="http://geojson.org/geojson-spec.html#polygon"/>
    public class Polygon : GeoJSONObject, IGeometryObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        /// <param name="linearRings">
        /// The <see cref="http://geojson.org/geojson-spec.html#linestring">linear rings</see> with the first element
        /// in the array representing the exterior ring. Any subsequent elements represent interior rings (or holes).
        /// </param>
        public Polygon(List<LineString> linearRings = null)
        {
            if (linearRings == null)
            {
                throw new ArgumentNullException("linearRings");
            }

            if (linearRings.Any(linearRing => !linearRing.IsLinearRing()))
            {
                throw new ArgumentOutOfRangeException("linearRings", "All elements must be closed LineStrings with 4 or more positions (see GeoJSON spec at 'http://geojson.org/geojson-spec.html#linestring').");
            }

            //this.Coordinates = linearRings;
            this.Type = GeoJSONObjectType.Polygon;
        }

        //SH - Added to remove the reqt to use LineString class causes issues with the output i.e. it includes the type linestring as a sub type of polygon
        /// <summary>
        /// Initializes a new instance of the <see cref="Polygon"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public Polygon(List<List<List<double>>> coordinates)
        {
            //todo - implement linear ring checks

            this.Coordinates = coordinates;
            this.Type = GeoJSONObjectType.Polygon;
        }

        /// <summary>
        /// Gets the list of points outlining this Polygon.
        /// </summary>
        ///[JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        ///public List<LineString> Coordinates { get; private set; }

        //SH _ Added - having a property as a linestring causes issues with the output i.e. it includes the type linestring as a sub type of polygon
        /// <summary>
        /// Gets the Positions.
        /// </summary>
        /// <value>The Positions.</value>
        [JsonProperty(PropertyName = "coordinates", Required = Required.Always)]
        //[JsonConverter(typeof(PositionConverter))]
        public List<List<List<double>>> Coordinates { get; set; }


    }
}
