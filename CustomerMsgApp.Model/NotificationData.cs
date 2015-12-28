using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerMsgApp.Model
{
    [Table("NotificationData")]
    public class NotificationData
    {
        [Key]
        public long BookingRef { get; set; }
        [StringLength(255)]
        public string TourOpCode { get; set; }
        public int PassengerId { get; set; }
        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string Surname { get; set; }
        [StringLength(255)]
        public string MobileNo { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(255)]
        public string DirectOrAgent { get; set; }
        public DateTime StartDate { get; set; }
        [StringLength(255)]
        public string DeparturePoint { get; set; }
        [StringLength(255)]
        public string ArrivalPoint { get; set; }
        public DateTime? TravelDate { get; set; }
        [StringLength(255)]
        public string TravelDepatureTime { get; set; }
        [StringLength(255)]
        public string TravelArrivalTime { get; set; }
        [StringLength(255)]
        public string TravelDirection { get; set; }
        [StringLength(255)]
        public string TransportCarrier { get; set; }
        [StringLength(255)]
        public string TransportNumber { get; set; }
        [StringLength(255)]
        public string TransportType { get; set; }
        [StringLength(255)]
        public string TransportChain { get; set; }
        [StringLength(255)]
        public string CountryCode { get; set; }
        [StringLength(255)]
        public string CountryName { get; set; }
        [StringLength(255)]
        public string ResortCode { get; set; }
        [StringLength(255)]
        public string ResortName { get; set; }
        [StringLength(255)]
        public string AccommodationCode { get; set; }
        [StringLength(255)]
        public string AccommodationName { get; set; }
    }
}
